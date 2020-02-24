using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCube : MonoBehaviour
{
    public GameObject cube;
    public float radius = 10.0f;
    public int num = 5;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
        
    }

    void Spawn()
    {
        int angle = 360 / num;
        for(int i = 0; i <= 360; i+=angle)
        {
            float x = radius * Mathf.Cos(Mathf.PI * i / 180);
            float y = radius * Mathf.Sin(Mathf.PI * i / 180);

            Vector3 v = transform.position + Vector3.forward * y + Vector3.right * x;
            Instantiate(cube, v, Quaternion.identity);

            Debug.Log(v);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
