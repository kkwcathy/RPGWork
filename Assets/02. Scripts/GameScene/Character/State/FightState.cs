using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightState : StateBase
{
	public FightState(Character character) : base(character)
	{
	}

	public override void StartState()
	{
		_character.StopMove();
		_character.StartBaseAttack();
	}

	public override void UpdateState()
	{
		if (!_character.CheckTargetExist())
		{
			_character.ChangeState(Character.eStateType.NoTarget);
		}
		else if (!_character.CheckTargetDistance(_character.FightDistance))
		{
			_character.ChangeState(Character.eStateType.RunToTarget);
		}
	}
}
