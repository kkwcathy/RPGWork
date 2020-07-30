using UnityEngine;

public class TitleScene : SceneBase
{
	void Start()
    {
		EnterScene();
    }

	public void GameStart()
	{
		SceneController.Instance.SwitchScene(SceneName.OrganizeScene, SceneSwitchType.Curtain, 0, 1);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
