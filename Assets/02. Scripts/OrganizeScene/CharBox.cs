using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharBox : MonoBehaviour
{
	[SerializeField] private GameObject _outline;
	[SerializeField] private Image _charImg;
	[SerializeField] private Image _container;

	private RectTransform _tr;

	public CharBox LinkedBox;
	public Text _levelText;
	public Rect TrRect;

	public TeamCharInfo Info;
	
	public Color CharImgColor
	{
		get
		{
			return _charImg.color;
		}
		set
		{
			_charImg.color = value;
		}
	}

	public Color ContainerColor
	{
		get
		{
			return _container.color;
		}
		set
		{
			_container.color = value;
		}
	}

	private void Awake()
	{
		_tr = GetComponent<RectTransform>();
		_levelText = GetComponentInChildren<Text>();
	}

	public void SetBoxInfo(TeamCharInfo info)
	{
		Info = info;

		string name = InfoManager.Instance.modelDic[Info.modelID].imgName;
		_charImg.sprite = SpriteManager.Instance.GetSprite(name);

		_levelText.text = info.level.ToString();
	}

	public void SetCharImg(Sprite sprite)
	{
		_charImg.sprite = sprite;
	}

	public Sprite GetSprite()
	{
		if(_charImg.sprite == null)
		{
			Debug.Log("no sprite");
			return null;
		}

		return _charImg.sprite;
	}

	public Rect GetRect()
	{
		return TrRect;
	}

	public bool IsRectEmpty() => (TrRect.center == Vector2.zero);
	public bool IsIn(Vector2 pos) => (TrRect.Contains(pos));
	public bool IsInfoSet() => (_levelText.text.Length > 0);

	public void SetRect()
	{
		float width = _tr.rect.width;
		float height = _tr.rect.height;

		TrRect.width = width;
		TrRect.height = height;

		TrRect.x = _tr.position.x - (width / 2);
		TrRect.y = _tr.position.y - (height / 2);
	}

	public void SwitchOutline(bool on)
	{
		_outline.SetActive(on);
	}
}
