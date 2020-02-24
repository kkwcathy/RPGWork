using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility 
{
	public static float spawnDelayTime = 0.8f;

	public static bool GetIsNear(Vector3 A, Vector3 B)
	{
		float distance =
			Mathf.Sqrt((A.x - B.x) * (A.x - B.x) + (A.z - B.z) * (A.z - B.z));

		if (distance < 0.1f)
		{
			return true;
		}

		return false;
	}

	//public static bool LerpMove(ref Vector3 curPos, 
	//	Vector3 endPos, 
	//	float moveDamping)
	//{
	//	curPos = Vector3.Lerp(curPos, endPos, Time.deltaTime * moveDamping);

	//	float distance =
	//		Mathf.Sqrt((curPos.x - endPos.x) * (curPos.x - endPos.x) + (curPos.z - endPos.z) * (curPos.z - endPos.z));

	//	if((int)distance == 0)
	//	{
	//		return false;
	//	}

	//	return true;
	//}
}
