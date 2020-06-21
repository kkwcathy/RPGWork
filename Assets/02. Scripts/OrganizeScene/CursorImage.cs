using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	public delegate void BoxSpriteHandler();
	private BoxSpriteHandler _changeReturnBox;

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

	public void MoveBegin(Vector2 destPos, BoxSpriteHandler boxCtrl)
	{
		_destPos = destPos;

		_changeReturnBox = boxCtrl;

		_isMove = true;

		_shadow.SetActive(false);
		_outline.SetActive(false);
	}

	//public void Attach()
	//{
	//	//_resetBox();
	//	gameObject.SetActive(false);
	//}

	//public void AttachFail()
	//{
	//	_isMove = true;

	//	_shadow.SetActive(false);
	//	_outline.SetActive(false);
	//}

	public void Move()
	{
		_elapsedTime += Time.deltaTime * _speed;
		tr.position = Vector2.Lerp(tr.position, _destPos, _elapsedTime);

		if ((Vector2)tr.position == _destPos)
		{
			_changeReturnBox();

			//_charboxlist[_workindex].fadecharimg(false); 위 절로 대체

			_isMove = false;
			_elapsedTime = 0.0f;

			gameObject.SetActive(false);
		}
	}
}
