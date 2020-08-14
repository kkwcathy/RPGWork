using UnityEngine;
using TMPro;

// 데미지 텍스트 클래스
public class DamageText : MonoBehaviour, IMovingGameUI
{
	private float _elapsedTime = 0.0f;
	private float _floatTime = 0.5f; // 위로 떠오르는 시간
	private float _floatSpeed = 0.8f;

	private bool _isRun = true;

	private Vector2 _startPos;

	private RectTransform _tr;

	[SerializeField] private Animator _animator;
	[SerializeField] private TMP_Text _text;

	private void Awake()
    {
		_tr = GetComponent<RectTransform>();
		_animator = GetComponent<Animator>();
	}

	private void Start()
	{
		_tr.localPosition = _startPos;
	}

    void Update()
    {
		UpdateDo();
    }

	private void UpdateDo()
	{
		MoveUI();
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
		// 떠오르는 시간이 지나면 사라지는 애니메이션 실행 및 파괴
		else
		{
			_animator.SetTrigger("Disappear");

			_isRun = false;
			Destroy(gameObject, 0.5f);
		}
	}
}
