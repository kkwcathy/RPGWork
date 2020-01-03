using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float speed;

	public List<GameObject> wayPoints;

    // Start is called before the first frame update
    void Start()
    {
		
	}

	private void MovePoint()
	{
		
	}

	void Update()
    {
		transform.Translate(wayPoints[0].transform.position * Time.deltaTime * speed);
		Debug.Log(transform.position);
	}
}
