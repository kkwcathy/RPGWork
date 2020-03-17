using UnityEngine;

public class BasicSkillEffect : SkillBase
{
    private float _speed = 0.3f;
	private float _destroyTime = 1.0f;

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
        _tr.Translate(Vector3.forward * _speed);
    }
}
