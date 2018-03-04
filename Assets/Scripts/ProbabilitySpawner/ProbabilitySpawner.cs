using UnityEngine;
using System.Collections.Generic;

namespace Assets.GameLogic.Core
{
	[CreateAssetMenu]
	public class ProbabilitySpawner : ScriptableObject
	{
		[System.Serializable]
		private struct ScoreGameObjectPair
		{
			[SerializeField] public int score;
			[SerializeField] public GameObject gameObject;
		}

		public const int MinScore = 1;
		public const int MaxScore = 10000;

		[SerializeField] private List<ScoreGameObjectPair> scoreGameObjectPairs;

		public GameObject Spawn(Vector2 position)
		{
			return Spawn(position, Quaternion.identity);
		}

		public GameObject Spawn(Vector2 position, Quaternion rotation)
		{
			int score = Random.Range(0, MaxScore);
			int count = scoreGameObjectPairs.Count;
			int low, high = 0;

			for (int i = 0; i < count; ++i)
			{
				var pair = scoreGameObjectPairs[i];

				low = high;
				high += pair.score;

				if (score >= low && score < high)
				{
					return Instantiate(pair.gameObject, position, rotation);
				}
			}

			return null;
		}
	}
}
