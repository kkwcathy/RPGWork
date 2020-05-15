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
}
