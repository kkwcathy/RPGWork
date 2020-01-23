using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
	public Transform Axis;
    void Start()
    {
        
    }

    void Update()
    {
		transform.RotateAround(Axis.position, Vector3.down, 100 * Time.deltaTime);
    }
}
