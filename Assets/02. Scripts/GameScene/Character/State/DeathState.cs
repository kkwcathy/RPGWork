using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : StateBase
{
	public DeathState(Character character) : base(character)
	{
	}

	public override void StartState()
	{
		_character.PlayAnimation("Death");
	}

	public override void UpdateState()
	{
		if (!_character.IsAnimationPlaying("Death"))
		{
			_character.Die();
		}
	}
}
