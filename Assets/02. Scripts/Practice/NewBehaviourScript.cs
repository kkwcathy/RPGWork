using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
	public List<GameObject> dafda = new List<GameObject>();
	public GameObject prefab;
	public bool button = false;
	// Start is called before the first frame update

	void Start()
    {
		for(int i = 0; i < 3; i++)
		{
			dafda.Add(Instantiate(prefab));
		}
    }

	// Update is called once per frame
	void Update()
    {
        if(button)
		{
			Destroy(dafda[0]);
		}

		Debug.Log(dafda.Count);
    }
}
