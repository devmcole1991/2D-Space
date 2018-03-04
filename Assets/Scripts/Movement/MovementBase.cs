using UnityEngine;
using Assets.Update;

namespace Assets.GameLogic.Core
{
	[DisallowMultipleComponent]
	public abstract class MovementBase : MonoBehaviour, IUpdatable
	{
		new protected Transform transform;

		public Vector3 PreviousPosition { get; protected set; }

		protected virtual void Awake()
		{
			transform = base.transform;
			PreviousPosition = transform.position;
		}

		protected abstract void Move();

		public virtual void OnUpdate()
		{
			PreviousPosition = transform.position;
			Move();
		}
	}
}
