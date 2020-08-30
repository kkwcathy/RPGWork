using UnityEngine;
using TMPro;
using System.Collections;

// 데미지 텍스트 클래스
public class DamageText : MonoBehaviour, IMovingGameUI
{
	private float _elapsedTime = 0.0f;
	private float _floatTime = 0.5f; // 위로 떠오르는 시간
	private float _floatSpeed = 0.8f;
	private float _exitTime = 0.8f;

	private bool _isRun = true;

	private Vector2 _startPos;

	private RectTransform _tr;
	
	[SerializeField] private TMP_Text _text;

	private void Awake()
    {
		_tr = GetComponent<RectTransform>();
	}
	
	public void ActivateText()
	{
		_elapsedTime = 0.0f;

		_tr.localPosition = _startPos;
		_tr.localScale = Vector3.one;

		_isRun = true;
		gameObject.SetActive(true);
	}

    void Update()
    {
		MoveUI();
	}

	public bool IsTextActive
	{
		get
		{
			return gameObject.activeInHierarchy;
		}
	}

	public void SetUIPosition(Canvas uiCanvas, RectTransform uiCanvasTr, Transform targetTr, float offset)
	{
		Vector3 startPos = Camera.main.WorldToScreenPoint(targetTr.position + (Vector3.up * offset));

		RectTransformUtility.ScreenPointToLocalPointInRectangle(uiCanvasTr, startPos, uiCanvas.worldCamera, out _startPos);
	}

	public void SetUIContent(float amount)
	{
		_text.text = ((int)amount).ToString();
	}

	public void MoveUI()
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
		else if (_elapsedTime < _exitTime)
		{
			float time = (_elapsedTime - _floatTime) / (_exitTime - _floatTime);

			_tr.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, time);
		}
		else
		{
			gameObject.SetActive(false);
			_isRun = false;
		}
	}
}
