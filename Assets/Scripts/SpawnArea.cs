using UnityEngine;

namespace Assets.GameLogic.Core
{
	public class SpawnArea : MonoBehaviour
	{
		[SerializeField] private ProbabilitySpawner spawner;
		[SerializeField] private Area area;

		private void Awake()
		{
			if (spawner == null)
			{
				Debug.LogWarning(name + " was not assigned a spawner.");
				spawner = ScriptableObject.CreateInstance<ProbabilitySpawner>();
				spawner.name = "EmptySpawner";
			}
		}

		public void Spawn(int count)
		{
			var position = area.isAbsolute ? Vector2.zero : (Vector2)transform.position;
			var min = position + new Vector2(area.xMin, area.yMin);
			var max = position + new Vector2(area.xMax, area.yMax);
			var spawnPoint = new Vector2(); 

			for (int i = 0; i < count; ++i)
			{
				spawnPoint.Set(Random.Range((int)min.x, (int)max.x), Random.Range((int)min.y, (int)max.y));
				spawner.Spawn(spawnPoint);
			}
		}

#if UNITY_EDITOR
		private void OnDrawGizmosSelected()
		{
			var position = area.isAbsolute ? Vector3.zero : GetComponent<Transform>().position;
			var tl = position + new Vector3(area.xMin, area.yMax);
			var tr = position + new Vector3(area.xMax, area.yMax);
			var bl = position + new Vector3(area.xMin, area.yMin);
			var br = position + new Vector3(area.xMax, area.yMin);

			Gizmos.color = Color.green;
			Gizmos.DrawLine(tl, tr);
			Gizmos.DrawLine(tr, br);
			Gizmos.DrawLine(br, bl);
			Gizmos.DrawLine(bl, tl);
		}
#endif
	}

	[System.Serializable]
	public struct Area
	{
		public bool isAbsolute;
		public int xMin;
		public int xMax;
		public int yMin;
		public int yMax;
	}
}