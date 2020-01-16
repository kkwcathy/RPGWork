using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

	public Transform playerFocus;

	Transform target;

	public float moveDamping = 15.0f;
	//public float rotateDamping = 10.0f;
	private float xDistance;
	private float zDistance;

	private float height;

	private Transform tr;

	bool isTargetChange = false;
	bool isEnemyFocus = false;
	
	// Start is called before the first frame update
	void Start()
	{
		target = playerFocus;

		tr = GetComponent<Transform>();

		xDistance = tr.position.x;
		zDistance = tr.position.z;
		height = tr.position.y;
	}

	private void LateUpdate()
	{
		Vector3 followPos = target.position + (Vector3.forward * zDistance) + (Vector3.right * xDistance);
		followPos.y = height; 
		tr.position = Vector3.Lerp(tr.position, followPos, Time.deltaTime * moveDamping);
	
	}
	public void ChangeTarget(Transform change)
	{
		StartCoroutine(FocusEnemy(change));
	}

	public IEnumerator FocusEnemy(Transform change)
	{

		target = change;
		yield return new WaitForSeconds(1.5f);

		target = playerFocus;
		isEnemyFocus = false;
	}
}
