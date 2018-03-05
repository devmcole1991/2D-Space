using UnityEngine;
using Assets.Update;
using Assets.UI;

namespace Assets.GameLogic.Core
{
    public sealed class UpdateManager : MonoBehaviour
    {
        private static UpdateManager instance;
        
        private static Updater<IUpdatable> generalUpdater = new Updater<IUpdatable>();
		private static Updater<PlayerController> playerControllerUpdater = new Updater<PlayerController>();
		private static Updater<PlatformCharacter> platformCharacterUpdater = new Updater<PlatformCharacter>();
		private static Updater<Velocity> velocityUpdater = new Updater<Velocity>();
		private static Updater<SimpleMovement> simpleMovementUpdater = new Updater<SimpleMovement>();
		private static Updater<SimulatePhysics> simulatePhysicsUpdater = new Updater<SimulatePhysics>();
		private static Updater<FollowPath> followPathUpdater = new Updater<FollowPath>();
		private static Updater<MovingPlatform> movingPlatformUpdater = new Updater<MovingPlatform>();
<<<<<<< HEAD
		private static Updater<UpdatableUI> uiUpdater = new Updater<UpdatableUI>();
=======
>>>>>>> devin-changes

		public static IUpdater<IUpdatable> GeneralUpdater { get { return generalUpdater; } }
		public static IUpdater<PlayerController> PlayerControllerUpdater { get { return playerControllerUpdater; } }
		public static IUpdater<PlatformCharacter> PlatformCharacterUpdater { get { return platformCharacterUpdater; } }
		public static IUpdater<Velocity> VelocityUpdater { get{ return velocityUpdater; } }
		public static IUpdater<SimpleMovement> SimpleMovementUpdater { get { return simpleMovementUpdater; } }
		public static IUpdater<SimulatePhysics> SimulatePhysicsUpdater { get { return simulatePhysicsUpdater; } }
		public static IUpdater<FollowPath> FollowPathUpdater { get { return followPathUpdater; } }
		public static IUpdater<MovingPlatform> MovingPlatformUpdater { get { return movingPlatformUpdater; } }
<<<<<<< HEAD
		public static IUpdater<UpdatableUI> UiUpdater { get { return uiUpdater; } }
=======
>>>>>>> devin-changes
		
		private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
				QualitySettings.vSyncCount = 0;
				Application.targetFrameRate = 60;
            }
            else if (instance != this)
            {
                Debug.LogWarning("Instance of " + GetType().Name + " already exists. Removed.");
                Destroy(gameObject);
            }
        }

        private void Update()
        {
			playerControllerUpdater.Update();
			platformCharacterUpdater.Update();
			velocityUpdater.Update();
			followPathUpdater.Update();
			movingPlatformUpdater.Update();
			simpleMovementUpdater.Update();
			simulatePhysicsUpdater.Update();

			generalUpdater.Update();
<<<<<<< HEAD
			uiUpdater.Update();
=======
>>>>>>> devin-changes
        }
    }
}