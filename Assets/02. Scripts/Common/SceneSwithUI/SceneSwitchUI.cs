using UnityEngine;
using UnityEngine.UI;

// Scene 전환 효과 UI 상위 클래스
public class SceneSwitchUI : MonoBehaviour
{
	protected bool _isUpdate = false;
	protected float _durationTime = 0.0f;
	protected float _elapsedTime = 0.0f;

	virtual public void In(float duration)
	{
		Activate(duration);
	}

	virtual public void Out(float duration)
	{
		Activate(duration);
	}

	private void Activate(float duration)
	{
		_isUpdate = true;
		_elapsedTime = 0.0f;

		_durationTime = duration;
	}
}
