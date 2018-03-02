using UnityEngine;
using Assets.Update;

namespace Assets.GameLogic.Core
{
    public class BulletMovement : MonoBehaviour, IUpdatable
    {
        [SerializeField] private int bulletSpeed;

        private void OnEnable()
        {
            UpdateManager.BulletUpdater.Register(this);
        }

        private void OnDisable()
        {
            UpdateManager.BulletUpdater.Deregister(this);
        }

        public void OnUpdate()
        {
            // Check player facing
            transform.Translate(Vector3.right * Time.deltaTime * bulletSpeed);
        }
    }
}
