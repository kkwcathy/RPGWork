using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganizeScene : SceneBase
{
	[SerializeField] TeamSetPanel _teamSetPanel;
	[SerializeField] GameObject _gameStartBtn;

	void Start()
	{
		EnterScene();
	}

	public void StartGameScene()
	{
		_teamSetPanel.ConfirmTeam();

		SceneController.Instance.SwitchScene(SceneName.GameScene, SceneSwitchType.Fade, 1.0f, 0.0f);
	}

	private void Update()
	{
		if(!_gameStartBtn.activeInHierarchy && _teamSetPanel.IsGamePossible())
		{
			_gameStartBtn.SetActive(true);
		}
		else if(!_teamSetPanel.IsGamePossible())
		{
			_gameStartBtn.SetActive(false);
		}
	}
}
