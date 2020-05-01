
// 플레이어 상태 제어 클래스
public class PlayerAI : CharacterAI
{
	public override void Init()
	{
		base.Init();

		// 플레이어는 타겟이 없을 때 way point를 찾아가는 탐험 상태로 들어감
		// 플레이어는 스테이지 클리어 시 실행되는 클리어 이벤트 실행하기 위해 클리어 상태 추가
		_charStateDic.Add(Character.eStateType.NoTarget, new ExploreState(_character));
		_charStateDic.Add(Character.eStateType.Clear, new ClearState(_character));

		// 스테이지 실행과 동시에 탐험 시작
		_character.ChangeState(Character.eStateType.NoTarget);
	}
}
