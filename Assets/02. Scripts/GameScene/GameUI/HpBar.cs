using UnityEngine;
using UnityEngine.UI;

// 체력 UI 클래스
public class HpBar : MonoBehaviour, IMovingGameUI
{
	private Canvas _canvas;
	private Camera _camera;

	private RectTransform _uiCanvasTr;
	private RectTransform _tr;

	private Transform _targetTr; // 캐릭터 Transform
	private float _offset;

	[SerializeField] private Image _hpImage;

	public bool IsBarActive
	{
		set
		{
			gameObject.SetActive(value);
		}
		get
		{
			return gameObject.activeInHierarchy;
		}
	}

	void Awake()
	{
		_tr = GetComponent<RectTransform>();
    }

	// 캐릭터 이동 시 함께 이동
	private void LateUpdate()
	{
		MoveUI();
	}

	public void SetUIPosition(Canvas uiCanvas, RectTransform uiCanvasTr, Transform targetTr, float offset)
	{
		_canvas = uiCanvas;
		_uiCanvasTr = uiCanvasTr;
		_targetTr = targetTr;
		_offset = offset;

		_camera = _canvas.worldCamera;
	}

	public void SetUIContent(float amount)
	{
		_hpImage.fillAmount = amount;
	}

	public void MoveUI()
	{
		Vector3 screenPos = Camera.main.WorldToScreenPoint(_targetTr.position + (Vector3.up * _offset));

		Vector2 pos = Vector2.zero;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(_uiCanvasTr, screenPos, _camera, out pos);

		_tr.localPosition = pos;
	}
}
