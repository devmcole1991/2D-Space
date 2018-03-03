using UnityEngine;

namespace Assets.GameLogic.Core
{
	public abstract class ItemBase : ScriptableObject
	{
		public abstract bool Use(GameObject target);
	}
}
