using UnityEngine;
using UnityEngine.Events;
<<<<<<< HEAD
using System.Collections;
=======
>>>>>>> devin-changes
using Assets.SOVariables;

namespace Assets.GameLogic.Core
{
	public class Health : MonoBehaviour
	{
		[SerializeField] private IntReference health;
		[SerializeField] private IntReference maxHealth;
<<<<<<< HEAD
		[SerializeField] private IntReference immuneFrames;
		[SerializeField] private UnityEvent Healed;
		[SerializeField] private UnityEvent Damaged;
		[SerializeField] private UnityEvent Depleted;

		public bool IsImmune { get; private set; }
		public bool IsFull { get { return (health.Value >= maxHealth.Value); } }
		public int CurrentHealth { get { return health.Value; } }
		public int MaxHealth { get { return maxHealth.Value; } }

		private void Awake()
		{
			IsImmune = false;
		}
=======
		[SerializeField] private UnityEvent Depleted;

		public bool IsFull { get { return (health.Value >= maxHealth.Value); } }
>>>>>>> devin-changes

		public void Heal(int amount)
		{
			amount = amount < 0 ? 0 : amount;
			health.Value = Mathf.Min(health.Value + amount, maxHealth.Value);
<<<<<<< HEAD
			Healed.Invoke();
=======
>>>>>>> devin-changes
		}

		public void Damage(int amount)
		{
<<<<<<< HEAD
			if (amount > 0 && !IsImmune)
			{
				health.Value = Mathf.Max(health.Value - amount, 0);

				if (health.Value == 0)
				{
					Depleted.Invoke();
				}
				else
				{
					Damaged.Invoke();
					TriggerImmunity();
				}
			}
		}

		public void TriggerImmunity()
		{
			if (!IsImmune)
			{
				StartCoroutine(ImmunityRoutine(immuneFrames.Value));
			}
		}

		private IEnumerator ImmunityRoutine(int frames)
		{
			IsImmune = true;
			while (--frames > 0) yield return null;
			IsImmune = false;
		}

=======
			amount = amount < 0 ? 0 : amount;
			health.Value = Mathf.Max(health.Value - amount, 0);

			if (health.Value == 0)
			{
				Depleted.Invoke();
			}
		}

>>>>>>> devin-changes
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
