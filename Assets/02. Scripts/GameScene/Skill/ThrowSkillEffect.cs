using UnityEngine;

// 발사 공격 이펙트 클래스
public class ThrowSkillEffect : SkillBase
{
    private float _speed = 0.5f;
	private float _destroyTime = 2.0f;

    void Start()
    {
		StartDo();

		Destroy(gameObject, _destroyTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer != gameObject.layer)
		{
			Destroy(gameObject);
		}
	}

	void Update()
    {
		MoveEffect();
    }

	public override void MoveEffect()
	{
		_tr.Translate(Vector3.forward * _speed);
	}
}
