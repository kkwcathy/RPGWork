using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Organize Scene에 사용되는 캐릭터 박스 클래스
public class CharBox : MonoBehaviour
{
	[SerializeField] private GameObject _outline;
	[SerializeField] private Image _charImg;
	[SerializeField] private Image _container;
	[SerializeField] private Text _levelText;

	private RectTransform _tr;
	private Sprite _initSprite;
	public CharBoxInfo _boxInfo;

	public Rect TrRect;

	public CharBoxInfo BoxInfo
	{
		get
		{
			return _boxInfo;
		}
		set
		{
			_boxInfo = value;
			ApplyBoxInfo(); 
		}
	}

	public Color CharImgFadeColor
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

		_initSprite = _charImg.sprite;
	}

	public void SetBoxInfo(TeamCharInfo info)
	{
		CharBoxInfo newInfo = new CharBoxInfo(info);
		_charImg.sprite = newInfo.charSprite;

		BoxInfo = newInfo;
	}

	public void ApplyBoxInfo()
	{
		if(BoxInfo == null)
		{
			return;
		}

		_charImg.sprite = BoxInfo.charSprite;
		_levelText.text = BoxInfo.levelText;
	}

	public void EmptyBox()
	{
		_charImg.sprite = _initSprite;
		_levelText.text = "";
	}

	public bool IsRectEmpty() => (TrRect.center == Vector2.zero);
	public bool IsIn(Vector2 pos) => (TrRect.Contains(pos));

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

// Charbox 의 캐릭터 및 링크 정보
[System.Serializable]
public class CharBoxInfo
{
	public TeamCharInfo charInfo;
	public ModelInfo modelInfo;

	public Sprite charSprite;
	public CharBox LinkedBox;

	public string levelText;

	public CharBoxInfo(TeamCharInfo info)
	{
		charInfo = info;
		modelInfo = InfoManager.Instance.modelDic[charInfo.modelID];
		charSprite = ResourceManager.Instance.GetSprite(ResourceManager.SpriteType.Models, modelInfo.imgName);

		levelText = charInfo.level.ToString();

		LinkedBox = null;
	}

	// CharBoxInfo 복사
	public CharBoxInfo CopyCharBoxInfo()
	{
		return new CharBoxInfo(charInfo);
	}
}


