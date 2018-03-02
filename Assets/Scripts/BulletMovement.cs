using UnityEngine;
using Assets.Update;
using UnityEngine.Events;

namespace Assets.GameLogic.Core
{
    public class BulletMovement : MonoBehaviour, IUpdatable
    {
        [SerializeField] private int bulletSpeed;
        [SerializeField] private int Damage;

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
            transform.Translate(Vector3.right * Time.deltaTime * bulletSpeed);
        }
    }
}
