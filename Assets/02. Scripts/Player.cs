using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
	[SerializeField] private float speed;

	public Vector3 curDesPos;

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
		navMeshAgent.SetDestination(curDesPos);
	}

	void Update()
    {
		if (IsChange)
		{
			ChangeDestination();
		}
		
	}
}
