using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : SkillBase
{
    private float _speed = 1.0f;
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
