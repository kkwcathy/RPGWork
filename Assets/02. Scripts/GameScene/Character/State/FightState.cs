using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightState : CharacterState
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
			_character.ChangeState(Character.StateType.NoTarget);
		}
		else if (!_character.CheckTargetDistance())
		{
			_character.ChangeState(Character.StateType.RunToTarget);
		}
	}
}
