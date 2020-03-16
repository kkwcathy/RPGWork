using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour
{
	protected Transform _tr;

	protected void StartDo()
	{
		_tr = GetComponent<Transform>();
	}
}
