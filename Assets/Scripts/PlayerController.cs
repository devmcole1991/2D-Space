using UnityEngine;
using Assets.Update;

namespace Assets.GameLogic.Core
{
	public class PlayerController : MonoBehaviour, IPlatformCharacterController, IUpdatable
	{
		public int Up { get; private set; }
		public int Down { get; private set; }
		public int Left { get; private set; }
		public int Right { get; private set; }
		public int JumpPressed { get; private set; }
		public int JumpHeld { get; private set; }

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
			Up = Input.GetKey(KeyCode.W) ? 1 : 0;
			Down = Input.GetKey(KeyCode.S) ? 1 : 0;
			Left = Input.GetKey(KeyCode.A) ? 1 : 0;
			Right = Input.GetKey(KeyCode.D) ? 1 : 0;
			JumpPressed = Input.GetKeyDown(KeyCode.Space) ? 1 : 0;
			JumpHeld = Input.GetKey(KeyCode.Space) ? 1 : 0;
		}
	}
}
