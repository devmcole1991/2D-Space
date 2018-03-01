using UnityEngine;
using UnityEngine.Events;

namespace Assets.GameLogic.Core
{
	public class Pickup : MonoBehaviour
	{
		[SerializeField] private ItemBase item;
		[SerializeField] private UnityEvent PickedUp;

		private void OnTriggerEnter2D(Collider2D other)
		{
			item.Use(other.gameObject);
			PickedUp.Invoke();
		}
	}
}
