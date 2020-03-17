
// 다음 Way Point를 찾아가는 탐험 상태 클래스
public class ExploreState : StateBase
{
	public ExploreState(Character character) : base(character)
	{
	}

	public override void StartState()
	{
		_character.Explore();
		_character.BeginMove();
	}

	public override void UpdateState()
	{
		if (!_character.IsAnimationPlaying("Run"))
		{
			_character.PlayAnimation("Run");
		}

		// 타겟이 잡힐 경우 타겟 쫓기 상태로 변환
		if(_character.CheckTargetExist())
		{
			_character.ChangeState(Character.eStateType.RunToTarget);
		}
	}

}
