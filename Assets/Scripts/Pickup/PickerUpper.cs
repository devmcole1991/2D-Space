using UnityEngine;
using Assets.Update;

namespace Assets.GameLogic.Core
{
	// NOTE: Maybe use trigger instead of manual overlap check
	// Not sure which is more efficient, need to profile
	[RequireComponent(typeof(IPickerUpperController))]
	public class PickerUpper : MonoBehaviour, IUpdatable
	{
		[SerializeField] private LayerMask mask;
		[SerializeField] private BoundingBox bbox;
		[SerializeField][Tooltip("Can be left null if variable")] private ScriptablePickup closestPickup;
		private IPickerUpperController controller;

		public bool HasPermission { get { return controller.WantsToPickUp; } }

		private void Awake()
		{
			controller = GetComponent<IPickerUpperController>();
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
			closestPickup.Value = null;

			var position = (Vector2)transform.position;
			var pointA = position + new Vector2(bbox.LeftOffset, bbox.TopOffset);
			var pointB = position + new Vector2(bbox.RightOffset, bbox.BottomOffset);
			var overlaps = Physics2D.OverlapAreaAll(pointA, pointB, mask);

			int count = overlaps.Length;
			Pickup pickup;

			for (int i = 0; i < count; ++i)
			{
				pickup = overlaps[i].GetComponent<Pickup>();

				if (pickup)
				{
					if (!pickup.RequiresPermission)
					{
						pickup.PickUp(this);
					}
					else if (!closestPickup.Value || IsCloser(ref position, pickup, closestPickup.Value))
					{
						closestPickup.Value = pickup;
					}
				}
			}

			if (closestPickup.Value && closestPickup.Value.PickUp(this))
			{
				closestPickup.Value = null;
			}
		}

		private bool IsCloser(ref Vector2 position, Pickup challenger, Pickup current)
		{
			return (Vector2.Distance(position, challenger.transform.position) < Vector2.Distance(position, current.transform.position));
		}
	}
}
