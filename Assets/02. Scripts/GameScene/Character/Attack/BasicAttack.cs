using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : Attack
{
	public override void Fire(Transform tr)
	{
		GameObject skillEffect = Instantiate(_basicSkillEffect, tr.position, tr.rotation);
		skillEffect.layer = tr.gameObject.layer;
	}
}
