using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

	

	// Update is called once per frame
	void Update()
    {
        
    }

	private void OnGUI()
	{
		if (GUI.Button(new Rect(0, 0, 100, 100), "Dodge"))
		{
			player.Dodge();
		}

		if (GUI.Button(new Rect(100, 0, 100, 100), "Damaged"))
		{
			player.ShowDamaged();
		}
	}
}
