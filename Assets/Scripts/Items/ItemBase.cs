using UnityEngine;

namespace Assets.GameLogic.Core
{
	public abstract class ItemBase : ScriptableObject
	{
		public abstract void Use(GameObject target);
	}
}
