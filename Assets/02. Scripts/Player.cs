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
		navMeshAgent.SetDestination(wayPoints[0].transform.position);
	}

	void Update()
    {

		if (EnemyGenerator.isGenerated)
		{
			navMeshAgent.SetDestination(wayPoints[1].transform.position);
			Debug.Log("ddaf");
		}

		//transform.position = Vector3.Lerp(, wayPoints[0].transform.position, Time.deltaTime * speed);
		//Debug.Log(transform.position);
	}
}
