using UnityEngine;

// 기본 공격 이펙트 클래스
public class BasicSkillEffect : SkillBase
{
    private float _speed = 0.3f;
	private float _destroyTime = 1.0f;

    void Start()
    {
		StartDo();

        //Destroy(gameObject, _destroyTime);
    }

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("crash" + other.name);

		if (other.gameObject.layer != gameObject.layer)
		{
			Destroy(gameObject);
		}
	}

	void Update()
    {
		//MoveEffect();
    }

	public override void MoveEffect()
	{
		_tr.Translate(Vector3.forward * _speed);
	}
}
