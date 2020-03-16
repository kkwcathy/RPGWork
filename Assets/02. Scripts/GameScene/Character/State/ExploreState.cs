using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreState : StateBase
{
	public ExploreState(Character character) : base(character)
	{
	}

	public override void StartState()
	{
		_character.PlayAnimation("Run");
		_character.Explore();
		_character.BeginMove();
	}

	public override void UpdateState()
	{
		if(_character.CheckTargetExist())
		{
			_character.ChangeState(Character.eStateType.RunToTarget);
		}
	}

}
