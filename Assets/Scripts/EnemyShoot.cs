using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.GameLogic.Core
{
    [RequireComponent(typeof(EnemyWeapon))]
    public class EnemyShoot : MonoBehaviour
    {
        private EnemyWeapon weapon;
        private Animator animator;
        private EnemyFOV enemyFOV;

        [SerializeField] private float shootDelay;

        void Awake()
        {
            weapon = GetComponent<EnemyWeapon>();
            animator = GetComponent<Animator>();
            enemyFOV = GetComponent<EnemyFOV>();

            InvokeRepeating("CanShoot", 2.0f, shootDelay);
        }

        void CanShoot()
        {
            if (enemyFOV.visiblePlayers.Count != 0)
            {
                weapon.Shoot();
            }
        }
    }
}
