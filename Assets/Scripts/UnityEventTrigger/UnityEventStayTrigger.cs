using UnityEngine;

namespace Assets.UnityEventTrigger
{
	public class UnityEventStayTrigger : UnityEventTriggerBase
	{
		private void OnTriggerStay2D(Collider2D other)
		{
			Triggered.Invoke(other.gameObject);
		}
	}
}