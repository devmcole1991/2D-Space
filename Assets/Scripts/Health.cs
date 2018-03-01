using UnityEngine;
using UnityEngine.Events;
using Assets.VariableReferences;

namespace Assets.GameLogic.Core
{
	public class Health : MonoBehaviour
	{
		[SerializeField] private IntReference health;
		[SerializeField] private IntReference maxHealth;
		[SerializeField] private UnityEvent Depleted;

		public void Heal(int amount)
		{
			amount = amount < 0 ? 0 : amount;
			health.Value = Mathf.Min(health.Value + amount, maxHealth.Value);
		}

		public void Damage(int amount)
		{
			amount = amount < 0 ? 0 : amount;
			health.Value = Mathf.Max(health.Value - amount, 0);

			if (health.Value == 0)
			{
				Depleted.Invoke();
			}
		}
	}
}
