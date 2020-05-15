using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
	private Canvas _canvas;
	private Camera _camera;

	private RectTransform _parentRectTr;
	private RectTransform _rectTr;

	private Transform _targetTr;
	private float _offset;

    void Start()
	{
		_canvas = GetComponentInParent<Canvas>();
		_camera = _canvas.worldCamera;

		_parentRectTr = _canvas.GetComponent<RectTransform>();
		_rectTr = GetComponent<RectTransform>();
    }

	public void SetBarPosition(Transform tr, float offset)
	{
		_targetTr = tr;
		_offset = offset;
	}

	private void LateUpdate()
	{
		Vector3 screenPos = Camera.main.WorldToScreenPoint(_targetTr.position + (Vector3.up * _offset));

		Vector2 pos = Vector2.zero;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTr, screenPos, _camera, out pos);

		_rectTr.localPosition = pos;
	}
}
