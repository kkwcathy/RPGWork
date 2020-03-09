using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI; // ★ 나중에 이미지 분리하면 삭제하기



public class Character : ObjBase
{
	// ★ 나중에 더 다듬기
	new public Transform transform;

	public GameObject charModel;
	protected GameObject model;
    
	protected Renderer objRenderer;

	protected bool isDamaged = false;

	public bool isDead = false;

	protected float navSpeed = 8.0f;
	protected float elapsedTime = 0;
	protected float damageEffectSpeed = 10.0f;

	private float stopDistance = 1.5f; // 타겟과의 거리가 이만큼 이하이면 멈춤
	private Vector3 tempVelocity = Vector3.zero;

	[SerializeField]
	protected Character targetObj = null;

    protected NavMeshAgent navMeshAgent;

    public Character godjiulguya = null;

    // 데미지 관련

    protected float hp = 100.0f;
    protected float initHp = 100.0f;

    public GameObject hpBarPrefab;
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    protected Canvas uiCanvas;
    protected Image hpBarImage;

    // 공격 관련

    [SerializeField] GameObject basicSkillEffect = null;

	// 상태 관련


    public void GenerateModel()
	{
		transform = GetComponent<Transform>();
		model = Instantiate(charModel, transform);

		bs.center = transform.position;

		bs.size = Vector3.one;
        objRenderer = GetComponentInChildren<Renderer>();
		navMeshAgent = GetComponent<NavMeshAgent>();
		navMeshAgent.speed = navSpeed;

	}

    public void ChangeDestination(List<Character> targetList)
    {
        for (int i = 0; i < targetList.Count; ++i)
        {
            if (targetObj == null
                || Vector3.Distance(targetObj.transform.position, transform.position) >
                   Vector3.Distance(targetList[i].transform.position, transform.position))
            {
                targetObj = targetList[i];
            }
        }

        if (targetObj != null)
        {
            godjiulguya = targetObj;
            navMeshAgent.SetDestination(targetObj.transform.position);
        }
    }

	// ★ 일단 플레이어에 hpbar 흔들리는거 보기 싫어서 몬스터만 띄울려고 virtual 로 했음
    virtual public void Damaged()
	{
		hp -= 10;
        

        if (!isDamaged)
		{
			isDamaged = true;
		}
	}

	public void Attack()
	{
        StartCoroutine(Attacking());
	}

    public void BasicSkillAttack()
    {
        GameObject skillEffect = Instantiate(basicSkillEffect, transform.position, transform.rotation);
        skillEffect.transform.parent = transform;

    }

    IEnumerator Attacking()
    {
        while(targetObj != null)
        {
            targetObj.Damaged();

            yield return new WaitForSeconds(1.0f);
        }
    }
    
	public void UpdateDo()
	{
		if (isDamaged)
		{
			Blink();
		}

		if (targetObj != null)
		{
			CheckDistance();
		}

		if (hp <= 0)
		{
			isDead = true;
			Destroy(gameObject);
		}
	}

	public float DistanceToTarget()
	{
		return Vector3.Distance(targetObj.transform.position, transform.position);
	}

	public void CheckDistance()
	{
		if(DistanceToTarget() < stopDistance)
		{
			navMeshAgent.velocity = Vector3.zero;
			navMeshAgent.isStopped = true;
		}
		else
		{
			if(navMeshAgent.isStopped)
			{
				navMeshAgent.velocity = tempVelocity;
				navMeshAgent.isStopped = false;
			}

			tempVelocity = navMeshAgent.velocity;
		}
	}

	public void Blink()
	{
		elapsedTime += Time.deltaTime * damageEffectSpeed;
		elapsedTime = Mathf.Clamp(elapsedTime, 0.0f, 2.0f);
		Color color = Color.Lerp(Color.black, Color.white, Mathf.PingPong(elapsedTime, 1));

		objRenderer.material.SetFloat("_R", color.r);
		objRenderer.material.SetFloat("_G", color.g);
        objRenderer.material.SetFloat("_B", color.b);

		if (elapsedTime >= 2.0f)
		{
			elapsedTime = 0.0f;
			isDamaged = false;
		}
	}
}
