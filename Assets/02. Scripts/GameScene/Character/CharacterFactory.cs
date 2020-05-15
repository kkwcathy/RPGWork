using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 관련 객체 생성 클래스
public class CharacterFactory 
{
	public CharacterAI GetCharacterAI(CharType charType)
	{
		switch (charType)
		{
			case CharType.Player:
				return new PlayerAI();

			case CharType.Enemy:
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
}
