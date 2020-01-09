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
	public float distance = 5.0f;
	public float height = 4.0f;

	public float targetOffset = 2.0f;

	private Transform tr;

	// Start is called before the first frame update
	void Start()
	{
		target = player;
		tr = GetComponent<Transform>();

		distance = tr.position.z;
		height = tr.position.y;
	}

	private void LateUpdate()
	{
		if (EnemyGenerator.isGenerated)
		{
			StartCoroutine(ChangeTarget());
			EnemyGenerator.isGenerated = false;
		}

		distance = target.Equals(player) ? -20 : 0;
		var camPos = target.position - (target.forward * distance) + (target.up * height);

		tr.position = Vector3.Slerp(tr.position, camPos, Time.deltaTime * moveDamping);

		//tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, Time.deltaTime * rotateDamping);

		//tr.LookAt(target.position + (target.up * targetOffset));

		Debug.Log(target.position);
	}

	IEnumerator ChangeTarget()
	{
		target = change;
		yield return new WaitForSeconds(1.5f);

		Debug.Log("change");
		target = player;
	}
}
