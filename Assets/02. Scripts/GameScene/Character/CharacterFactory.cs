using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 관련 객체 생성 클래스
public class CharacterFactory 
{
	//public void BuildCharInfo(TeamCharInfo teamInfo, ModelInfo modelInfo)
	//{
	//	_charInfo.charName = modelInfo.modelName;
	//	_charInfo.prefabName = modelInfo.prefabName;

	//	_charInfo.maxHp = teamInfo.maxHp;
	//	_charInfo.power = teamInfo.power;
	//	_charInfo.defence = teamInfo.defence;

	//	_charInfo.attackIDs = modelInfo.skillIDs;

	//	_charInfo.charAI = GetCharacterAI();
	//	_charInfo.charAttack = GetCharacterAttack();
	//}

	public CharacterAI GetCharacterAI(Character.eCharType charType)
	{
		switch (charType)
		{
			case Character.eCharType.Player:
				return new PlayerAI();

			case Character.eCharType.Enemy:
				return new EnemyAI();

			default:
				return null;
		}
	}

	public CharacterAttack GetCharacterAttack(int[] attackIDs)
	{
		CharacterAttack charAttack = new CharacterAttack();
		AttackBase[] skills = new AttackBase[attackIDs.Length];

		for(int i = 0; i < skills.Length; ++i)
		{
			AttackType attackType = 
				InfoManager.Instance.attackInfoDic[attackIDs[i]].attackType;

			AttackBase skill = null;
			
			switch (attackType)
			{
				case AttackType.Basic:
					skill = new BasicAttack();
					break;

				case AttackType.Throw:
					skill = new FireAttack();
					break;

				case AttackType.Spin:
					skill = new BasicAttack();
					break;

				case AttackType.Radiate:
					skill = new BasicAttack();
					break;

				default:
					skill = new BasicAttack();
					break;
			}

			skills[i] = skill;
		}

		charAttack.SetSkills(skills);

		return charAttack;
	}

	//public CharacterAttack GetCharacterAttack()
	//{
	//	CharacterAttack charAttack = new CharacterAttack();

	//	for(int i = 0; i < _charInfo.attackIDList.Count; ++i)
	//	{
	//		charAttack.AddAttack(
	//			_attackDic[InfoManager.Instance.attackInfoDic
	//			[_charInfo.attackIDList[i]]
	//			.attackType]);
	//	}

	//	charAttack.SetPower(_charInfo.power);
	
	//	return charAttack;
	//}
	
	//Dictionary<CharType, CharacterAI> _charAIDic;

	//private void BuildCharAIDic()
	//{
	//	_charAIDic.Add(CharType.Player, new PlayerAI());
	//	_charAIDic.Add(CharType.Enemy, new EnemyAI());
	//}

	//현재 캐릭터의 캐릭터 타입에 맞는 AI 객체 반환
	//public CharacterAI GetCharAI()
	//{
	//	return _charAIDic[_charInfo.charType];
	//}
}
