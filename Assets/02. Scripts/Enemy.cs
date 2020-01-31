using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // Start is called before the first frame update
    void Start()
    {
		GenerateModel();
    }

    // Update is called once per frame
    void Update()
    {
		UpdateDo();

		bs.center = transform.position;
    }
}
