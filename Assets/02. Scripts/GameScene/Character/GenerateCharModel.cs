using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCharModel : MonoBehaviour
{

	public GameObject[] gameObjects;
	// Start is called before the first frame update
	void Start()
    {
		gameObjects = Resources.LoadAll<GameObject>("Prefabs/Models");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
