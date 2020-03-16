using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI 
{
	protected Character _character;
	
	protected Dictionary<Character.eStateType, StateBase> _charStateDic = new Dictionary<Character.eStateType, StateBase>();
	
	public CharacterAI(Character character)
	{
		_character = character;
	}

	virtual public void Init()
	{
		_charStateDic.Add(Character.eStateType.RunToTarget, new RunState(_character));
		_charStateDic.Add(Character.eStateType.Fight, new FightState(_character));
		_charStateDic.Add(Character.eStateType.Death, new DeathState(_character));
	}

	public void SwitchState(Character.eStateType state)
	{
		_charStateDic[state].StartState();
	}

	virtual public void CheckState(Character.eStateType state)
	{
		_charStateDic[state].UpdateState();
	}
}
