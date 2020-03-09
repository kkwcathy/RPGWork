using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI
{
	Character _character;

	public enum charState
	{
		Idle,
		Run,
		Fight,
		Death,
		Clear,
	}

	private Dictionary<charState, CharacterState> _charStateDic = null;
	private charState _state = charState.Idle;

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
		_state = state;
	}

	public charState GetCurState()
	{
		return _state;
	}

	public void UpdateState()
	{ 

	}
}
