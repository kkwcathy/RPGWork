using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
	[SerializeField] private float speed = 15.0f;

	[SerializeField] private float DodgeDistance = -5.0f;
	public Vector3 curDesPos;

	Vector3 prevPos;

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
		prevPos = transform.forward;
	}

	void Update()
    {
		if (IsChange)
		{
			ChangeDestination();
		}

		if (!navMeshAgent.enabled)
		{
			transform.position = Vector3.Lerp(transform.position, prevPos * DodgeDistance, speed * Time.deltaTime);

			if(transform.position.Equals(prevPos * DodgeDistance))
			{
				navMeshAgent.enabled = true;
				navMeshAgent.SetDestination(curDesPos);
			}
		}

	}
}
