using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Organize Scene에서 드래그 시 마우스 커서와 함께 따라가는 이미지 제어 클래스
public class CursorImage : MonoBehaviour
{
	public RectTransform tr;

	[SerializeField] private float _speed = 10.0f;
	
	[SerializeField] private Image _charImage;
	[SerializeField] private GameObject _shadow;
	[SerializeField] private GameObject _outline;

	private CharBox _returnBox;
	private Vector2 _destPos;

	private float _elapsedTime;
	private bool _isMove = false;

	public delegate void MoveEndHandler();
	private MoveEndHandler _boxHandler;

	private CharBoxInfo _boxInfo;

	private void Awake()
	{
		tr = GetComponent<RectTransform>();
	}

	private void Update()
	{
		if (_isMove)
		{
			Move();
		}
	}

	public void Activate(Sprite sprite)
	{
		_charImage.sprite = sprite;

		gameObject.SetActive(true);

		_shadow.SetActive(true);
		_outline.SetActive(true);
	}

	public void MoveBegin(Vector2 destPos, MoveEndHandler boxCtrl)
	{
		_destPos = destPos;

		// 움직임이 끝날 시 실행할 함수 설정
		_boxHandler = boxCtrl;

		_isMove = true;

		_shadow.SetActive(false);
		_outline.SetActive(false);
	}

	public void Move()
	{
		_elapsedTime += Time.deltaTime * _speed;
		tr.position = Vector2.Lerp(tr.position, _destPos, _elapsedTime);

		if ((Vector2)tr.position == _destPos)
		{
			_boxHandler();
			
			_isMove = false;
			_elapsedTime = 0.0f;

			gameObject.SetActive(false);
		}
	}
}
