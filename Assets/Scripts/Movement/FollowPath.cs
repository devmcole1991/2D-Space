using UnityEngine;
using System.Collections.Generic;

// TODO: Create custom inspector that uses offsets instead of absolute points.
// Remove reposition from Awake()

namespace Assets.GameLogic.Core
{
	public class FollowPath : MovementBase
	{
		[SerializeField] private bool reverseAtEnd = true;
		[SerializeField] private float speed;
		[SerializeField] private List<Vector2> path;
		private SubPixelVelocity velocity = new SubPixelVelocity();
		private bool reversing = false;
		private int targetIndex;

		protected override void Awake()
		{
			base.Awake();
			transform.position = path[0];
			targetIndex = 1;

			velocity.real = (path[1] - path[0]).normalized * speed;
		}

		private void OnEnable()
		{
			UpdateManager.FollowPathUpdater.Register(this);
		}

		private void OnDisable()
		{
			UpdateManager.FollowPathUpdater.Deregister(this);
		}

		protected override void Move()
		{
			var position = (Vector2)transform.position;
			var deltaToTarget = path[targetIndex] - position;
			var distance = deltaToTarget.magnitude;

			velocity.Update();
			var delta = velocity.Delta;
			var speed = delta.magnitude;

			if (distance < speed)
			{
				Move(deltaToTarget);
				
				if (reversing)
				{
					--targetIndex;

					if (targetIndex < 0)
					{
						reversing = false;
						targetIndex = 1;
					}
				}
				else
				{
					++targetIndex;

					if (targetIndex >= path.Count)
					{
						if (reverseAtEnd)
						{
							targetIndex = path.Count - 2;
							reversing = true;
						}
						else
						{
							targetIndex = 0;
						}
					}
				}

				velocity.real = ((Vector3)path[targetIndex] - transform.position).normalized * this.speed;
			}
			else
			{
				Move(delta);
			}
		}

		private void Move(Vector3 delta)
		{
			PreviousPosition = transform.position;
			transform.position += delta;
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			var position = GetComponent<Transform>().position;

			while (path.Count < 2)
			{
				path.Add(position);
			}

			if (speed < 0f)
			{
				speed = 0f;
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;

			int count = path.Count;

			if (count > 0)
			{
				Vector2 a = path[0];
				Vector2 b;
				
				Gizmos.DrawSphere(a, 2f);

				for (int i = 1; i < count; ++i)
				{
					b = path[i];

					Gizmos.DrawLine(a, b);
					Gizmos.DrawSphere(b, 2f);
					a = b;
				}

				if (!reverseAtEnd && count > 2)
				{
					Gizmos.DrawLine(path[0], path[count - 1]);
				}
			}
		}
#endif
	}
}
