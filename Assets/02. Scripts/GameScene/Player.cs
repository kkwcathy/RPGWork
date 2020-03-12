using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Character
{
	public float dodgeSpeed = 5.0f;

	[SerializeField] private float dodgeDistance = -5.0f;

	public Vector3 curDesPos;

	Vector3 targetPos;

	public GameObject enemyGroup;
    public Enemy[] enemies;

    CharacterController characterController;
	

	private void Awake()
	{
		StartDo();
		_animator = GetComponentInChildren<Animator>();
		PlayAnimation("Run");
	}


	void Start()
	{
		//characterController = GetComponent<CharacterController>();
		
		curDesPos = transform.position + (Vector3.left * 20 + Vector3.back * 20);
		// 시작할 땐 첫 웨이브 전까지 ↙ 방향으로 이동
		navMeshAgent.SetDestination(curDesPos);
		//navMeshAgent.SetDestination(wayPoints[0].transform.position);

	}



	public void Dodge()
	{
		targetPos = transform.position + (transform.forward * -dodgeDistance);

       // isLerpMoving = true;
    }

    // 테스트 전용
    public void Pause()
	{
		if (!navMeshAgent.isStopped)
		{
			navMeshAgent.isStopped = true;
		}
		else
		{
			navMeshAgent.isStopped = false;
			navMeshAgent.SetDestination(curDesPos);
		}
	}

	void Update()
	{
		UpdateDo();
	}
}
