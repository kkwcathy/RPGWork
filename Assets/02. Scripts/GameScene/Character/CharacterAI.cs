﻿using System.Collections.Generic;

// 캐릭터 상태 제어 클래스
public class CharacterAI 
{
	protected Character _character;

	protected Dictionary<StateType, StateBase> _charStateDic;
	
	public void SetCharacter(Character character)
	{
		_character = character;
	}

	// 플레이어와 적 둘다 공통되는 상태 정보 Dictionary 생성
	virtual public void Init()
	{
		_charStateDic = new Dictionary<StateType, StateBase>();

		_charStateDic.Add(StateType.RunToTarget, new RunState(_character));
		_charStateDic.Add(StateType.Fight, new FightState(_character));
		_charStateDic.Add(StateType.Death, new DeathState(_character));
	}

	// 상태 변환
	public void SwitchState(StateType state)
	{
		_charStateDic[state].StartState();
	}

	// 상태 업데이트
	virtual public void CheckState(StateType state)
	{
		_charStateDic[state].UpdateState();
	}
}
