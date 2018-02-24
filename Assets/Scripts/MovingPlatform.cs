using UnityEngine;
using System.Collections.Generic;
using Assets.Update;

namespace Assets.GameLogic.Core
{
	[RequireComponent(typeof(SimulatePhysics))]
	public class MovingPlatform : MonoBehaviour, IUpdatable
	{
		private SimulatePhysics physics;
		private List<SimulatePhysics> passengers = new List<SimulatePhysics>();
		new private Transform transform;

		private void Awake()
		{
			physics = GetComponent<SimulatePhysics>();
			transform = base.transform;
		}

		private void OnEnable()
		{
			UpdateManager.MovingPlatformUpdater.Register(this);
		}

		private void OnDisable()
		{
			UpdateManager.MovingPlatformUpdater.Deregister(this);
		}

		private void MovePassengers()
		{
			int count = passengers.Count;
			var deltaPosition = transform.position - physics.PreviousPosition;
			var delta = new Vector3Int((int)deltaPosition.x, (int)deltaPosition.y, 0);
			SimulatePhysics passenger;

			for (int i = count - 1; i >= 0; --i)
			{
				passenger = passengers[i];
				passenger.Move(delta);
			}
		}

		public void AddPassenger(SimulatePhysics passenger)
		{
			passengers.Add(passenger);
		}

		public void RemovePassenger(SimulatePhysics passenger)
		{
			passengers.Remove(passenger);
		}

		public void OnUpdate()
		{
			MovePassengers();
		}
	}
}
