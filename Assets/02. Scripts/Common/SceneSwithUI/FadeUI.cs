using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Scene 전환시 흐림 효과
public class FadeUI : SceneSwitchUI
{
	private Image fadeImg;

	[SerializeField] private Color _fadeColor = Color.white;

	private Color _start;
	private Color _end;
	
	void Start()
    {
		fadeImg = GetComponentInChildren<Image>();
	}

	public override void In(float duration)
	{
		base.In(duration);

		_start = new Color(_fadeColor.r, _fadeColor.g, _fadeColor.b, 0.0f);
		_end = new Color(_fadeColor.r, _fadeColor.g, _fadeColor.b, 1.0f);
	}

	public override void Out(float duration)
	{
		base.Out(duration);

		_start = new Color(_fadeColor.r, _fadeColor.g, _fadeColor.b, 1.0f);
		_end = new Color(_fadeColor.r, _fadeColor.g, _fadeColor.b, 0.0f);
	}

    void Update()
    {
		Run();
    }

	private void Run()
	{
		if (!_isUpdate)
		{
			return;
		}

		if (_durationTime != 0.0f)
		{
			_elapsedTime += Time.deltaTime / _durationTime;
		}

		fadeImg.color = Color.Lerp(_start, _end, _elapsedTime);

		if (_elapsedTime >= _durationTime)
		{
			_isUpdate = false;
		}
	}
}
