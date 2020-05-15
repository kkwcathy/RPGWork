
// 캐릭터 게임 클리어 상태 클래스
public class ClearState : StateBase
{
	public ClearState(Character character) : base(character)
	{
	}

	public override void StartState()
	{
		_character.StopMove();
		_character.PlayAnimation("Clear");
	}
}
