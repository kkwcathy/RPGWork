using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 기본 공격 동작 클래스
public class BasicAttack : CharacterAttack
{
	public override void Fire(Transform tr)
	{
		GameObject skillEffect = Instantiate(_basicSkillEffect, tr.position, tr.rotation);

		// 스킬 이펙트의 레이어를 발사한 캐릭터의 레이어로 설정
		skillEffect.layer = tr.gameObject.layer; 

		// 스킬 이펙트에 기본 공격력 전달
		skillEffect.GetComponent<SkillBase>().Power = _basicSkillPower;
	}
}
