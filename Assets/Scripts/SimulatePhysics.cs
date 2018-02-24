﻿using UnityEngine;
using Assets.Update;

namespace Assets.GameLogic.Core
{
	[RequireComponent(typeof(Velocity))]
	public class SimulatePhysics : MonoBehaviour, IUpdatable
	{
		[System.Serializable]
		private struct CollisionRect
		{
			public int topOffset;
			public int bottomOffset;
			public int rightOffset;
			public int leftOffset;
		}

		[SerializeField] private LayerMask solidMask;
		[SerializeField] private LayerMask onewayMask;
		[SerializeField] private float gravityScale = 1f;
		[SerializeField] private CollisionRect rect;

		new private Transform transform;
		private Velocity velocity;

		public Vector3 PreviousPosition { get; private set; }
		public MovingPlatform Transport { get; private set; }
		public Collider2D Ground { get; private set; }

		private void Awake()
		{
			transform = base.transform;
			PreviousPosition = transform.position;
			velocity = GetComponent<Velocity>();
		}

		private void OnEnable()
		{
			UpdateManager.SimulatePhysicsUpdater.Register(this);
		}

		private void OnDisable()
		{
			UpdateManager.SimulatePhysicsUpdater.Deregister(this);
		}

		private void ApplyGravity()
		{
			if (!Ground)
			{
				velocity.AddVertical(WorldData.gravity * gravityScale);
			}
		}

		private void MoveAndCollide(Vector3 delta)
		{
			var position = transform.position;
			var origin = position + new Vector3(rect.rightOffset + rect.leftOffset, rect.topOffset + rect.bottomOffset) / 2f;
			var size = new Vector2(rect.rightOffset - rect.leftOffset, rect.topOffset - rect.bottomOffset);
			var initialDelta = delta;

			ResolveHorizontal(ref position, ref delta, ref origin, size);
			ResolveVertical(ref position, ref delta, ref origin, size);

			if (initialDelta.y < 0)
			{
				ResolveOneway(ref position);
			}

			PreviousPosition = transform.position;
			transform.position = position;

			CheckGround(ref position);

			if (Ground)
			{
				var transport = Ground.GetComponent<MovingPlatform>();

				if (transport && transport != Transport)
				{
					if (Transport)
					{
						Transport.RemovePassenger(this);
					}

					Transport = transport;
					Transport.AddPassenger(this);
				}
			}
			else if (Transport)
			{
				Transport.RemovePassenger(this);
				Transport = null;
			}
		}

		private void ResolveHorizontal(ref Vector3 position, ref Vector3 delta, ref Vector3 origin, Vector3 size)
		{
			var horizontal = new Vector2(Mathf.Sign(delta.x), 0f);
			var hit = Physics2D.BoxCast(origin, size, 0f, horizontal, delta.x * horizontal.x, solidMask);

			if (hit)
			{
				velocity.StopHorizontal();
				delta.x = Mathf.Round(hit.distance) * horizontal.x - horizontal.x;
			}

			position.x += delta.x;
			origin.x += delta.x;
		}

		private void ResolveVertical(ref Vector3 position, ref Vector3 delta, ref Vector3 origin, Vector3 size)
		{
			var vertical = new Vector2(0f, Mathf.Sign(delta.y));
			var hit = Physics2D.BoxCast(origin, size, 0f, vertical, delta.y * vertical.y, solidMask);

			if (hit)
			{
				velocity.StopVertical();
				delta.y = Mathf.Round(hit.distance) * vertical.y - vertical.y;
			}

			position.y += delta.y;
			origin.x += delta.y;
		}

		private void ResolveOneway(ref Vector3 position)
		{
			var hits = Physics2D.LinecastAll(position + new Vector3(rect.leftOffset, rect.bottomOffset),
					position + new Vector3(rect.rightOffset, rect.bottomOffset), onewayMask);

			for (int i = 0; i < hits.Length; ++i)
			{
				var hit = hits[i];

				if (hit && hit.collider.bounds.max.y < PreviousPosition.y + rect.bottomOffset)
				{
					velocity.StopVertical();
					position.y = hit.collider.bounds.max.y - rect.bottomOffset + 1;
				}
			}
		}

		private void CheckGround(ref Vector3 position)
		{
			var hit = Physics2D.Linecast(position + new Vector3(rect.leftOffset, rect.bottomOffset - 1),
					position + new Vector3(rect.rightOffset, rect.bottomOffset - 1), solidMask);

			Ground = hit.collider;

			var hits = Physics2D.LinecastAll(position + new Vector3(rect.leftOffset, rect.bottomOffset - 1),
					position + new Vector3(rect.rightOffset, rect.bottomOffset - 1), onewayMask);

			for (int i = 0; i < hits.Length; ++i)
			{
				hit = hits[i];

				if (hit && hit.collider.bounds.max.y < PreviousPosition.y + rect.bottomOffset)
				{
					Ground = hit.collider;
					break;
				}
			}

		}

		public void Move(Vector3Int delta)
		{
			MoveAndCollide(delta);
		}

		public void OnUpdate()
		{
			MoveAndCollide(velocity.Delta);
			ApplyGravity();
		}

#if UNITY_EDITOR
		private void OnDrawGizmosSelected()
		{
			var position = base.transform.position;
			var tl = position + new Vector3(rect.leftOffset, rect.topOffset);
			var tr = position + new Vector3(rect.rightOffset, rect.topOffset);
			var bl = position + new Vector3(rect.leftOffset, rect.bottomOffset);
			var br = position + new Vector3(rect.rightOffset, rect.bottomOffset);

			Gizmos.color = Color.green;
			Gizmos.DrawLine(tl, tr);
			Gizmos.DrawLine(tr, br);
			Gizmos.DrawLine(br, bl);
			Gizmos.DrawLine(bl, tl);
		}
#endif
	}
}