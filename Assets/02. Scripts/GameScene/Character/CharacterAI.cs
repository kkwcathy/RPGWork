using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI 
{
	protected Character _character;
	
	protected Dictionary<Character.StateType, CharacterState> _charStateDic = new Dictionary<Character.StateType, CharacterState>();
	
	public CharacterAI(Character character)
	{
		_character = character;
	}

	virtual public void Init()
	{
		_charStateDic.Add(Character.StateType.RunToTarget, new RunState(_character));
		_charStateDic.Add(Character.StateType.Fight, new FightState(_character));
		_charStateDic.Add(Character.StateType.Death, new DeathState(_character));
	}

	public void SwitchState(Character.StateType state)
	{
		_charStateDic[state].StartState();
	}

	virtual public void CheckState(Character.StateType state)
	{
		if (_character.IsDead())
		{
			_character.ChangeState(Character.StateType.Death);
			
		}
		else
		{
			_charStateDic[state].UpdateState();
		}
	}

}
