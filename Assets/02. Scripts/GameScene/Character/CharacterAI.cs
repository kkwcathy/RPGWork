using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI
{
	Character _character;
	
	public enum charState
	{
		None,
		Idle,
		Run,
		Fight,
		Death,
		Clear,
	}

	private Dictionary<charState, CharacterState> _charStateDic = new Dictionary<charState, CharacterState>();

	private CharacterState _state = null;
	private charState _stateKey = charState.None;

	public CharacterAI(Character character)
	{
		_character = character;
	}

	public void Init()
	{
		_charStateDic.Add(charState.Idle,	new IdleState(_character));
		_charStateDic.Add(charState.Run,	new RunState(_character));
		_charStateDic.Add(charState.Fight,	new FightState(_character));
		_charStateDic.Add(charState.Death,	new DeathState(_character));
	}

	public void ChangeState(charState state)
	{
		_stateKey = state;
		_state = _charStateDic[state];
		_state.SwitchState();
	}

	public charState GetStateKey()
	{
		return _stateKey;
	}

	public void UpdateState()
	{
		if (_character.isDead && 
			_stateKey != charState.Death)
		{
			ChangeState(charState.Death);
		}
		else if(!_character.isDead)
		{
			if (_character.isAttackable &&
			_stateKey != charState.Fight)
			{
				Debug.Log("ss" + _stateKey + _character.name);
				ChangeState(charState.Fight);
			}
			else if (_stateKey != charState.Run && _stateKey != charState.Fight)
			{
				Debug.Log("d");
				ChangeState(charState.Run);
			}
		}
	}
}
