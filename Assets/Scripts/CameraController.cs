using UnityEngine;
using System;
using Assets.Update;

namespace Assets.GameLogic.Core
{
	[RequireComponent(typeof(Camera))]
	public class CameraController : MonoBehaviour, IUpdatable
	{

		[Serializable]
		private struct ContainingArea
		{
			public int left, right, bottom, top;

			public void Expand(int x, int y)
			{
				left -= x;
				right += x;
				bottom -= y;
				top += y;
			}
		}

		private const int Idle = 0;
		private const int SmoothMove = 1;
		private const int LockOn = 2;

		[SerializeField] private Transform targetTransform;
		[SerializeField] private Vector2 triggerExtends;
		[SerializeField] private float horizontalSmoothTime;
		[SerializeField] private float verticalSmoothTime;
		[SerializeField] private ContainingArea containingArea;

		new private Transform transform;
		private Action horizontalStateMachine;
		private Action verticalStateMachine;
		private Vector2 velocity = new Vector2();
		private Vector2 followDirection = new Vector2();
		private Vector3 targetPreviousPosition;
		private Vector3 stopPosition;
		private Vector3 realPosition;

		private void Awake()
		{
			transform = base.transform;
			realPosition = transform.position;
			transform.position = realPosition.Rounded();
			stopPosition = transform.position;

			// Shrink the containing area by half size of the camera view
			var camera = GetComponent<Camera>();
			containingArea.Expand(Mathf.RoundToInt(-camera.orthographicSize * camera.aspect), Mathf.RoundToInt(-camera.orthographicSize));
		}

		private void Start()
		{
			horizontalStateMachine = HorizontalIdle;
			verticalStateMachine = VerticalIdle;

			if (targetTransform != null)
			{
				targetPreviousPosition = targetTransform.position;
			}
		}

		private void OnEnable()
		{
			UpdateManager.GeneralUpdater.Register(this);
		}

		private void OnDisable()
		{
			UpdateManager.GeneralUpdater.Deregister(this);
		}

		public void OnUpdate()
		{
			if (targetTransform != null)
			{
				horizontalStateMachine();
				verticalStateMachine();
			}
		}

		private Vector2 GetTargetPositionContained()
		{
			return new Vector2(Mathf.Clamp(targetTransform.position.x, containingArea.left, containingArea.right),
					Mathf.Clamp(targetTransform.position.y, containingArea.bottom, containingArea.top));
		}

		private void SetPositionX(float x)
		{
			var position = transform.position;
			position.x = Mathf.Round(x);
			transform.position = position;
		}

		private void SetPositionY(float y)
		{
			var position = transform.position;
			position.y = Mathf.Round(y);
			transform.position = position;
		}

		#region Horizontal
		private void HorizontalIdle()
		{
			if (Mathf.Abs(stopPosition.x - realPosition.x) > 0.01f)
			{
				realPosition.x = Mathf.SmoothDamp(realPosition.x, stopPosition.x, ref velocity.x, horizontalSmoothTime);
				SetPositionX(realPosition.x);
			}

			var targetPosition = GetTargetPositionContained();
			
			if (Mathf.Abs(targetTransform.position.x - realPosition.x) >= triggerExtends.x)
			{
				followDirection.x = System.Math.Sign(targetPosition.x - realPosition.x);
				targetPreviousPosition.x = targetPosition.x;
				horizontalStateMachine = HorizontalSmoothMove;
			}
		}

		private void HorizontalSmoothMove()
		{
			var targetPosition = GetTargetPositionContained();
			float targetDelta = targetPosition.x - targetPreviousPosition.x;

			if (System.Math.Sign(targetDelta) == -followDirection.x &&
					Mathf.Abs(targetDelta) > 0.01f)
			{
				stopPosition.x = Mathf.Round(realPosition.x + (targetPosition.x - realPosition.x) * 0.3f);
				horizontalStateMachine = HorizontalIdle;
				return;
			}

			realPosition.x = Mathf.MoveTowards(realPosition.x, targetPosition.x, Mathf.Abs(targetPosition.x - targetPreviousPosition.x));
			realPosition.x = Mathf.SmoothDamp(realPosition.x, targetPosition.x, ref velocity.x, horizontalSmoothTime);
			SetPositionX(realPosition.x);

			targetPreviousPosition.x = targetPosition.x;

			if (Mathf.Abs(realPosition.x - targetPreviousPosition.x) <= 0.01f)
			{
				horizontalStateMachine = HorizontalLockOn;
			}
		}

