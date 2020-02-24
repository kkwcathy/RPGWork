using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    private void Awake()
    {
        
    }

    public Player player;

	private void OnGUI()
	{
		if (GUI.Button(new Rect(0, 0, 100, 100), "Dodge"))
		{
			player.Dodge();
		}

		if (GUI.Button(new Rect(100, 0, 100, 100), "Damaged"))
		{
			player.Damaged();
		}
		if (GUI.Button(new Rect(200, 0, 100, 100), "Attack"))
		{
			player.Attack();
		}

		if (GUI.Button(new Rect(300, 0, 100, 100), "Pause"))
		{
			player.Pause();
		}
        if (GUI.Button(new Rect(400, 0, 100, 100), "BasicSkill"))
        {
            player.BasicSkillAttack();
        }
    }

    private void Update()
    {
        //if (player.godjiulguya != null)
        //Debug.Log(Vector3.Distance(player.transform.position, player.godjiulguya.transform.position));
    }
}
