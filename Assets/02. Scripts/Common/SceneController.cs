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

// Scene 전환 제어 클래스
public class SceneController : SingleTon<SceneController>
{
	private SceneSwitch _sceneSwitch;

	void Awake()
	{
		_sceneSwitch = gameObject.AddComponent<SceneSwitch>();
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

		SceneManager.LoadScene(scene.ToString());
	}

	IEnumerator IEOpenScene(float duration, float delay)
	{
		yield return new WaitForSeconds(delay);

		_sceneSwitch.SwitchExit(duration);
	}
}
