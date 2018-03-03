using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Assets.GameLogic.Core
{
	public class Pickup : MonoBehaviour
	{
		private const float ActivateDelay = 0.5f;

		[SerializeField] private bool requiresPermission;
		[SerializeField] private ItemBase item;
		[SerializeField] private UnityEvent PickedUp;

		public bool CanBePickedUp { get; private set; }
		public bool RequiresPermission { get { return requiresPermission; } }

		private void Awake()
		{
			StartCoroutine(ActivateRoutine());
		}

		private IEnumerator ActivateRoutine()
		{
			CanBePickedUp = false;
			yield return new WaitForSecondsRealtime(ActivateDelay);
			CanBePickedUp = true;
		}

		public bool PickUp(PickerUpper pickerUpper)
		{
			if (CanBePickedUp && (!requiresPermission || pickerUpper.HasPermission) &&
					item.Use(pickerUpper.gameObject))
			{
				PickedUp.Invoke();
				return true;
			}

			return false;
		}
	}
}
