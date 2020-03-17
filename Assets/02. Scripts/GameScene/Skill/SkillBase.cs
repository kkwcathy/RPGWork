using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour
{
	protected Transform _tr;
	protected float _power;

	public float Power
	{
		get { return _power; }
		set { _power = value; }
	}

	protected void StartDo()
	{
		_tr = GetComponent<Transform>();
	}
}
