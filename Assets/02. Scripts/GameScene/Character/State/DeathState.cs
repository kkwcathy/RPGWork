
// 캐릭터 사망 상태 클래스
public class DeathState : StateBase
{
	public DeathState(Character character) : base(character)
	{
	}

	public override void StartState()
	{
		_character.PlayAnimation("Death");
	}

	public override void UpdateState()
	{
		// Death 애니메이션이 끝나면 캐릭터 오브젝트 파괴
		if (_character.IsAnimationFinished("Death"))
		{
			_character.Die();
		}
	}
}
