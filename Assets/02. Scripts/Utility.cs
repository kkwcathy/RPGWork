using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility 
{
	static float lerpTotalTime = 1.0f;

	public static bool LerpMove(ref Vector3 curPos, 
		Vector3 startPos, 
		Vector3 endPos, 
		float startTime)
	{
		float passedTime = Time.time - startTime;
		float curPercentage = passedTime / lerpTotalTime;

		curPos = Vector3.Lerp(curPos, endPos, curPercentage);

		float distance =
			Mathf.Sqrt((curPos.x - endPos.x) * (curPos.x - endPos.x) + (curPos.z - endPos.z) * (curPos.z - endPos.z));
		Debug.Log("cupPos=" + curPos);
		Debug.Log("curPercentage=" + curPercentage);
		if((int)distance == 0)
		{
			return false;
		}

		return true;
	}
}
