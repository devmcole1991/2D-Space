using UnityEngine;

namespace Assets.GameLogic.Core
{
	[RequireComponent(typeof(Velocity))]
	public class SimpleMovement : MovementBase
	{
		private Velocity velocity;

		public Vector3 Velocity { get { return velocity.Real; } }

		protected override void Awake()
		{
			base.Awake();
			velocity = GetComponent<Velocity>();
		}

		private void OnEnable()
		{
			UpdateManager.SimpleMovementUpdater.Register(this);
		}

		private void OnDisable()
		{
			UpdateManager.SimpleMovementUpdater.Deregister(this);
		}

		protected override void Move()
		{
			transform.position += velocity.Delta;
		}
	}
}