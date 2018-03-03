using UnityEngine;
using UnityEngine.Events;
using Assets.SOVariables;

namespace Assets.GameLogic.Core
{
	public class Health : MonoBehaviour
	{
		[SerializeField] private IntReference health;
		[SerializeField] private IntReference maxHealth;
		[SerializeField] private UnityEvent Depleted;

		public bool IsFull { get { return (health.Value >= maxHealth.Value); } }

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

#if UNITY_EDITOR
		private void OnValidate()
		{
			if (!maxHealth.EDITOR_IsNull)
			{
				if (maxHealth.Value < 1)
				{
					maxHealth.Value = 1;
				}

				if (!health.EDITOR_IsNull && health.Value > maxHealth.Value)
				{
					health.Value = maxHealth.Value;
				}
			}

			if (!health.EDITOR_IsNull && health.Value < 1)
			{
				health.Value = 1;
			}
		}
#endif
	}
}
