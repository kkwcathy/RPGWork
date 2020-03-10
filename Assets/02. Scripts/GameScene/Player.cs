using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Character
{
	//[SerializeField] private float speed = 15.0f;

	public float dodgeSpeed = 5.0f;

	[SerializeField] private float dodgeDistance = -5.0f;
	public Vector3 curDesPos;

	Vector3 targetPos;

	//private bool isLerpMoving = false;

	public GameObject enemyGroup;
    public Enemy[] enemies;

    CharacterController characterController;
	

	private void Awake()
	{
		StartDo();
		//model.transform.parent = transform;
		_animator = GetComponentInChildren<Animator>();
		//IsChange = false
		curDesPos = transform.position + (Vector3.left * 20 + Vector3.back * 20);
	}


	void Start()
	{
		//characterController = GetComponent<CharacterController>();
		
		

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

	//public void CheckDamaged()
	//{
 //       enemies = enemyGroup.GetComponentsInChildren<Enemy>();

	//	foreach(var i in enemies)
	//	{
	//		if (bs.Intersects(i.bs))
	//		{
	//			targetObj = i;
	//		}
	//		else
	//		{
	//			targetObj = null;
	//		}
	//	}
	//}

	void Update()
	{
		UpdateDo();

		//bs.center = transform.position;

		//CheckDamaged();

		//if (isLerpMoving)
		//{
		//	Vector3 curPos = transform.position;

		//	characterController.Move((targetPos - curPos) * Time.deltaTime * dodgeSpeed);

		//	if (Utility.GetIsNear(curPos, targetPos))
		//	{
		//		isLerpMoving = false;
		//	}
		//}
	}
}
