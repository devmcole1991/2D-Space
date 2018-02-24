using UnityEngine;
using UnityEngine.Events;

namespace Assets.Event
{
	public class GameEventListener : MonoBehaviour
	{
		[SerializeField] private GameEvent gameEvent;
		[SerializeField] private UnityEvent response;

		private void OnEnable()
		{
			gameEvent.Register(this);
		}

		private void OnDisable()
		{
			gameEvent.Deregister(this);
		}

		public void OnEventRaised()
		{
			response.Invoke();
		}
	}
}
