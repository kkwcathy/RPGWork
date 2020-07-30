// 캐릭터 상태 타입
public enum StateType
{
	NoTarget,
	RunToTarget,
	Fight,
	Death,
	Clear,
}

// 캐릭터 상태 상위 클래스
public class StateBase
{
	protected Character _character;

	public StateBase(Character character)
	{
		_character = character;
	}

	// 해당 상태로 변환될 시 한번 호출
	virtual public void StartState()
	{

	}

	// 함수 상태 업데이트
	virtual public void UpdateState()
	{
		
	}
}
