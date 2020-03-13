using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Character : MonoBehaviour
{
	public enum CharType
	{
		Fire,
		Water,
	}

	public enum StateType
	{
		None,
		Idle,
		Explore,
		RunTowards,
		Fight,
		Death,
		Clear,
	}

	private StateType _stateType = StateType.Run;

	// ★ 나중에 더 다듬기
	new public Transform transform;

	protected Renderer _renderer;

	protected bool isDamaged = false;
	public bool isDead = false;

	// 움직임 관련
	protected float navSpeed = 8.0f;
	protected float damageEffectSpeed = 10.0f;
	private float rotateSpeed = 2.0f;

	public float dodgeSpeed = 5.0f;
	[SerializeField] private float dodgeDistance = -5.0f;
 
    // 공격 관련
    [SerializeField] GameObject basicSkillEffect = null;

	// 상태 관련
	private List<Character> _targetList = new List<Character>();
	private Vector3 _explorePoint;
	private Character _target = null;

	private float stopDistance = 1.5f; // 타겟과의 거리가 이만큼 이하이면 멈춤

	private Vector3 tempVelocity = Vector3.zero;

	private NavMeshAgent _agent;
	private CharacterAI _charAI;

	[SerializeField] protected Animator _animator;

	public void StartDo()
	{
		transform = GetComponent<Transform>();

		_renderer = GetComponentInChildren<Renderer>();
		_agent = GetComponent<NavMeshAgent>();
		_agent.speed = navSpeed;

		_charAI = new CharacterAI(this);
		_charAI.Init();
	}

	public void SetExplorePoint(Vector3 point)
	{
		_explorePoint = point;
	}

	public void Explore()
	{
		_agent.SetDestination(_explorePoint);
	}

	public void SearchTarget()
    {
        for (int i = 0; i < _targetList.Count; ++i)
        {
            if (_target == null
                || Vector3.Distance(_target.transform.position, transform.position) >
                   Vector3.Distance(_targetList[i].transform.position, transform.position))
            {
                _target = _targetList[i];
            }
        }

        if (_target != null)
        {
            _agent.SetDestination(_target.transform.position);
        }
    }

	// ★ 일단 플레이어에 hpbar 흔들리는거 보기 싫어서 몬스터만 띄울려고 virtual 로 했음
    virtual public void Damaged()
	{
		//hp -= 10;
        
        if (!isDamaged)
		{
			isDamaged = true;
		}
	}

	public void Attack()
	{
        StartCoroutine(BaseAttack());
	}

    public void BasicSkillAttack()
    {
        GameObject skillEffect = Instantiate(basicSkillEffect, transform.position, transform.rotation);
        skillEffect.transform.parent = transform;

    }

    IEnumerator BaseAttack()
    {
        while(_target != null && _stateType == StateType.Fight)
        {
			PlayAnimation("Attack");
			BasicSkillAttack();
			yield return new WaitForSeconds(2.0f);
        }
    }

	public void UpdateDo()
	{
		_charAI.CheckState(_stateType);

		if (isDamaged)
		{
			Blink();
		}

		if (_target != null)
		{
			RotateToTarget();
		}

		//if (hp <= 0)
		//{
		//	isDead = true;
		//	Destroy(gameObject);
		//}
		
		// ★ 정리 다 되면 적절한 곳에 넣기
		tempVelocity = _agent.velocity;
	}

	public bool IsAttackable()
	{
		if(_target != null && 
			Vector3.Distance(_target.transform.position, transform.position) 
			< stopDistance)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public void StopMove()
	{
		_agent.velocity = Vector3.zero;
		_agent.isStopped = true;
	}

	public void BeginMove()
	{
		_agent.velocity = tempVelocity;
		_agent.isStopped = false;
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
		Vector3 dir = _target.transform.position - transform.position;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotateSpeed * Time.deltaTime);
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

	float elapsedTime = 0;

	public void Blink()
	{
		elapsedTime += Time.deltaTime * damageEffectSpeed;
		elapsedTime = Mathf.Clamp(elapsedTime, 0.0f, 2.0f);
		Color color = Color.Lerp(Color.black, Color.white, Mathf.PingPong(elapsedTime, 1));

		_renderer.material.SetFloat("_R", color.r);
		_renderer.material.SetFloat("_G", color.g);
        _renderer.material.SetFloat("_B", color.b);

		if (elapsedTime >= 2.0f)
		{
			elapsedTime = 0.0f;
			isDamaged = false;
		}
	}

	//public void Dodge()
	//{
	//	targetPos = transform.position + (transform.forward * -dodgeDistance);

	//	// isLerpMoving = true;
	//}
}
