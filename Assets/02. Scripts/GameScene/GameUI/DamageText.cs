﻿using UnityEngine;
using TMPro;

// 데미지 텍스트 클래스
public class DamageText : MonoBehaviour
{
	private float _elapsedTime = 0.0f;
	private float _floatTime = 0.5f; // 위로 떠오르는 시간
	private float _floatSpeed = 0.8f;

	private bool _isRun = true;

	private Vector2 _startPos;
	private float _offSet;

	private RectTransform _tr;

	[SerializeField] private Animator _animator;

	void Start()
    {
		_tr = GetComponent<RectTransform>();
		_animator = GetComponent<Animator>();

		_tr.localPosition = _startPos;
	}

	public void SetText(int damage)
	{
		TextMeshProUGUI textPro = GetComponent<TextMeshProUGUI>();
		textPro.text = damage.ToString();
	}

	// 생성될 위치 계산 (캐릭터 위치 + offset)
	public void SetPosition(Transform charTr, float offset)
	{
		Canvas _canvas;
		Camera _camera;

		RectTransform _parentRectTr;

		_canvas = GetComponentInParent<Canvas>();
		_camera = _canvas.worldCamera;

		_parentRectTr = _canvas.GetComponent<RectTransform>();
		_offSet = offset;

		Vector3 startPos = Camera.main.WorldToScreenPoint(charTr.position + (Vector3.up * offset));
		
		RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTr, startPos, _camera, out _startPos);
	}

 
    void Update()
    {
		UpdateDo();
    }

	private void UpdateDo()
	{
		if (!_isRun)
		{
			return;
		}

		_elapsedTime += Time.deltaTime;

		if (_elapsedTime < _floatTime)
		{
			_tr.Translate(Vector2.up * _elapsedTime * _floatSpeed);
		}
		// 떠오르는 시간이 지나면 사라지는 애니메이션 실행 및 파괴
		else
		{
			_animator.SetTrigger("Disappear");

			_isRun = false;
			Destroy(gameObject, 0.5f);
		}
			
	}
}