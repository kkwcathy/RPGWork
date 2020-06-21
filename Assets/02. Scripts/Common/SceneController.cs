using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName
{
	TitleScene,
	OrganizeScene,
	GameScene,
}

public class SceneController : MonoBehaviour
{
	private static SceneController _instance;

	public static SceneController Instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject sceneController = new GameObject("Scene Controller", typeof(SceneController));
				_sceneSwitch = sceneController.AddComponent<SceneSwitch>();

				DontDestroyOnLoad(sceneController);

				_instance = sceneController.GetComponent<SceneController>();
			}

			return _instance;
		}
	}

	private static SceneSwitch _sceneSwitch;

	void Start()
	{
		//_sceneSwitch = GetComponent<SceneSwitch>();
	}

	public void SwitchScene(SceneName scene)
	{
		SceneManager.LoadScene(scene.ToString());
	}

	public void SwitchScene(SceneName scene, SceneSwitchType type, float duration = 1.0f, float delay = 1.0f)
	{
		_sceneSwitch.SwitchEnter(type, duration);

		delay = duration + delay;
		StartCoroutine(IESwitchScene(scene, delay));
	}

	public void OpenScene(float duration = 1.0f, float delay = 0.5f)
	{
		StartCoroutine(IEOpenScene(duration, delay));
	}

	IEnumerator IESwitchScene(SceneName scene, float delay)
	{
		yield return new WaitForSeconds(delay);

		SwitchScene(scene);
	}

	IEnumerator IEOpenScene(float duration, float delay)
	{
		yield return new WaitForSeconds(delay);

		_sceneSwitch.SwitchExit(duration);
	}
}
