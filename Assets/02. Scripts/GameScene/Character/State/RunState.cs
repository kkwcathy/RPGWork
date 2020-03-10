using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : CharacterState
{
	public RunState(Character character) : base(character)
	{

	}

	public override void SwitchState()
	{
		base.SwitchState();

		_character.PlayAnimation("Run");
	}
}
