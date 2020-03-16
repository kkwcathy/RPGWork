using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateBase
{
	public IdleState(Character character) : base(character)
	{

	}

	public override void StartState()
	{
		_character.PlayAnimation("Idle");
	}

	public override void UpdateState()
	{
		if (_character.CheckTargetExist())
		{
			_character.ChangeState(Character.eStateType.RunToTarget);
		}
	}
}
