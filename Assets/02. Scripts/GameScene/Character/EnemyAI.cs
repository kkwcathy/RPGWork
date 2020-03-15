using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : CharacterAI
{
	public EnemyAI(Character character) : base(character)
	{

	}

	public override void Init()
	{
		base.Init();

		_charStateDic.Add(Character.StateType.NoTarget, new IdleState(_character));
	}
}
