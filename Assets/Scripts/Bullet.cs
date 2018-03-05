﻿using UnityEngine;
using Assets.Update;
using UnityEngine.Events;

namespace Assets.GameLogic.Core
{
    [RequireComponent(typeof(Velocity))]
    public class Bullet : MonoBehaviour, IUpdatable
    {
        private Velocity velocity;

        [SerializeField] private int damage;
        [SerializeField] private int bulletSpeed;
        [SerializeField] private UnityEvent BulletHit;

        SpriteRenderer player; 

        private void Awake()
        {
            /*velocity = GetComponent<Velocity>();
            player = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();

            if (player.flipX)
                velocity.SetX(-bulletSpeed);
            else
                velocity.SetX(bulletSpeed);*/
        }

        private void OnEnable()
        {
            UpdateManager.GeneralUpdater.Register(this);
        }

        private void OnDisable()
        {
            UpdateManager.GeneralUpdater.Deregister(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            BulletHit.Invoke();
            Destroy(gameObject);
        }

        public void OnUpdate()
        {
            transform.Translate(Vector3.right * Time.deltaTime * bulletSpeed);
        }
    }
}
