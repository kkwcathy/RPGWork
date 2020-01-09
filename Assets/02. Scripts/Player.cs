using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
	[SerializeField] private float speed;

	public List<GameObject> wayPoints;

	NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
		navMeshAgent = GetComponent<NavMeshAgent>();
		navMeshAgent.speed = 10;
		navMeshAgent.SetDestination(wayPoints[0].transform.position);
	}

	private void MovePoint()
	{
		
	}

	void Update()
    {
		MovePoint();
		
		//transform.position = Vector3.Lerp(, wayPoints[0].transform.position, Time.deltaTime * speed);
		//Debug.Log(transform.position);
	}
}
