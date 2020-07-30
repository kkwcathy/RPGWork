using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Organize Scene의 팀 편성 칸 제어
public class TeamSetPanel : CharPanelBase
{
	[SerializeField] private Sprite _emptySprite;
	[SerializeField] private Color _baseContainerColor;

	[SerializeField] List<Transform> _modelTrList;
	[SerializeField] GameObject _gameStartBtn;
	
	GameObject[] _teamModels;

	private void Start()
    {
		Init();
    }

	private void Update()
	{
		UpdateDo();
	}

	protected override void Init()
	{
		base.Init();

		CharBox[] charBoxes = GetComponentsInChildren<CharBox>();
		
		for(int i = 0; i < charBoxes.Length; ++i)
		{
			charBoxes[i].SetRect();
			charBoxes[i].BoxInfo = null;

			_charBoxList.Add(charBoxes[i]);
		}

		_teamModels = new GameObject[_charBoxList.Count];
	}

	public CharBox GetAttachableBox(Vector2 cursorPos)
	{
		for (int i = 0; i < _charBoxList.Count; ++i)
		{
			if (_charBoxList[i].IsIn(cursorPos))
			{
				_curBox = _charBoxList[i];
				return _curBox;
			}
		}

		return null;
	}

	public override void DisableBox()
	{
		_curBox.EmptyBox();
		_curBox.SwitchOutline(false);
	}

	public override Vector2 GetOriginPos()
	{
		CharBox linkedBox = _curBox.BoxInfo.LinkedBox;

		return linkedBox.TrRect.center;
	}

	public override void ReturnBox()
	{
		CharBox linkedBox = _curBox.BoxInfo.LinkedBox;

		linkedBox.ContainerColor = _baseContainerColor;
		
		_curBox.BoxInfo = null;
		linkedBox.BoxInfo.LinkedBox = null;
	}

	public void ConfirmTeam()
	{
		for(int i = 0; i < _charBoxList.Count; ++i)
		{
			CharBoxInfo boxInfo = _charBoxList[i].BoxInfo;
			if (boxInfo != null)
			{
				InfoManager.Instance.playerIDList.Add(boxInfo.charInfo.charID);
			}
		}
	}

	public bool IsGamePossible()
	{
		for(int i = 0; i < _charBoxList.Count; ++i)
		{
			// 캐릭터가 세팅된 칸이 한 개라도 존재하면 게임 실행 가능
			if(_charBoxList[i].BoxInfo != null)
			{
				return true;
			}
		}

		return false;
	}
	
	private void UpdateDo()
	{
		// 게임 시작 버튼 
		if (!_gameStartBtn.activeInHierarchy && IsGamePossible())
		{
			_gameStartBtn.SetActive(true);
		}
		else if (!IsGamePossible())
		{
			_gameStartBtn.SetActive(false);
		}

		//모델
		for (int i = 0; i < _charBoxList.Count; ++i)
		{
			if(_charBoxList[i].BoxInfo == null)
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
				GameObject modelObj = ResourceManager.Instance.GetPrefab
					(ResourceManager.PrefabType.Models,
					InfoManager.Instance.modelDic[_charBoxList[i].BoxInfo.modelInfo.modelID].prefabName);

				_teamModels[i] = Instantiate(modelObj, _modelTrList[i]);
			}
		}
	}
}
