using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

	public Transform playerFocus;

	Transform target;

	public float moveDamping = 15.0f;

	private float xDistance;
	private float zDistance;

	private float _height;

	private Transform _tr;

	//bool isTargetChange = false;
	bool isEnemyFocus = false;

	Vector3 followPos;

	// Start is called before the first frame update
	void Start()
	{
		target = playerFocus;

		_tr = GetComponent<Transform>();

		xDistance = _tr.position.x;
		zDistance = _tr.position.z;
		_height = _tr.position.y;
	}

    public void ChangeDistance(int x, int y, int z)
    {
        xDistance += x;
        _height += y;
        zDistance += z;
    }
	private void LateUpdate()
	{
		followPos = target.position + (Vector3.forward * zDistance) + (Vector3.right * xDistance);
		followPos.y = _height;

		if (!isEnemyFocus)
		{
			_tr.position = followPos;
		}
		else
		{
			_tr.position = Vector3.Lerp(_tr.position, followPos, Time.deltaTime * moveDamping);

			if (Utility.GetIsNear(_tr.position, followPos) && target == playerFocus)
			{
				isEnemyFocus = false;
			}

		}
		
	}
	public void ChangeTarget(Transform change)
	{
		isEnemyFocus = true;

		StartCoroutine(FocusEnemy(change));
	}

	public IEnumerator FocusEnemy(Transform change)
	{

		target = change;
		yield return new WaitForSeconds(Utility.spawnDelayTime);

		target = playerFocus;
	}
}
