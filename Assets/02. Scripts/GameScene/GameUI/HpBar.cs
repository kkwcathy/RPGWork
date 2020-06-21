using UnityEngine;

// 체력 UI 클래스
public class HpBar : MonoBehaviour
{
	private Canvas _canvas;
	private Camera _camera;

	private RectTransform _parentRectTr;
	private RectTransform _rectTr;

	private Transform _targetTr; // 캐릭터 Transform
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

	// 캐릭터 이동 시 함께 이동
	private void LateUpdate()
	{
		Vector3 screenPos = Camera.main.WorldToScreenPoint(_targetTr.position + (Vector3.up * _offset));

		Vector2 pos = Vector2.zero;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTr, screenPos, _camera, out pos);

		_rectTr.localPosition = pos;
	}
}
