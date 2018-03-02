namespace Assets.GameLogic.Core
{
	public interface IPlatformCharacterController
	{
		int Up { get; }
		int Down { get; }
		int Left { get; }
		int Right { get; }
		int JumpPressed { get; }
		int JumpHeld { get; }
        int ShootPressed { get; }
        int ShootHeld { get; }
    }
}
