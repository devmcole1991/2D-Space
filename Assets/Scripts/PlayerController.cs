using UnityEngine;
using Assets.Update;

namespace Assets.GameLogic.Core
{
	public class PlayerController : MonoBehaviour, IPlatformCharacterController, IPickerUpperController, IShootController, IUpdatable
	{
		public int VerticalAxis { get; private set; }
		public int HorizontalAxis { get; private set; }
		public bool JumpPressed { get; private set; }
		public bool JumpHeld { get; private set; }
        public bool ShootPressed { get; private set; }
        public bool ShootHeld { get; private set; }

        public bool WantsToPickUp { get; private set; }

		private void OnEnable()
		{
			UpdateManager.PlayerControllerUpdater.Register(this);
		}

		private void OnDisable()
		{
			UpdateManager.PlayerControllerUpdater.Deregister(this);
		}

		public void OnUpdate()
		{
			VerticalAxis = (int)Input.GetAxisRaw("Vertical");
			HorizontalAxis = (int)Input.GetAxisRaw("Horizontal");
			JumpPressed = Input.GetButtonDown("XBoxA");
			JumpHeld = Input.GetButton("XBoxA");
            ShootPressed = Input.GetKeyDown(KeyCode.Mouse0);
            ShootHeld = Input.GetKey(KeyCode.Mouse0);
            WantsToPickUp = Input.GetButtonDown("XBoxB");
		}
	}
}
