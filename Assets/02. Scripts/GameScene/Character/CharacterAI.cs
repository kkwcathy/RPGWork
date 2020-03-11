using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI
{
	Character _character;
	
	public enum CharState
	{
		None,
		Idle,
		Run,
		Fight,
		Death,
		Clear,
	}

	private Dictionary<CharState, CharacterState> _charStateDic = new Dictionary<CharState, CharacterState>();

	private CharacterState _state = null;
	private CharState _stateKey = CharState.None;

	public CharacterAI(Character character)
	{
		_character = character;
	}

	public void Init()
	{
		_charStateDic.Add(CharState.Idle,	new IdleState(_character));
		_charStateDic.Add(CharState.Run,	new RunState(_character));
		_charStateDic.Add(CharState.Fight,	new FightState(_character));
		_charStateDic.Add(CharState.Death,	new DeathState(_character));
	}

	public void ChangeState(CharState state)
	{
		_stateKey = state;
		_state = _charStateDic[state];
		_state.SwitchState();
	}

	public CharState GetStateKey()
	{
		return _stateKey;
	}

	public void UpdateState()
	{
		if (_character.isDead && 
			_stateKey != CharState.Death)
		{
			ChangeState(CharState.Death);
		}
		else if(!_character.isDead)
		{
			if (_character.isAttackable &&
			_stateKey != CharState.Fight)
			{
				ChangeState(CharState.Fight);
			}
			else if (_stateKey != CharState.Run && _stateKey != CharState.Fight)
			{
				ChangeState(CharState.Run);
			}
		}
	}
}
