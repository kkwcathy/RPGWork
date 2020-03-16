using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : StateBase
{
	public RunState(Character character) : base(character)
	{

	}

	public override void StartState()
	{
		_character.BeginMove();
		_character.PlayAnimation("Run");
	}

	public override void UpdateState()
	{
		_character.SearchTarget();

		if(!_character.CheckTargetExist())
		{
			_character.ChangeState(Character.eStateType.NoTarget);
		}
		else if (_character.CheckTargetDistance(_character.FightDistance))
		{
			_character.ChangeState(Character.eStateType.Fight);
		}
	}
}
