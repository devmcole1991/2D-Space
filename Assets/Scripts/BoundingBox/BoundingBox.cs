using UnityEngine;

namespace Assets.GameLogic.Core
{
	public class BoundingBox : MonoBehaviour
	{
		[SerializeField] private int topOffset;
		[SerializeField] private int bottomOffset;
		[SerializeField] private int rightOffset;
		[SerializeField] private int leftOffset;

		public int TopOffset { get { return topOffset; } }
		public int BottomOffset { get { return bottomOffset; } }
		public int RightOffset { get { return rightOffset; } }
		public int LeftOffset { get { return leftOffset; } }

#if UNITY_EDITOR
		private void OnValidate()
		{
			if (bottomOffset >= topOffset)
			{
				bottomOffset = topOffset - 1;
			}

			if (leftOffset >= rightOffset)
			{
				leftOffset = rightOffset - 1;
			}
		}

		private void OnDrawGizmosSelected()
		{
			var position = GetComponent<Transform>().position;
			var tl = position + new Vector3(leftOffset, topOffset);
			var tr = position + new Vector3(rightOffset, topOffset);
			var bl = position + new Vector3(leftOffset, bottomOffset);
			var br = position + new Vector3(rightOffset, bottomOffset);

			Gizmos.color = Color.green;
			Gizmos.DrawLine(tl, tr);
			Gizmos.DrawLine(tr, br);
			Gizmos.DrawLine(br, bl);
			Gizmos.DrawLine(bl, tl);
		}
#endif
	}
}
