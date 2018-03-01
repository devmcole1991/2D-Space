using UnityEngine;
using Assets.Update;

namespace Assets.GameLogic.Core
{
    [RequireComponent(typeof(Velocity))]
    [RequireComponent(typeof(SimulatePhysics))]
    public class PlayerAnimations : MonoBehaviour, IUpdatable
    {
        private Velocity velocity;
        private SimulatePhysics physics;
        private SpriteRenderer renderer;
        private Animator animator;

        private void Awake()
        {
            velocity = GetComponent<Velocity>();
            physics = GetComponent<SimulatePhysics>();
            renderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            UpdateManager.PlayerAnimationsUpdater.Register(this);
        }

        private void OnDisable()
        {
            UpdateManager.PlayerAnimationsUpdater.Deregister(this);
        }

        public void OnUpdate()
        {
            if (velocity.Real.x != 0 && physics.Ground)
            {
                animator.SetInteger("State", 1);
            }
            else
            {
                animator.SetInteger("State", 0);
            }

            bool flipSprite = (renderer.flipX ? (velocity.Real.x > 0) : (velocity.Real.x < 0));

            if (flipSprite)
            {
                renderer.flipX = !renderer.flipX;
            }
        }
    }
}