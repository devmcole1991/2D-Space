using UnityEngine;
using Assets.Update;

namespace  Assets.GameLogic.Core
{
	public class Velocity : MonoBehaviour, IUpdatable
	{
		private SubPixelVelocity velocity;
		public Vector3 Real { get { return velocity.real; } }
		public Vector3 Delta { get { return velocity.Delta; } }

		private void OnEnable()
		{
			UpdateManager.VelocityUpdater.Register(this);
		}

		private void OnDisable()
		{
			UpdateManager.VelocityUpdater.Deregister(this);
		}

		public void OnUpdate()
		{
			velocity.Update();
		}

		public void Set(Vector3 velocity)
		{
			this.velocity.real = velocity;
		}

		public void Set(float x, float y)
		{
			velocity.real.Set(x, y, 0f);
		}

		public void SetX(float velocity)
		{
			this.velocity.real.x = velocity;
		}

		public void SetY(float velocity)
		{
			this.velocity.real.y = velocity;
		}

		public void Add(Vector3 velocity)
		{
			this.velocity.real += velocity;
		}

		public void AddHorizontal(float velocity)
		{
			this.velocity.real.x += velocity;
		}

		public void AddVertical(float velocity)
		{
			this.velocity.real.y += velocity;
		}

		public void Stop()
		{
			this.velocity.Reset();
		}

		public void StopHorizontal()
		{
			velocity.ResetX();
		}

		public void StopVertical()
		{
			velocity.ResetY();
		}
	}
}