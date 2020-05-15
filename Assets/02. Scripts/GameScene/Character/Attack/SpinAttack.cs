using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttack : AttackBase
{
	public override void Init()
	{
	}

	public override void SetFirePoint(Transform effecTr)
	{
	}

	public override void StartAttack()
	{
		base.StartAttack();

		Debug.Log("예아 잇츠 스핀어택!!!!");
	}

	public override void RunAttack()
	{
	}
}
