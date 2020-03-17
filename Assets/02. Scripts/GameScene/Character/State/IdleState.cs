
// 대기 상태 클래스
public class IdleState : StateBase
{
	public IdleState(Character character) : base(character)
	{

	}

	public override void StartState()
	{
		_character.PlayAnimation("Idle");
	}

	public override void UpdateState()
	{
		// 타겟이 잡힐 경우 타겟 쫓기 상태로 변환
		if (_character.CheckTargetExist())
		{
			_character.ChangeState(Character.eStateType.RunToTarget);
		}
	}
}
