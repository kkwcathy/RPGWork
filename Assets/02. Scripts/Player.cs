using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
	[SerializeField] private float speed;


	NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
		navMeshAgent = GetComponent<NavMeshAgent>();

		// 시작할 땐 첫 웨이브 전까지 ↙ 방향으로 이동
		navMeshAgent.SetDestination(transform.position + (Vector3.left * 20 + Vector3.back * 20));
		//navMeshAgent.SetDestination(wayPoints[0].transform.position);

	}

	void Update()
    {

		//transform.position = Vector3.Lerp(, wayPoints[0].transform.position, Time.deltaTime * speed);
		//Debug.Log(transform.position);
	}
}
