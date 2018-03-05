using UnityEngine;
using Assets.Update;
using Assets.GameLogic.Core;

namespace Assets.UI
{
	public abstract class UpdatableUI : MonoBehaviour, IUpdatable
	{
		protected void OnEnable()
		{
			UpdateManager.UiUpdater.Register(this);
		}

		protected void OnDisable()
		{
			UpdateManager.UiUpdater.Deregister(this);
		}

		public abstract void OnUpdate();
	}
}
