using UnityEngine;
using Assets.Update;
using UnityEngine.Events;

namespace Assets.GameLogic.Core
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private UnityEvent BulletHit;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            BulletHit.Invoke();
            Destroy(gameObject);
        }
    }
}
