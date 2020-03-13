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
		//_character.PlayAnimation("Attack");
		_character.Attack();
	}

	public override void UpdateState()
	{
		if (!_character.IsAttackable())
		{
			_character.ChangeState(Character.StateType.RunTowards);
		}
	}
}
