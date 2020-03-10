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

	private Dictionary<charState, CharacterState> _charStateDic = null;

	private CharacterState _state = null;
	private charState _stateKey = charState.None;

	public CharacterAI(Character character)
	{
		_character = character;
	}

	public void Init()
	{
		_charStateDic.Add(charState.Idle,	new IdleState());
		_charStateDic.Add(charState.Run,	new RunState());
		_charStateDic.Add(charState.Fight,	new FightState());
		_charStateDic.Add(charState.Death,	new DeathState());
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
				ChangeState(charState.Fight);
			}
			else if (_stateKey != charState.Run)
			{
				ChangeState(charState.Run);
			}
		}
	}
}
