using UnityEngine;

namespace Assets.UnityEventTrigger
{
	public class UnityEventExitTrigger : UnityEventTriggerBase
	{
		private void OnTriggerExit2D(Collider2D other)
		{
			Triggered.Invoke(other.gameObject);
		}
	}
}