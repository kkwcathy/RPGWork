using UnityEngine;

// 회전 공격 클래스 (구현 중)
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
