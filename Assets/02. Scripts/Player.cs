using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
	[SerializeField] private float speed = 15.0f;

	[SerializeField] private float dodgeDistance = -5.0f;
	public Vector3 curDesPos;

	Vector3 startPos;
	Vector3 endPos;
	float moveStartTime;

	private bool isLerpMoving = false;

	public bool IsChange
	{
		get
		{
			return !EnemyGenerator.isClear && EnemyGenerator.curWayPoint.position != curDesPos;
		}
	}

	NavMeshAgent navMeshAgent;

	private void Awake()
	{
		//IsChange = false;
		curDesPos = transform.position + (Vector3.left * 20 + Vector3.back * 20);
	}


	void Start()
    {
		navMeshAgent = GetComponent<NavMeshAgent>();

		// 시작할 땐 첫 웨이브 전까지 ↙ 방향으로 이동
		navMeshAgent.SetDestination(curDesPos);
		//navMeshAgent.SetDestination(wayPoints[0].transform.position);

	}
	
	public void ChangeDestination()
	{
		curDesPos = EnemyGenerator.curWayPoint.position;

		if (navMeshAgent.enabled)
		{
			navMeshAgent.SetDestination(curDesPos);
		}
	}

	public void Dodge()
	{
		navMeshAgent.enabled = false;

		startPos = transform.position;
		endPos = transform.forward * dodgeDistance;
		moveStartTime = Time.time;

		isLerpMoving = true;
	}

	void Update()
    {
		if (IsChange)
		{
			ChangeDestination();
		}
	}

	private void FixedUpdate()
	{
		if (isLerpMoving)
		{
			Vector3 changePos = transform.position;

			if(Utility.LerpMove(ref changePos, startPos, endPos, moveStartTime))
			{
				transform.position = changePos;
			}
			else
			{
				isLerpMoving = false;
				navMeshAgent.enabled = true;
				navMeshAgent.SetDestination(curDesPos);
			}
		}
	}
}
