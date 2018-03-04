using UnityEngine;
using Assets.Update;
using UnityEngine.Events;

namespace Assets.GameLogic.Core
{
    [RequireComponent(typeof(Velocity))]
    public class Bullet : MonoBehaviour
    {
        private Velocity velocity;

        [SerializeField] private int damage;
        [SerializeField] private int bulletSpeed;
        [SerializeField] private UnityEvent BulletHit;

        SpriteRenderer player; 

        private void Awake()
        {
            velocity = GetComponent<Velocity>();
            player = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();

            if (player.flipX)
                velocity.SetX(-bulletSpeed);
            else
                velocity.SetX(bulletSpeed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            BulletHit.Invoke();
            Destroy(gameObject);
        }
    }
}
