using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 공격 동작 상위 클래스
public class CharacterAttack : MonoBehaviour
{
	[SerializeField] protected GameObject _skillEffect = null;

	[SerializeField] protected float _basicSkillPower = 20.0f; // 테스트 위해 임시로 설정

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
