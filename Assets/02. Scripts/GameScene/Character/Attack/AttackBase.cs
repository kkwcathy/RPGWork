using UnityEngine;

// 공격 타입
public enum AttackType
{
	None,
	Basic,
	Throw,
	Spin,
	Radiate,
}

// 캐릭터 공격 동작 상위 클래스
public class AttackBase
{
	protected Character _character;
	protected GameObject _skillPrefab;
	
	protected float _elapsedTime = 0.0f;

	protected float _fireTime = 0.0f; // 애니메이션 시작 후 이펙트 발사까지 소요되는 시간
	protected float _finishTime = 0.0f;

	protected float _additionalPower;
	protected float _minDistance;
	protected float _coolTime;

	protected bool _isFired = false;

	protected float _finalPower;

	public void SetSkillInfo(string effectName, float power, float distance, float coolTime)
	{
		_skillPrefab = ResourceManager.Instance.GetPrefab(ResourceManager.PrefabType.Effects, effectName);
		_additionalPower = power;
		_minDistance = distance;
		_coolTime = coolTime;
	}

	public void SetCharacter(Character character)
	{
		_character = character;
	}

	virtual public void Init()
	{

	}

	virtual public void SetFirePoint(Transform effecTr)
	{

	}

	virtual public void StartAttack()
	{
		_elapsedTime = 0.0f;
		_isFired = false;
	}

	public void SetPower(float basePower)
	{
		_finalPower = basePower * _additionalPower;
	}

	public void AddElapsedTime()
	{
		_elapsedTime += Time.deltaTime;
		_elapsedTime = Mathf.Clamp(_elapsedTime, 0.0f, _coolTime);
	}
	
	public void SpawnSkillEffect()
	{
		GameObject skillEffect = _character.Fire(_skillPrefab);
		SetFirePoint(skillEffect.transform);

		// 스킬 이펙트에 공격력 전달
		skillEffect.GetComponent<SkillBase>().Power = _finalPower;
	}

	// 공격 가능 여부
	public bool IsAttackable()
	{
		return _elapsedTime >= _coolTime &&
				_character.CheckTargetDistance(_minDistance);
	}

	// 공격 종료 여부
	public bool IsFinished()
	{
		return _elapsedTime >= _finishTime;
	}
	
	// Update 시 실행될 공격 진행 함수
	virtual public void RunAttack()
	{
	}
}
