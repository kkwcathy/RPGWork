using UnityEngine;

// Scene 작업 실행하는 Scene 상위 클래스
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
