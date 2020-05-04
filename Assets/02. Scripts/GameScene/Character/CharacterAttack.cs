using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack 
{
	private float _attackCoolTime = 3.0f;

	public delegate void AttackHandler();
	public AttackHandler _AddTime;

	public enum eAttackType
	{
		None,
		Basic,
		Spin,
		Radiate,
	}

	Character _character;
	private float _basePower;

	AttackBase _basicAttack = null;

	List<AttackBase> _skillList;

	//private Queue<int> _attackQueue = new Queue<int>();
	public CharacterAttack()
	{
		_skillList = new List<AttackBase>();
	}

	public void SetCharacter(Character character, float power)
	{
		_character = character;
		_basePower = power;	
	}

	public void Init()
	{
		for(int i = 0; i < _skillList.Count; ++i)
		{
			_skillList[i].SetCharacter(_character);
			_skillList[i].Init();
		}

		// 첫번째는 기본공격
		_basicAttack = _skillList[0];
		_skillList.RemoveAt(0);
	}

	public void AddAttack(AttackBase attack) { 

		_skillList.Add(attack);
		_AddTime += attack.AddElapsedTime;
	}

	public void UseSkill(int index)
	{
		SetAttack(_skillList[index]);
	}

	public void SetAttack(AttackBase attack)
	{
		attack.SetPower(_basePower);
		attack.StartAttack();
		_character.Attack = attack;
	}

	public void UpdateAttack()
	{
		if(_character.Attack == null &&
			_basicAttack.IsAttackable(_attackCoolTime))
		{
			SetAttack(_basicAttack);
		}
		else if(_character.Attack != null && _character.Attack.IsFinished())
		{
			_character.Attack = null;
		}
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
}
