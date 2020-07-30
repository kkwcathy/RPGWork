using UnityEngine;

public class OrganizeScene : SceneBase
{
	[SerializeField] TeamSetPanel _teamSetPanel;
	

	void Start()
	{
		EnterScene();
	}

	public void StartGameScene()
	{
		_teamSetPanel.ConfirmTeam();

		SceneController.Instance.SwitchScene(SceneName.GameScene, SceneSwitchType.Fade, 1.0f, 0.0f);
	}
}
