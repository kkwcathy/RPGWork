using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 관련 객체 생성 클래스
public class CharacterFactory 
{
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

		for (int i = 0; i < attackIDs.Length; ++i)
		{
			AttackInfo attackInfo = InfoManager.Instance.attackInfoDic[attackIDs[i]];

			AttackBase skill = null;

			switch (attackInfo.attackType)
			{
				case AttackType.Basic:
					skill = new BasicAttack();
					break;

				case AttackType.Throw:
					skill = new ThrowAttack();
					break;

				case AttackType.Spin:
					skill = new SpinAttack();
					break;

				case AttackType.Radiate:
					Debug.Log("radiate skill is not developed");
					skill = null;
					break;

				default:
					Debug.Log("Skill has no type");
					skill = null;
					break;
			}

			skill.SetSkillInfo("Prefabs/Effects/" + attackInfo.effectName,
								attackInfo.skillPower,
								attackInfo.minDistance,
								attackInfo.coolTime);

			charAttack.AddAttack(skill);
		}
	
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
