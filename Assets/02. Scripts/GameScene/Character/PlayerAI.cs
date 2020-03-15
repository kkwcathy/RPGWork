using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : CharacterAI
{
	public PlayerAI(Character character) : base(character)
	{
	}

	public override void Init()
	{
		base.Init();

		_charStateDic.Add(Character.StateType.NoTarget, new ExploreState(_character));
		_charStateDic.Add(Character.StateType.Clear, new ClearState(_character));

		_character.ChangeState(Character.StateType.NoTarget);
	}
}
