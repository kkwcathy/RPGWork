
// 적 상태 제어 클래스
public class EnemyAI : CharacterAI
{
	public override void Init()
	{
		base.Init();

		// 적은 타겟이 없을 때(생성 직후) 대기 상태로 들어감
		_charStateDic.Add(Character.eStateType.NoTarget, new IdleState(_character));
	}
}
