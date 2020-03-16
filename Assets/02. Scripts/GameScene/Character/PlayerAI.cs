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

		_charStateDic.Add(Character.eStateType.NoTarget, new ExploreState(_character));
		_charStateDic.Add(Character.eStateType.Clear, new ClearState(_character));

		_character.ChangeState(Character.eStateType.NoTarget);
	}
}
