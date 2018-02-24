using UnityEngine;
using Assets.Update;

namespace  Assets.GameLogic.Core
{
	public class Velocity : MonoBehaviour, IUpdatable
	{
		private Vector3 real;
		private Vector3 delta;
		private Vector3 excess;
		public Vector3 Real { get { return real; } }
		public Vector3 Delta { get { return delta; } }

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
			excess += real;
			delta.Set((int)excess.x, (int)excess.y, 0f);
			excess -= Delta;
		}

		public void Set(Vector3 velocity)
		{
			real = velocity;
		}

		public void Set(float x, float y)
		{
			real.Set(x, y, 0f);
		}

		public void SetX(float velocity)
		{
			real.x = velocity;
		}

		public void SetY(float velocity)
		{
			real.y = velocity;
		}

		public void Add(Vector3 velocity)
		{
			real += velocity;
		}

		public void AddHorizontal(float velocity)
		{
			real.x += velocity;
		}

		public void AddVertical(float velocity)
		{
			real.y += velocity;
		}

		public void Stop()
		{
			real = excess = delta = Vector3.zero;
		}

		public void StopHorizontal()
		{
			real.x = excess.x = delta.x = 0f;
		}

		public void StopVertical()
		{
			real.y = excess.y = delta.y = 0f;
		}
	}
}