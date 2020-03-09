using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI
{
	Character _character;

	private float stopDistance = 1.5f; // 타겟과의 거리가 이만큼 이하이면 멈춤

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

	public void CheckDistance()
	{

	}
	public void UpdateState()
	{
		if (_character.GetTargetDistance() < stopDistance)
		{
			ChangeState(charState.Fight);
		}
	}
}
