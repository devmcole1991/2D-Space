using UnityEngine;

namespace Assets.GameLogic.Core
{
	public static class ClassExtensions
	{
		public static Vector3 Rounded(this Vector3 vector)
		{
			return new Vector3(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));
		}
	}
}