using UnityEngine;
using UnityEngine.Events;

namespace Assets.UnityEventTrigger
{
	public abstract class UnityEventTriggerBase : MonoBehaviour
	{
		[System.Serializable]
		protected class UnityEventTriggerEvent : UnityEvent<GameObject> { }
		[SerializeField] protected UnityEventTriggerEvent Triggered;
	}
}