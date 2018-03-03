using UnityEngine;
using UnityEngine.Events;

namespace Assets.GameLogic.Core
{
	public class UnityEventTrigger : MonoBehaviour
	{
		[SerializeField] private UnityEvent Triggered;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			Triggered.Invoke();
		}
	}
}