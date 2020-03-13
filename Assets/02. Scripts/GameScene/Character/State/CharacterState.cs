using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState
{
	protected Character _character;
	
	public CharacterState(Character character)
	{
		_character = character;
	}

	virtual public void StartState()
	{

	}

	virtual public void UpdateState()
	{
		
	}
}
