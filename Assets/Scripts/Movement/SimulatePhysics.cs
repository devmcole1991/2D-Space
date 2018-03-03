using UnityEngine;

namespace Assets.GameLogic.Core
{
	[RequireComponent(typeof(Velocity))]
	public class SimulatePhysics : MovementBase
	{
		[SerializeField] private LayerMask solidMask;
		[SerializeField] private LayerMask onewayMask;
		[SerializeField] private float gravityScale = 1f;
		[SerializeField] private BoundingBox bbox;
		private Velocity velocity;

		public Vector3 Velocity { get { return velocity.Real; } }
		public MovingPlatform Transport { get; private set; }
		public Collider2D Ground { get; private set; }

		protected override void Awake()
		{
			base.Awake();
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
			var origin = position + new Vector3(bbox.RightOffset + bbox.LeftOffset, bbox.TopOffset + bbox.BottomOffset) / 2f;
			var size = new Vector2(bbox.RightOffset - bbox.LeftOffset, bbox.TopOffset - bbox.BottomOffset);
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
			var hits = Physics2D.LinecastAll(position + new Vector3(bbox.LeftOffset, bbox.BottomOffset),
					position + new Vector3(bbox.RightOffset, bbox.BottomOffset), onewayMask);

			for (int i = 0; i < hits.Length; ++i)
			{
				var hit = hits[i];

				if (hit && hit.collider.bounds.max.y < PreviousPosition.y + bbox.BottomOffset)
				{
					velocity.StopVertical();
					position.y = hit.collider.bounds.max.y - bbox.BottomOffset + 1;
				}
			}
		}

		private void CheckGround(ref Vector3 position)
		{
			var hit = Physics2D.Linecast(position + new Vector3(bbox.LeftOffset, bbox.BottomOffset - 1),
					position + new Vector3(bbox.RightOffset, bbox.BottomOffset - 1), solidMask);

			Ground = hit.collider;

			var hits = Physics2D.LinecastAll(position + new Vector3(bbox.LeftOffset, bbox.BottomOffset - 1),
					position + new Vector3(bbox.RightOffset, bbox.BottomOffset - 1), onewayMask);

			for (int i = 0; i < hits.Length; ++i)
			{
				hit = hits[i];

				if (hit && hit.collider.bounds.max.y < PreviousPosition.y + bbox.BottomOffset)
				{
					Ground = hit.collider;
					break;
				}
			}

		}

		protected override void Move()
		{
			MoveAndCollide(velocity.Delta);
			ApplyGravity();
		}

		public void Move(Vector3Int delta)
		{
			MoveAndCollide(delta);
		}
	}
}