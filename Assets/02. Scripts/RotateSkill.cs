using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkill : MonoBehaviour
{
	public Player player;
	public float fSpeed = 25;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		transform.parent.position = player.transform.position;
		Vector3 vAxis = player.transform.position;
		transform.RotateAround(vAxis,
								Vector3.up, Time.deltaTime * fSpeed);
	}
}
