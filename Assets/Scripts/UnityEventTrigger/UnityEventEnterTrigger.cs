using UnityEngine;

namespace Assets.UnityEventTrigger
{
	public class UnityEventEnterTrigger : UnityEventTriggerBase
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			Triggered.Invoke(other.gameObject);
		}
	}
}