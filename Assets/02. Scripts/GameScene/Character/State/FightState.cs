
// 캐릭터 싸움 상태 클래스
public class FightState : StateBase
{
	public FightState(Character character) : base(character)
	{
	}

	public override void StartState()
	{
		_character.StopMove();
	}

	public override void UpdateState()
	{
		// 발견되는 타겟이 존재하지 않으면 타겟 없음 상태로 변환
		if (!_character.CheckTargetExist() && _character.Attack == null)
		{
			_character.ChangeState(StateType.NoTarget);
		}
		// 타겟이 공격 가능 거리 바깥에 있으면 타겟 쫓기 상태로 변환
		else if(_character.Attack == null)
		{
			_character.ChangeState(StateType.RunToTarget);
		}
		else 
		{
			_character.Attack.RunAttack();
		}
	}
}
