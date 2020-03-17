using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 기본 공격 동작 클래스
public class BasicAttack : CharacterAttack
{
	[SerializeField] private float _fireTime = 0.5f; // 애니메이션 시작 후 이펙트 발사까지 소요되는 시간
	[SerializeField] private float _coolTime = 1.5f;

	private bool _isFired = false; // 이펙트가 계속 발사되지 않기 위해 제어하는 변수

	public override void Fire(Transform tr)
	{
		_elapsedTime += Time.deltaTime;

		if (_elapsedTime >= _coolTime)
		{
			_elapsedTime = 0.0f;
			_isFired = false;
		}
		else if (!_isFired && (_elapsedTime >= _fireTime))
		{
			GameObject skillEffect = Instantiate(_basicSkillEffect, tr.position, tr.rotation);

			// 스킬 이펙트의 레이어를 발사한 캐릭터의 레이어로 설정
			skillEffect.layer = tr.gameObject.layer;

			// 스킬 이펙트에 기본 공격력 전달
			skillEffect.GetComponent<SkillBase>().Power = _basicSkillPower;

			_isFired = true;
		}
	}
}
