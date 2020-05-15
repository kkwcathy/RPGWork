using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAttack : AttackBase
{
	float moveSpeed = 10.0f;
	float moveTime = 0.2f;
	float moveDistance = -1.0f;

	Vector3 moveDir;
	bool isAnimationPlayed = false;

	public override void Init()
	{
		_fireTime = 1.0f;
		_finishTime = 2.0f;
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

		isAnimationPlayed = false;
		moveDir = _character.tr.forward * moveDistance;
	}

	public override void RunAttack()
	{
		if (_elapsedTime < moveTime)
		{
			_character.StraightMove(moveDir, moveSpeed);
		}
		else if(!isAnimationPlayed)
		{
			_character.PlayAnimation("Attack");
			isAnimationPlayed = true;
		}
		else if (!_isFired && _elapsedTime >= _fireTime)
		{
			SpawnSkillEffect();

			_isFired = true;
		}
	}
}
