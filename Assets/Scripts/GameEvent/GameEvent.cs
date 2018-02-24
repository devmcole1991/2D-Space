using UnityEngine;
using System.Collections.Generic;

namespace Assets.Event
{
	[CreateAssetMenu]
	public class GameEvent : ScriptableObject
	{
		private List<GameEventListener> listeners = new List<GameEventListener>();

		private void Raise()
		{
			for (int i = listeners.Count - 1; i >= 0; --i)
			{
				listeners[i].OnEventRaised();
			}
		}

		public void Register(GameEventListener listener)
		{
			listeners.Add(listener);
		}

		public void Deregister(GameEventListener listener)
		{
			listeners.Remove(listener);
		}
	}
}