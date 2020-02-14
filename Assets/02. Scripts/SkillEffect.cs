using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
	public Transform Axis;

    public Bounds bs;

    float speed = 1.0f;

    void Start()
    {
        bs.size = Vector3.one;

        Destroy(gameObject, 1.0f);
    }

    void Update()
    {
        bs.center = transform.position;
        transform.Translate(Vector3.forward * speed);
		//transform.RotateAround(Axis.position, Vector3.down, 100 * Time.deltaTime);
    }
}
