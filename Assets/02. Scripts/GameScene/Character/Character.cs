using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Character : MonoBehaviour
{
	public enum CharType
	{
		Player,
		Enemy,
	}

	public enum StateType
	{
		NoTarget,
		RunToTarget,
		Fight,
		Death,
		Clear,
	}

	private StateType _stateType = StateType.NoTarget;

	private Transform _tr;
	public bool _isDead = false;

	// 움직임 관련
	private float rotateSpeed = 2.0f;

	// 공격 관련
	public Transform _firePoint;
	[SerializeField] private Attack _attack;

	// 상태 관련
	private List<Character> _targetList = new List<Character>();
	private Vector3 _explorePoint;
	private Character _target = null;

	private float stopDistance = 2.0f; // 타겟과의 거리가 이만큼 이하이면 멈춤
	
	private NavMeshAgent _agent;

	[SerializeField] private CharType _charType = CharType.Enemy;
	private CharacterAI _charAI;

	[SerializeField] protected Animator _animator;

	private void Awake()
	{
		_charAI = new CharacterFactory(this).GetCharAI();
	}

	private void Update()
	{
		UpdateDo();
	}

	private void Start()
	{
		_tr = GetComponent<Transform>();
		_animator = GetComponentInChildren<Animator>();
		_attack = GetComponent<Attack>();
		_agent = GetComponent<NavMeshAgent>();

		_charAI.Init();
	}

	public CharType GetCharType()
	{
		return _charType;
	}

	public void SetExplorePoint(Vector3 point)
	{
		_explorePoint = point;
	}

	public void Explore()
	{
		Debug.Log(_explorePoint);
		_agent.SetDestination(_explorePoint);
	}

	public bool CheckTargetDistance()
	{
		return Vector3.Distance(_target._tr.position,
				_tr.position) < stopDistance;
	}

	public bool CheckTargetExist()
	{
		return _targetList.Count > 0;
	}

	public void SearchTarget()
    {
        for (int i = 0; i < _targetList.Count; ++i)
        {
            if (_target == null
                || Vector3.Distance(_target._tr.position, _tr.position) >
                   Vector3.Distance(_targetList[i]._tr.position, _tr.position))
            {
                _target = _targetList[i];
            }
        }

		if (_target != null)
		{
			_agent.SetDestination(_target._tr.position);
		}
    }

	public void StartBaseAttack()
	{
        StartCoroutine(BaseAttack());
	}

    IEnumerator BaseAttack()
    {
        while(_target != null && _stateType == StateType.Fight)
        {
			PlayAnimation("Attack");

			_attack.Fire(_firePoint);
			yield return new WaitForSeconds(2.0f);
        }
    }

	public void UpdateDo()
	{
		_charAI.CheckState(_stateType);

		if (_target != null)
		{
			RotateToTarget();
		}
	}

	private Vector3 tempVelocity = Vector3.zero;

	public void StopMove()
	{
		tempVelocity = _agent.velocity;

		_agent.velocity = Vector3.zero;
		_agent.isStopped = true;
	}

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

	public bool IsAnimationPlaying(string anim)
	{
		AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);

		return info.IsName(anim) && _animator.IsInTransition(0);
	}

	public void RotateToTarget()
	{
		Vector3 dir = _target._tr.position - _tr.position;
		_tr.rotation = Quaternion.Lerp(_tr.rotation, Quaternion.LookRotation(dir), rotateSpeed * Time.deltaTime);
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

	public List<Character> GetTargetList()
	{
		return _targetList;
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

		if(target == _target)
		{
			_target = null;
			SearchTarget();
		}
	}

	public bool IsDead()
	{
		return _isDead;
	}
}
