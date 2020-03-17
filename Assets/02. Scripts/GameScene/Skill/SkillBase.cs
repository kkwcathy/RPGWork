using UnityEngine;

// 스킬 이펙트 상위 클래스
public class SkillBase : MonoBehaviour
{
	protected Transform _tr;
	protected float _power;

	public float Power
	{
		get { return _power; }
		set { _power = value; }
	}

	protected void StartDo()
	{
		_tr = GetComponent<Transform>();
	}

	// 스킬 실행 시 이펙트 움직임 함수
	virtual public void MoveEffect()
	{

	}
}
