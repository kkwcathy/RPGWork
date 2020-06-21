using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSwitchUI : MonoBehaviour
{
	protected bool _isUpdate = false;
	protected float _durationTime = 0.0f;
	protected float _elapsedTime = 0.0f;

	virtual public void In(float duration)
	{
		Activate(duration);
	}

	virtual public void Out(float duration)
	{
		Activate(duration);
	}

	private void Activate(float duration)
	{
		_isUpdate = true;
		_elapsedTime = 0.0f;

		_durationTime = duration;
	}
}
