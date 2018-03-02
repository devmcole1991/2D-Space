using UnityEngine;
using Assets.Update;

namespace Assets.GameLogic.Core
{
    public class Weapon : MonoBehaviour, IUpdatable
    {
        private IPlatformCharacterController controller;
        private SpriteRenderer renderer;

        [SerializeField] private float fireRate;
        [SerializeField] private float timeToFire;
        public LayerMask whatToHit;

        public Transform BulletPrefab;
        public Transform MuzzleFlashPrefab;
        Transform firePoint;

        private void Awake()
        {
            controller = GetComponent<IPlatformCharacterController>();
            renderer = GetComponent<SpriteRenderer>();
            firePoint = transform.Find("FirePoint");

            if (firePoint == null)
            {
                Debug.LogError("No firepoint");
            }
        }

        private void OnEnable()
        {
            UpdateManager.WeaponUpdater.Register(this);
        }

        private void OnDisable()
        {
            UpdateManager.WeaponUpdater.Deregister(this);
        }

        public void OnUpdate()
        {
            if (renderer.flipX)
            {
                firePoint.localPosition = new Vector3(-21, 21, 0);
                firePoint.localRotation = new Quaternion(0, 180, 0, 0);
            }
            else
            {
                firePoint.localPosition = new Vector3(21, 21, 0);
                firePoint.localRotation = new Quaternion(0, 0, 0, 0);
            }

            if (fireRate == 0)
            {
                if (controller.ShootPressed == 1)
                    Shoot();
            }
            else
            {
                if (controller.ShootHeld == 1 && Time.time > timeToFire)
                {
                    timeToFire = Time.time + 1 / fireRate;
                    Shoot();
                }
            }
        }

        void Shoot()
        {
            Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
            RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition-firePointPosition, 100, whatToHit);
            Effect();
        }

        void Effect()
        {
            Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
            Transform clone = Instantiate(MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
            Destroy(clone.gameObject, 0.03f);
        }
    }
}
