using UnityEngine;

namespace Assets.GameLogic.Core
{
	[CreateAssetMenu]
	public class IntegerValue : ScriptableObject
	{
		[SerializeField] private int value;

		public int Value { get { return value; } set { this.value = value; } }
	}
}
