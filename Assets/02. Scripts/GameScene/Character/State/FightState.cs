using UnityEngine;

// 캐릭터 싸움 상태 클래스
public class FightState : StateBase
{
	private float _elapsedTime = 0.0f;

	[SerializeField] private float _fireTime = 0.1f; // 애니메이션 시작 후 이펙트 발사까지 소요되는 시간

	private bool _isFired = false; // 이펙트가 계속 발사되지 않기 위해 제어하는 변수
	private bool _isStart = true; // true 일때만 애니메이션 실행

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
		if (!_character.CheckTargetExist())
		{
			_character.ChangeState(Character.eStateType.NoTarget);
		}
		// 타겟이 공격 가능 거리 바깥에 있으면 타겟 쫓기 상태로 변환
		else if (!_character.CheckTargetDistance(_character.FightDistance))
		{
			_character.ChangeState(Character.eStateType.RunToTarget);
		}
		else
		{
			_character.BasicAttack();
		}
	}
}
