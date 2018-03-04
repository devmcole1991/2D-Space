using UnityEngine;

namespace Assets.SOVariables
{
	[System.Serializable]
	public abstract class VariableReference<T0, T1> : VariableReferenceBase
		where T1 : ScriptableVariable<T0>
	{ 
		[SerializeField] private bool useVariable = true;
		[SerializeField] private T0 variable;
		[SerializeField] private T1 scriptableVariable;

#if UNITY_EDITOR
		public bool EDITOR_IsNull { get { return useVariable ? false : !scriptableVariable; } }
#endif

		public T0 Value
		{
			get { return useVariable ? variable : scriptableVariable.Value; }

			set
			{
				if (useVariable)
				{
					variable = value;
				}
				else
				{
					scriptableVariable.Value = value;
				}
			}
		}
	}
}
