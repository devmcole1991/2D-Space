using UnityEngine;
using Assets.Update;

namespace Assets.GameLogic.Core
{
	[RequireComponent(typeof(Velocity))]
	[RequireComponent(typeof(SimulatePhysics))]
	[RequireComponent(typeof(IPlatformCharacterController))]
	public class PlatformCharacter : MonoBehaviour, IUpdatable
	{
		private Velocity velocity;
		private SimulatePhysics physics;
		private IPlatformCharacterController controller;

		[SerializeField] private float runSpeed;
		[SerializeField] private float jumpSpeed;

		private void Awake()
		{
			velocity = GetComponent<Velocity>();
			physics = GetComponent<SimulatePhysics>();
			controller = GetComponent<IPlatformCharacterController>();
		}

		private void OnEnable()
		{
			UpdateManager.PlatformCharacterUpdater.Register(this);
		}

		private void OnDisable()
		{
			UpdateManager.PlatformCharacterUpdater.Deregister(this);
		}

		public void ResetController()
		{
			controller = GetComponent<IPlatformCharacterController>();
		}

		public void SetController(IPlatformCharacterController controller)
		{
			if (controller != null)
			{
				this.controller = controller;
			}
		}

		public void OnUpdate()
		{
            int horizontal = controller.Right - controller.Left;
			velocity.SetX(runSpeed * horizontal);

			if (controller.JumpPressed != 0 && physics.Ground)
			{
				velocity.SetY(jumpSpeed);
			}
		}
	}
}
