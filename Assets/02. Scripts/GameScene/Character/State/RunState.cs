
// 타겟 쫓기 상태 클래스
public class RunState : StateBase
{
	public RunState(Character character) : base(character)
	{

	}

	public override void StartState()
	{
		_character.PlayAnimation("Run");
	}

	public override void UpdateState()
	{
		// 타겟 탐색
		_character.SearchTarget();

		// 타겟 없을 경우 타겟 없음 상태로 변환
		if (!_character.CheckTargetExist())
		{
			_character.ChangeState(Character.eStateType.NoTarget);
		}
		// 타겟에게 공격이 가능할 만큼 가까워지면 움직임을 멈춤
		else if (_character.CheckTargetDistance(_character.FightDistance))
		{
			_character.StopMove();

			// 타겟이 시야에 있을 경우 공격 시작
			if (_character.IsTargetInSight())
			{
				_character.ChangeState(Character.eStateType.Fight);
			}
		}
		else
		{
			_character.BeginMove();
		}
	}
}
