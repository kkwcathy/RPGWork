using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBase : MonoBehaviour
{
	virtual protected void EnterScene()
	{
		SceneController.Instance.OpenScene();
	}

	virtual protected void ExitScene()
	{

	}
}
