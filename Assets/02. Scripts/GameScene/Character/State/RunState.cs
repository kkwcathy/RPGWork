
// 타겟 쫓기 상태 클래스
public class RunState : StateBase
{
	float _minDistance = 1.5f;

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
			_character.ChangeState(StateType.NoTarget);
		}
		// 타겟에게 공격이 가능할 만큼 가까워지면 움직임을 멈춤
		else if(_character.Attack != null)
		{
			_character.StopMove();
			_character.ChangeState(StateType.Fight);
		}
		// 타겟과 충분히 가까워지면 움직임을 멈춤
		else if(_character.CheckTargetDistance(_minDistance))
		{
			_character.StopMove();
		}
		else
		{
			_character.BeginMove();
		}
	}
}
