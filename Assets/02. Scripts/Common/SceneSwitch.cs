using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneSwitchType
{
	Fade,
	Curtain,
}

public class SceneSwitch : MonoBehaviour
{
	private Dictionary<SceneSwitchType, SceneSwitchUI> _switchUIDic;
	private SceneSwitchUI _curSwitchUI;

    void Awake()
    {
		BuildDic();
    }

	private void BuildDic()
	{
		_switchUIDic = new Dictionary<SceneSwitchType, SceneSwitchUI>();

		_switchUIDic.Add(SceneSwitchType.Fade, Resources.Load<SceneSwitchUI>("Prefabs/UI/FadeUI"));
		_switchUIDic.Add(SceneSwitchType.Curtain, Resources.Load<SceneSwitchUI>("Prefabs/UI/CurtainUI"));
	}

	public void SwitchEnter(SceneSwitchType type, float duration)
	{
		_curSwitchUI = _switchUIDic[type];

		if(!_curSwitchUI.isActiveAndEnabled)
		{
			_curSwitchUI = Instantiate(_curSwitchUI);
			_curSwitchUI.transform.SetParent(transform);
		}

		_curSwitchUI.In(duration);
	}

	public void SwitchExit(float duration = 1.0f)
	{
		if (_curSwitchUI == null)
		{
			return;
		}

		_curSwitchUI.Out(duration);
	}

}
