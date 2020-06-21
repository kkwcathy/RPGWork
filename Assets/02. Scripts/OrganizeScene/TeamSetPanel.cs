using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamSetPanel : CharPanelBase
{
	[SerializeField] private Sprite _emptySprite;
	[SerializeField] private Color _baseContainerColor;

	[SerializeField] List<Transform> _modelTrList;
	GameObject[] _teamModels;

	private Sprite _prevSprite;

	private void Start()
    {
		Init();
    }

	private void Update()
	{
		SetModel();
	}

	protected override void Init()
	{
		base.Init();

		CharBox[] charBoxes = GetComponentsInChildren<CharBox>();

		for(int i = 0; i < charBoxes.Length; ++i)
		{
			charBoxes[i].SetCharImg(_emptySprite);
			charBoxes[i].SetRect();

			_charBoxList.Add(charBoxes[i]);
		}

		_teamModels = new GameObject[_charBoxList.Count];
	}

	// 빈 박스가 아니면 박스 내용물 이동 가능
	public override bool IsCurBoxAvailable()
	{
		return _curBox.GetSprite() != _emptySprite;
	}

	public override void EmptyBox()
	{
		_prevSprite = _curBox.GetSprite();
		_curBox.SetCharImg(_emptySprite);
		_curBox._levelText.text = "";

		_curBox.SwitchOutline(false);
	}

	public override void ReturnBox()
	{
		_linkedCharBoxDic[GetIndex(_curBox)].ContainerColor = _baseContainerColor;
		DeleteLinkedBox(GetIndex(_curBox));

		_curBox.Info = new TeamCharInfo();
	}

	public override Vector2 GetOriginPos()
	{
		return _linkedCharBoxDic[GetIndex(_curBox)].TrRect.center;
	}

	public CharBox GetAttachableBox(Vector2 cursorPos)
	{
		for (int i = 0; i < _charBoxList.Count; ++i)
		{
			if (_charBoxList[i].IsIn(cursorPos))
			{
				return _charBoxList[i];
			}
		}

		return null;
	}

	public void ConfirmTeam()
	{
		for(int i = 0; i < _charBoxList.Count; ++i)
		{
			if(_charBoxList[i].IsInfoSet())
			{
				InfoManager.Instance.playerIDList.Add(_charBoxList[i].Info.charID);
			}
		}
	}

	public bool IsGamePossible()
	{
		return _linkedCharBoxDic.Count > 0;
	}

	private void SetModel()
	{
		for(int i = 0; i < _charBoxList.Count; ++i)
		{
			if(!_charBoxList[i].IsInfoSet())
			{
				if (_teamModels[i] != null)
				{
					Destroy(_teamModels[i]);
					_teamModels[i] = null;
				}
				continue;
			}
			else if(_teamModels[i] == null)
			{
				_teamModels[i] = Instantiate(Resources.Load<GameObject>("Prefabs/Models/" + 
					InfoManager.Instance.modelDic[_charBoxList[i].Info.modelID].prefabName), 
					_modelTrList[i]);
			}
		}
	}
}
