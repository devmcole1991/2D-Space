using UnityEngine;

namespace Assets.GameLogic.Core
{
	[CreateAssetMenu]
	public class DamageItem : ItemBase
	{
		[SerializeField] private int damageAmount = 1;

		public int DamageAmount { get { return damageAmount; } }

		public override bool Use(GameObject target)
		{
			var health = target.GetComponent<Health>();

			if (health)
			{
				health.Damage(damageAmount);
				return true;
			}

			return false;
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			if (damageAmount < 1)
			{
				damageAmount = 1;
			}
		}
#endif
	}
}
