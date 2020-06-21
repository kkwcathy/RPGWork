using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : SceneBase
{
	[SerializeField] private GameObject[] _titleBtns;
	[SerializeField] private float _delay = 0.8f;

	void Start()
    {
		EnterScene();
		
		StartCoroutine(IEShowTitle());
    }

	IEnumerator IEShowTitle()
	{
		yield return new WaitForSeconds(_delay);

		for (int i = 0; i < _titleBtns.Length; ++i)
		{
			_titleBtns[i].SetActive(true);

			yield return new WaitForSeconds(0.2f);
		}
	}

	private void BtnActivate(bool activate)
	{
		for (int i = 0; i < _titleBtns.Length; ++i)
		{
			_titleBtns[i].SetActive(activate);
		}
	}

	public void GameStart()
	{
		SceneController.Instance.SwitchScene(SceneName.OrganizeScene, SceneSwitchType.Curtain, 0, 1);

		_titleBtns[0].GetComponent<Image>().raycastTarget = false;
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
