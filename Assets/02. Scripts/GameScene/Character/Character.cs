using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{


	public StateType _stateType = StateType.NoTarget;

	public Transform tr;

	// 움직임 관련
	private CharacterController _charController;
	private float _rotateSpeed = 5.0f; // 회전 속도

	// 공격 관련
	[SerializeField] private CharacterAttack _charAttack = null;
	public AttackBase Attack = null;

	// 상태 관련
	private List<Character> _targetList = new List<Character>(); // 현재 타겟 리스트 (플레이어면 적, 적이면 플레이어)
	public Vector3 _explorePoint; // 현재 wave의 way point
	private Character _target = null;

	private NavMeshAgent _agent;

	[SerializeField] private CharType _charType = CharType.Enemy;
	private CharacterAI _charAI;

	[SerializeField] protected Animator _animator;

	private void Awake()
	{
		tr = GetComponent<Transform>();
	}

	private void Start()
	{
		_charAI.Init();
		_charAttack.Init();
	}

	private void Update()
	{
		UpdateDo();
	}

	public void BuildCharSetting(CharacterInfo charInfo)
	{
		_agent = GetComponent<NavMeshAgent>();
		_animator = GetComponentInChildren<Animator>();
		_charController = GetComponentInChildren<CharacterController>();

		_charAI = charInfo.charAI;
		_charAttack = charInfo.charAttack;

		_charAI.SetCharacter(this);
		_charAttack.SetCharacter(this, charInfo.power);

		GetComponent<CharacterDamage>().SetDamageStat(charInfo.maxHp, charInfo.defence);
	}
	
	public CharType GetCharType()
	{
		return _charType;
	}

	public void SetExplorePoint(Vector3 point)
	{
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

	public void UpdateDo()
	{
		// 죽거나 클리어 상태가 아닐 시 상태 업데이트 지속
		if (_stateType == StateType.Clear)
		{
			return;
		}

		_charAI.CheckState(_stateType);

		if(_stateType == StateType.Death)
		{
			return;
		}

		if (_target != null)
		{
			RotateToTarget();
		}

		if(_target!= null || Attack != null)
		{
			_charAttack.UpdateAttack();
		}
		
		_charAttack._AddTime();
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

	// 애니메이션 실행
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

	public void ChangeState(StateType state)
	{
		_stateType = state;
		_charAI.SwitchState(state);
	}

	public StateType GetStateType()
	{
		return _stateType;
	}

	// 타겟 리스트 조정
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

	// 스킬 이펙트 생성
	public GameObject Fire(GameObject prefab)
	{
		GameObject effect = Instantiate(prefab);

		// 스킬 이펙트의 레이어를 발사한 캐릭터의 레이어로 설정
		effect.layer = gameObject.layer;

		return effect;
	}

	public void Die()
	{
		Destroy(gameObject);
	}

	public void Clear()
	{
		ChangeState(StateType.Clear);
	}
}
