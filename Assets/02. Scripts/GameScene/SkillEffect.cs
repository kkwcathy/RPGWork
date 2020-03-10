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
        gameObject.layer = transform.parent.gameObject.layer;

        Destroy(gameObject, 1.0f);
    }

    void Update()
    {
        BoundsUpdate();

		//Debug.Log("position"+bs.center);

        transform.Translate(Vector3.forward * speed);
		//transform.RotateAround(Axis.position, Vector3.down, 100 * Time.deltaTime);
    }
}
