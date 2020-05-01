using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack 
{
	private float _attackCoolTime = 3.0f;

	private delegate void AttackHandler();
	private AttackHandler _AddTime;

	public enum eAttackType
	{
		None,
		Basic,
		Spin,
		Radiate,
	}

	Character _character;
	private float _power;

	AttackBase _basicAttack;
	AttackBase[] _skills;

	AttackBase _curAttack = null;

	//private Queue<int> _attackQueue = new Queue<int>();

	public void SetCharacter(Character character, float power)
	{
		_character = character;
		_power = power;
	}

	public void Init()
	{
		//_attacks[0] = new AttackBase(_character);
		//_attackDic.Add(eAttackType.Basic, new BasicAttack());
		//_attackDic.Add(eAttackType.Fire, new FireAttack());
	}

	public void SetSkills(AttackBase[] skills)
	{
		_skills = skills;

		for(int i = 0; i < skills.Length; ++i)
		{
			_AddTime += skills[i].AddElapsedTime;
		}
	}

	public void SetPower(float power)
	{
		_power = power;
	}

	public void ChangeAttack()
	{

	}

	public void UpdateAttack()
	{
		_AddTime();

		//if(_curAttack == null)
		//{
		//	SearchAttack();
		//}
		//else
		//{
		//	RunCurAttack();
		//}
	}

	// 나중에 적절하게 수정
	//public void SearchAttack()
	//{
	//	for(int i = 0; i < _skills.Count; ++i)
	//	{
	//		if(_attackList[i].GetElapsedTime() > _attackCoolTime)
	//		{
	//			_curAttack = _attackList[i];
	//			_curAttack.StartAttack();
	//		}
	//	}
	//}

	public void RunCurAttack()
	{
		_curAttack.RunAttack();
	}

	public bool IsAttackable(AttackBase attack)
	{
		return attack.GetElapsedTime() > _attackCoolTime;
	}
}
