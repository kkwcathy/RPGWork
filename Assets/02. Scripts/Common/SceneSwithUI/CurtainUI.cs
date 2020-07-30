using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Scene 전환시 여러 이미 조각들이 순차적으로 나타났다가 사라지는 효과 
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
		_animator.SetTrigger("Out");
	}
}
