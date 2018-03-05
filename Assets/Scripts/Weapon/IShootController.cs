using UnityEngine;

namespace Assets.GameLogic.Core
{
    public interface IShootController
    {
        bool ShootPressed { get; }
        bool ShootHeld { get; }
    }
}
