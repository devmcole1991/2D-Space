using UnityEngine;
using Assets.SOVariables;

namespace Assets.GameLogic.Core
{
	public class DestroyOnAwake : MonoBehaviour
	{
		[SerializeField] private ScriptableBool shouldBeDestroyed;

		private void Awake()
		{
			if (shouldBeDestroyed.Value)
			{
				Destroy(gameObject);
			}
		}
	}
}
