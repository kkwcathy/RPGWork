using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
	// 캐릭터 타입
	public enum eCharType
	{
		Player,
		Enemy,
	}

	// 캐릭터 상태 타입
	public enum eStateType
	{
		NoTarget,
		RunToTarget,
		Fight,
		Death,
		Clear,
	}

	private eStateType _stateType = eStateType.NoTarget;

	public Transform tr;

	public CharacterInfo Charinfo;

	//private CharacterInfo _charInfo;

	//public CharacterInfo CharInfo
	//{
	//	get { return _charInfo; }
	//}

	//public void SetCharInfo(CharacterInfo charInfo)
	//{
	//	_charInfo = charInfo;
	//	//Debug.Log("character information set");
	//}

	// 움직임 관련

	private CharacterController _charController;

	private float _rotateSpeed = 5.0f; // 회전 속도
	private float _sightNormalized = 0.99f; // 타겟과 자신의 각도 차이의 정규화된 값이 이보다 크면 타겟이 시야에 들어감
	private float _fightDistance = 3.0f; // 타겟과의 거리가 이만큼 이하이면 멈춤

	public float FightDistance
	{
		get { return _fightDistance; }
		set { _fightDistance = value; }
	}

	// 공격 관련
	[SerializeField] private Transform _firePoint = null; // 스킬 이펙트 생성되는 부위
	[SerializeField] private CharacterAttack _attack = null; 

	// 상태 관련
	private List<Character> _targetList = new List<Character>(); // 현재 타겟 리스트 (플레이어면 적, 적이면 플레이어)
	public Vector3 _explorePoint; // 현재 wave의 way point
	private Character _target = null;

	private NavMeshAgent _agent;

	[SerializeField] private eCharType _charType = eCharType.Enemy;
	private CharacterAI _charAI;

	[SerializeField] protected Animator _animator;

	private void Update()
	{
		//UpdateDo();
	}

	private void Start()
	{
		//tr = GetComponent<Transform>(); : 원래 Awake에 해줘야 함
		
		_charAI.Init();
	}

	public void BuildCharSetting(CharacterInfo charInfo)
	{
		_agent = GetComponent<NavMeshAgent>();
		_animator = GetComponentInChildren<Animator>();
		_charController = GetComponentInChildren<CharacterController>();

		_charAI = charInfo.charAI;
		_attack = charInfo.charAttack;

		_charAI.SetCharacter(this);
		_attack.SetCharacter(this, charInfo.power);

		GetComponent<CharacterDamage>().SetDamageStat(charInfo.maxHp, charInfo.defence);

		Charinfo = charInfo; // 인스펙터 확인용 곧 지울거임!!
	}
	
	public eCharType GetCharType()
	{
		return _charType;
	}

	public void SetExplorePoint(Vector3 point)
	{
		Debug.Log(name + " set explore point : " + point);
		_explorePoint = point;
	}

	// 현재 way point로 탐험
	public void Explore()
	{
		_agent.SetDestination(_explorePoint);
	}

	// 타겟과의 거리 측정
	public bool CheckTargetDistance(float distance)
	{
		return Vector3.Distance(_target.tr.position,
				tr.position) < distance;
	}

	// 타겟 존재 여부
	public bool CheckTargetExist()
	{
		return _targetList.Count > 0;
	}

	// 타겟 리스트에서 거리가 가장 가까운 적을 찾아 타겟에 대입
	public void SearchTarget()
    {
        for (int i = 0; i < _targetList.Count; ++i)
        {
            if (_target == null
                || Vector3.Distance(_target.tr.position, tr.position) >
                   Vector3.Distance(_targetList[i].tr.position, tr.position))
            {
                _target = _targetList[i];
            }
        }

		if (_target != null)
		{
			_agent.SetDestination(_target.tr.position);
		}
    }

	// 기본 공격
	//public void BasicAttack()
	//{
	//	if(_attack.GetElapsedTime() == 0.0f)
	//	{
	//		PlayAnimation("Attack");
	//	}

	//	_attack.Fire(_firePoint);
	//}

	public void UpdateDo()
	{
		// 죽거나 클리어 상태가 아닐 시 상태 업데이트 지속
		//if(_stateType != eStateType.Death && _stateType != eStateType.Clear)
		if (_stateType != eStateType.Clear)
		{
			_charAI.CheckState(_stateType);
		}

		if (_target != null)
		{
			RotateToTarget();
		}

		//if (_charType == eCharType.Player)
		//{
		//	Debug.Log(name+_stateType);
		//}
	}

	// 움직임을 멈출 시 agent의 관성을 없애기 위해 멈추기 직전 velocity 값을 tempVelocity에 저장하고 velocity 값을 0으로 설정
	private Vector3 tempVelocity = Vector3.zero;

	public void StopMove()
	{
		tempVelocity = _agent.velocity;

		_agent.velocity = Vector3.zero;
		_agent.isStopped = true;
	}

	// 멈추기 직전 저장했던 속도 값을 다시 대입하여 움직임 재개
	public void BeginMove()
	{
		if (_agent.isStopped)
		{
			_agent.velocity = tempVelocity;
			_agent.isStopped = false;
		}
	}

	public void PlayAnimation(string trigger)
	{
		if(_animator != null)
		{
			_animator.SetTrigger(trigger);
		}
	}

	// 파라미터로 들어온 trigger와 연결된 애니메이션 재생 중인지 검사하는 함수
	public bool IsAnimationPlaying(string anim)
	{
		AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);

		return info.IsName(anim);
	}

	public bool IsAnimationFinished(string anim)
	{
		AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);

		return info.IsName(anim) && info.normalizedTime >= 1.0f;
	}

	// 타겟을 향한 회전 움직임 보정
	public void RotateToTarget()
	{
		Vector3 direction = _target.tr.position - tr.position;
		tr.rotation = Quaternion.Lerp(tr.rotation, Quaternion.LookRotation(direction), _rotateSpeed * Time.deltaTime);
	}

	// 타겟과의 각도 차이를 통해 시야 내부에 존재하는지 확인
	public bool IsTargetInSight()
	{
		Vector3 direction = (_target.tr.position - tr.position).normalized;

		return Vector3.Dot(tr.forward, direction) >= _sightNormalized;
	}

	public void ChangeState(eStateType state)
	{
		_stateType = state;
		_charAI.SwitchState(state);
	}

	public eStateType GetStateType()
	{
		return _stateType;
	}

	public void AddTarget(Character target)
	{
		if(target.GetCharType() != _charType)
		{
			_targetList.Add(target);
		}
	}

	public void DeleteTarget(Character target)
	{
		_targetList.Remove(target);

		// 삭제된 캐릭터가 현재 타겟이였을 경우 타겟을 비우고 다시 Search
		if(target == _target)
		{
			_target = null;
			SearchTarget();
		}
	}

	// 직선 방향 움직임 (주로 공격 시 사용)
	public void StraightMove(Vector3 moveDir, float speed)
	{
		_charController.Move(moveDir * Time.deltaTime * speed);
	}

	public void Die()
	{
		Destroy(gameObject);
	}

	public void Clear()
	{
		ChangeState(eStateType.Clear);
	}
}
