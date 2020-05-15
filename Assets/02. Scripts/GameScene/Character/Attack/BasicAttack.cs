using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 기본 공격 동작 클래스
public class BasicAttack : AttackBase
{
	public override void Init()
	{
		_fireTime = 0.5f;
		_finishTime = 1.0f;
	}

	public override void SetFirePoint(Transform effecTr)
	{
		//effecTr.position = _character.tr.position + _character.tr.forward * 1.2f + Vector3.up * 0.4f;
		effecTr.position = _character.tr.position + Vector3.up * 0.4f;
		effecTr.rotation = _character.tr.rotation;
	}

	public override void StartAttack()
	{
		base.StartAttack();

		_character.PlayAnimation("Attack");
	}

	public override void RunAttack()
	{
		if (!_isFired && _elapsedTime >= _fireTime)
		{
			SpawnSkillEffect();

			_isFired = true;
		}
	}
	//private float _coolTime = 1.5f;

	//private bool _isFired = false; // 이펙트가 계속 발사되지 않기 위해 제어하는 변수


	//public override void Fire(Transform tr)
	//{
	//	_elapsedTime += Time.deltaTime;

	//	if (_elapsedTime >= _coolTime)
	//	{
	//		_elapsedTime = 0.0f;
	//		_isFired = false;
	//	}
	//	else if (!_isFired && (_elapsedTime >= _fireTime))
	//	{
	//		GameObject skillEffect = Instantiate(_skillEffect, tr.position, tr.rotation);

	//		// 스킬 이펙트의 레이어를 발사한 캐릭터의 레이어로 설정
	//		skillEffect.layer = tr.gameObject.layer;

	//		// 스킬 이펙트에 기본 공격력 전달
	//		skillEffect.GetComponent<SkillBase>().Power = _basicSkillPower;

	//		_isFired = true;
	//	}
	//}
}
