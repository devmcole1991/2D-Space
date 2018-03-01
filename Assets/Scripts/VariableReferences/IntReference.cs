using UnityEngine;

namespace Assets.VariableReferences
{
	[System.Serializable]
	public class IntReference
	{
		[SerializeField] private bool usePrimitive = true;
		[SerializeField] private int primitive;
		[SerializeField] private IntVariable reference;

		public int Value
		{
			get { return usePrimitive ? primitive : reference.value; }

			set
			{
				if (usePrimitive)
				{
					primitive = value;
				}
				else
				{
					reference.value = value;
				}
			}
		}
	}
}
