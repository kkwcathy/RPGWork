using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
	public Transform player;
	Transform target;
	public Transform change;
	
	public float moveDamping = 15.0f;
	//public float rotateDamping = 10.0f;
	private float xDistance;
	private float zDistance;

	private float height;

	private Transform tr;

	private bool isChanged = false;

	// Start is called before the first frame update
	void Start()
	{
		target = player;
		tr = GetComponent<Transform>();

		xDistance = tr.position.x;
		zDistance = tr.position.z;
		height = tr.position.y;
	}

	private void LateUpdate()
	{
		if (!isChanged && EnemyGenerator.isGenerated)
		{
			StartCoroutine(ChangeTarget());
			isChanged = true;
		}


		Vector3 followPos = target.position + (Vector3.forward * zDistance) + (Vector3.right * xDistance);
		followPos.y = height; 
		tr.position = Vector3.Lerp(tr.position, followPos, Time.deltaTime * moveDamping);
	
	}

	IEnumerator ChangeTarget()
	{
		target = change;
		yield return new WaitForSeconds(1.0f);

		target = player;
	}
}
