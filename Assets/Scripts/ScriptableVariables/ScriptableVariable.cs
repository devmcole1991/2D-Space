using UnityEngine;

namespace Assets.SOVariables
{
	public abstract class ScriptableVariable<T> : ScriptableObject
	{
		[SerializeField] private T value;
		public T Value { get { return value; } set { this.value = value; } }
	}
}
