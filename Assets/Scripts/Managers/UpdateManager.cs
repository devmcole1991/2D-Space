using UnityEngine;
using Assets.Update;

namespace Assets.GameLogic.Core
{
    public sealed class UpdateManager : MonoBehaviour
    {
        private static UpdateManager instance;
        
        private static Updater<IUpdatable> generalUpdater = new Updater<IUpdatable>();
		private static Updater<PlayerController> playerControllerUpdater = new Updater<PlayerController>();
		private static Updater<PlatformCharacter> platformCharacterUpdater = new Updater<PlatformCharacter>();
		private static Updater<Velocity> velocityUpdater = new Updater<Velocity>();
		private static Updater<SimulatePhysics> simulatePhysicsUpdater = new Updater<SimulatePhysics>();
		private static Updater<MovingPlatform> movingPlatformUpdater = new Updater<MovingPlatform>();
        private static Updater<PlayerAnimations> playerAnimationsUpdater = new Updater<PlayerAnimations>();
        private static Updater<Weapon> weaponUpdater = new Updater<Weapon>();
        private static Updater<BulletMovement> bulletUpdater = new Updater<BulletMovement>();

        public static IUpdater<IUpdatable> GeneralUpdater { get { return generalUpdater; } }
		public static IUpdater<PlayerController> PlayerControllerUpdater { get { return playerControllerUpdater; } }
		public static IUpdater<PlatformCharacter> PlatformCharacterUpdater { get { return platformCharacterUpdater; } }
		public static IUpdater<Velocity> VelocityUpdater { get{ return velocityUpdater; } }
		public static IUpdater<SimulatePhysics> SimulatePhysicsUpdater { get { return simulatePhysicsUpdater; } }
		public static IUpdater<MovingPlatform> MovingPlatformUpdater { get { return movingPlatformUpdater; } }
        public static IUpdater<PlayerAnimations> PlayerAnimationsUpdater { get { return playerAnimationsUpdater; } }
        public static IUpdater<Weapon> WeaponUpdater { get { return weaponUpdater; } }
        public static IUpdater<BulletMovement> BulletUpdater { get { return bulletUpdater; } }

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
            playerAnimationsUpdater.Update();
            velocityUpdater.Update();
			simulatePhysicsUpdater.Update();
            weaponUpdater.Update();
            bulletUpdater.Update();
            movingPlatformUpdater.Update();

            generalUpdater.Update();
        }
    }
}