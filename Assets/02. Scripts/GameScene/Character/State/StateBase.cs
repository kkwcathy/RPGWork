using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase
{
	protected Character _character;

	public StateBase(Character character)
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
