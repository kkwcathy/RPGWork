using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 관련 객체 생성 클래스
public class CharacterFactory 
{
	Character _character;

	Dictionary<Character.eCharType, CharacterAI> _charAIDic = new Dictionary<Character.eCharType, CharacterAI>();

	public CharacterFactory(Character character)
	{
		_character = character;
		BuildCharAI();
	}

	public void BuildCharAI()
	{
		_charAIDic.Add(Character.eCharType.Player, new PlayerAI(_character));
		_charAIDic.Add(Character.eCharType.Enemy, new EnemyAI(_character));
	}

	// 현재 캐릭터의 캐릭터 타입에 맞는 AI 객체 반환
	public CharacterAI GetCharAI()
	{
		return _charAIDic[_character.GetCharType()];
	}
}
