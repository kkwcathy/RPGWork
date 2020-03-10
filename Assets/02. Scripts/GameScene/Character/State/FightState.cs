using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightState : CharacterState
{
	public FightState(Character character) : base(character)
	{
	}


	public override void SwitchState()
	{
		base.SwitchState();

		_character.StopMove();
		//_character.PlayAnimation("Attack");

		_character.Attack();
		
	}
}
