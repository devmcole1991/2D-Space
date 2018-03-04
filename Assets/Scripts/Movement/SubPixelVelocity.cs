using UnityEngine;

namespace Assets.GameLogic.Core
{
	public struct SubPixelVelocity
	{
		public Vector3 real;
		private Vector3 delta;
		public Vector3 excess;

		public Vector3 Delta { get { return delta; } }

		public void Reset()
		{
			real = delta = excess = Vector3.zero;
		}

		public void ResetX()
		{
			real.x = delta.x = excess.x = 0f;
		}

		public void ResetY()
		{
			real.y = delta.y = excess.y = 0f;
		}

		public void Update()
		{
			excess += real;
			delta.Set((int)excess.x, (int)excess.y, 0f);
			excess -= delta;
		}
	}
}
