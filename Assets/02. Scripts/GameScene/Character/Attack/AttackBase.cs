using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 공격 동작 상위 클래스
public class AttackBase : MonoBehaviour
{
	protected Character _character;
	protected GameObject _skillPrefab;

	protected float _elapsedTime = 0.0f;
	protected float _fireTime = 0.0f; // 애니메이션 시작 후 이펙트 발사까지 소요되는 시간
	protected float _power = 0.0f;
	protected bool _isFired = false;

	//public AttackBase(Character character)
	//{
	//	_character = character;
	//}

	public void BuildAttack(Character character)
	{
		_character = character;

		Init();
	}

	virtual public void Init()
	{

	}

	virtual public void SetFirePoint()
	{

	}
	
	public void SetSkillInfo(string effectPath, float power)
	{
		_skillPrefab = Resources.Load(effectPath) as GameObject;
		power = _power;
	}

	virtual public void StartAttack()
	{
		_elapsedTime = 0.0f;
		_isFired = false;
	}

	public void AddElapsedTime()
	{
		_elapsedTime += Time.deltaTime;
	}

	public float GetElapsedTime()
	{
		return _elapsedTime;
	}

	virtual public void RunAttack()
	{
	}

	//[SerializeField] protected GameObject _skillEffect = null;

	//[SerializeField] protected float _basicSkillPower = 20.0f; // 테스트 위해 임시로 설정

	//protected Character _character;

	//protected bool IsStart = true;
	//protected float _elapsedTime = 0.0f;

	//public AttackBase(Character character)
	//{
	//	_character = character;
	//}

	//virtual public void Fire(Transform tr)
	//{ 
	//}

	//public float GetElapsedTime()
	//{
	//	return _elapsedTime;
	//}
}
