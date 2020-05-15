using System.Collections.Generic;

// 캐릭터 공격 제어 클래스
public class CharacterAttack 
{
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

	// 버튼 UI에서 호출
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
		// 실행 중인 공격이 없고 기본 공격이 실행 가능하면 기본 공격 실행
		if(_character.Attack == null &&
			_basicAttack.IsAttackable())
		{
			SetAttack(_basicAttack);
		}
		// 실행 중인 공격이 끝나면 공격 비우기
		else if(_character.Attack != null && _character.Attack.IsFinished())
		{
			_character.Attack = null;
		}
	}
}
