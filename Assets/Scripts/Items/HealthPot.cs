﻿using UnityEngine;

namespace Assets.GameLogic.Core
{
	[CreateAssetMenu]
	public class HealthPot : ItemBase
	{
		[SerializeField] private int healAmount = 1;

		public int HealAmount { get { return healAmount; } }

		public override bool Use(GameObject target)
		{
			var health = target.GetComponent<Health>();

			if (health && !health.IsFull)
			{
				health.Heal(healAmount);
				return true;
			}

			return false;
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			if (healAmount < 1)
			{
				healAmount = 1;
			}
		}
#endif
	}
}