using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 공격 동작 상위 클래스
public class CharacterAttack : MonoBehaviour
{
	[SerializeField] protected GameObject _basicSkillEffect = null;
	
	protected float _basicSkillPower = 20.0f;

	protected bool IsStart = true;
	protected float _elapsedTime = 0.0f;

	virtual public void Fire(Transform tr)
	{

	}

	public float GetElapsedTime()
	{
		return _elapsedTime;
	}
}
