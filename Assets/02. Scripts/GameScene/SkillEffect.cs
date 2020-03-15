using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : SkillBase
{
	public Transform Axis;

    float speed = 1.0f;

    void Start()
    {
        Init();

        Destroy(gameObject, 1.0f);
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
        BoundsUpdate();

		//Debug.Log("position"+bs.center);

        transform.Translate(Vector3.forward * speed);
		//transform.RotateAround(Axis.position, Vector3.down, 100 * Time.deltaTime);
    }
}