		private void HorizontalLockOn()
		{
			var targetPosition = GetTargetPositionContained();
			float targetDelta = targetPosition.x - targetPreviousPosition.x;

			if (System.Math.Sign(targetDelta) == -followDirection.x)
			{
				if (Mathf.Abs(targetDelta) > 0.01f)
				{
					stopPosition.x = Mathf.Round(realPosition.x + (targetPosition.x - realPosition.x) * 0.3f);
					horizontalStateMachine = HorizontalIdle;
				}
			}
			else if (targetPosition.x != containingArea.left && targetPosition.x != containingArea.right)
			{
				realPosition.x = targetPosition.x;
				targetPreviousPosition.x = targetPosition.x;
				SetPositionX(realPosition.x);
			}
		}
		#endregion

		#region Vertical
		private void VerticalIdle()
		{
			if (Mathf.Abs(stopPosition.y - realPosition.y) > 0.01f)
			{
				realPosition.y = Mathf.SmoothDamp(realPosition.y, stopPosition.y, ref velocity.y, verticalSmoothTime);
				SetPositionY(realPosition.y);
			}

			var targetPosition = GetTargetPositionContained();

			if (Mathf.Abs(targetTransform.position.y - realPosition.y) >= triggerExtends.y)
			{
				followDirection.y = System.Math.Sign(targetPosition.y - realPosition.y);
				targetPreviousPosition.y = targetPosition.y;
				verticalStateMachine = VerticalSmoothMove;
			}
		}

		private void VerticalSmoothMove()
		{
			var targetPosition = GetTargetPositionContained();
			float targetDelta = targetPosition.y - targetPreviousPosition.y;

			if (System.Math.Sign(targetDelta) == -followDirection.y &&
					Mathf.Abs(targetDelta) > 0.01f)
			{
				stopPosition.y = Mathf.Round(realPosition.y + (targetPosition.y - realPosition.y) * 0.3f);
				verticalStateMachine = VerticalIdle;
				return;
			}

			realPosition.y = Mathf.MoveTowards(realPosition.y, targetPosition.y, Mathf.Abs(targetPosition.y - targetPreviousPosition.y));
			realPosition.y = Mathf.SmoothDamp(realPosition.y, targetPosition.y, ref velocity.y, verticalSmoothTime);
			SetPositionY(realPosition.y);

			targetPreviousPosition.y = targetPosition.y;

			if (Mathf.Abs(realPosition.y - targetPreviousPosition.y) <= 0.01f)
			{
				verticalStateMachine = VerticalLockOn;
			}
		}

		private void VerticalLockOn()
		{
			var targetPosition = GetTargetPositionContained();
			float targetDelta = targetPosition.y - targetPreviousPosition.y;

			if (System.Math.Sign(targetDelta) == -followDirection.y)
			{
				if (Mathf.Abs(targetDelta) > 0.01f)
				{
					stopPosition.y = Mathf.Round(realPosition.y + (targetPosition.y - realPosition.y) * 0.3f);
					verticalStateMachine = VerticalIdle;
				}
			}
			else if (targetPosition.y != containingArea.bottom && targetPosition.y != containingArea.top)
			{
				realPosition.y = targetPosition.y;
				targetPreviousPosition.y = targetPosition.y;
				SetPositionY(realPosition.y);
			}
		}
		#endregion

		#region Gizmos
#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			Vector2 position = GetComponent<Transform>().position;

			Gizmos.color = new Color(0, 1, 0, 0.3f);
			Gizmos.DrawCube(position, triggerExtends * 2f);

			Gizmos.color = Color.magenta;
			Gizmos.DrawSphere(position, 0.06f);

			Gizmos.color = Color.cyan;
			Gizmos.DrawWireCube(new Vector3((containingArea.left + containingArea.right) / 2f, (containingArea.top + containingArea.bottom) / 2f),
					new Vector3(containingArea.right - containingArea.left, containingArea.top - containingArea.bottom));
		}
#endif
		#endregion
	}
}
