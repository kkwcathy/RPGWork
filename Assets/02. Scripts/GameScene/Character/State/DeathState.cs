
// 캐릭터 사망 상태 클래스
public class DeathState : StateBase
{
	public DeathState(Character character) : base(character)
	{
	}

	public override void StartState()
	{
		_character.Die();
	}
}
