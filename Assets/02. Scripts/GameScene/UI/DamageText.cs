using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
	private float _elapsedTime = 0.0f;
	private float _floatTime = 0.5f;
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
		else
		{
			_animator.SetTrigger("Disappear");

			_isRun = false;
			Destroy(gameObject, 0.5f);
		}
			
	}
}
