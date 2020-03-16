using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public CharacterAI GetCharAI()
	{
		return _charAIDic[_character.GetCharType()];
	}
}
