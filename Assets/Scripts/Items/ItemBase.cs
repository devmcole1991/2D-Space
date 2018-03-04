using UnityEngine;

namespace Assets.GameLogic.Core
{
	public abstract class ItemBase : ScriptableObject
	{
		public abstract bool Use(GameObject target);

		public void UseWithNoFeedback(GameObject target)
		{
			Use(target);
		}
	}
}
