using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurtainUI : SceneSwitchUI
{
	public RectTransform[] imgs;

	Animator _animator;

    void Start()
    {
		_animator = GetComponent<Animator>();
    }

	public override void Out(float duration)
	{
		//base.Out(duration);
		Debug.Log(">>>");
		_animator.SetTrigger("Out");
	}
}
