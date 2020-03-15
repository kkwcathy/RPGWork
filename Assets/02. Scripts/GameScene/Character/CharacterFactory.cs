using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory 
{
	Character _character;

	Dictionary<Character.CharType, CharacterAI> _charAIDic = new Dictionary<Character.CharType, CharacterAI>();

	public CharacterFactory(Character character)
	{
		_character = character;
		BuildCharAI();
	}

	public void BuildCharAI()
	{
		_charAIDic.Add(Character.CharType.Player, new PlayerAI(_character));
		_charAIDic.Add(Character.CharType.Enemy, new EnemyAI(_character));
	}

	public CharacterAI GetCharAI()
	{
		return _charAIDic[_character.GetCharType()];
	}
}
