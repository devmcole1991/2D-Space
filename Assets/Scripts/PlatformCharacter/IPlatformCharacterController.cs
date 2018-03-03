namespace Assets.GameLogic.Core
{
	public interface IPlatformCharacterController
	{
		int VerticalAxis { get; }
		int HorizontalAxis { get; }
		bool JumpPressed { get; }
		bool JumpHeld { get; }
	}
}
