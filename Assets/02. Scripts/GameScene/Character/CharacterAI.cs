using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI
{
	Character _character;

	private Dictionary<Character.StateType, CharacterState> _charStateDic = new Dictionary<Character.StateType, CharacterState>();
	
	public CharacterAI(Character character)
	{
		_character = character;
	}

	public void Init()
	{
		_charStateDic.Add(Character.StateType.Idle, new IdleState(_character));
		_charStateDic.Add(Character.StateType.RunTowards, new RunState(_character));
		_charStateDic.Add(Character.StateType.Fight, new FightState(_character));
		_charStateDic.Add(Character.StateType.Death, new DeathState(_character));
	}

	public void SwitchState(Character.StateType state)
	{
		_charStateDic[state].StartState();
	}

	public void CheckState(Character.StateType state)
	{
		if (!_character.isDead)
		{
			_charStateDic[state].UpdateState();
		}
		else
		{
			_character.ChangeState(Character.StateType.Death);
		}
	}

}
