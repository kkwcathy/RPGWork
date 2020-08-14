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
	CharBox _attachBox;

	private void Start()
    {
		Init();
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

		_gameStartBtn.SetActive(false);
	}

	public CharBox GetAttachableBox(Vector2 cursorPos)
	{
		for (int i = 0; i < _charBoxList.Count; ++i)
		{
			if (_charBoxList[i].IsIn(cursorPos))
			{
				_attachBox = _charBoxList[i];
				return _attachBox;
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

	public override void UpdateBox()
	{
		// 부착하는 박스가 현재 박스와 다를 경우 서로 정보 바꾸기(swap)
		if(_attachBox != _curBox)
		{
			CharBox curLinkBox = _curBox.BoxInfo.LinkedBox;

			if(_attachBox.BoxInfo != null)
			{
				CharBox attachLinkBox = _attachBox.BoxInfo.LinkedBox;

				attachLinkBox.BoxInfo.LinkedBox = _curBox;
				attachLinkBox.ContainerColor = _curBox.ContainerColor;
			}

			curLinkBox.BoxInfo.LinkedBox = _attachBox;
			curLinkBox.ContainerColor = _attachBox.ContainerColor;


			CharBoxInfo tempInfo = _curBox.BoxInfo;
			
			_curBox.BoxInfo = _attachBox.BoxInfo;
			_attachBox.BoxInfo = tempInfo;

			_curBox.ApplyBoxInfo();
			_curBox = _attachBox;

			UpdateStatus();
		}

		_curBox.ApplyBoxInfo();
		_curBox.SwitchOutline(true);
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
	
	public void UpdateStatus()
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
			if (_charBoxList[i].BoxInfo == null)
			{
				if (_teamModels[i] != null)
				{
					Destroy(_teamModels[i]);
					_teamModels[i] = null;
				}
				continue;
			}

			GameObject modelObj = ResourceManager.Instance.GetPrefab
				(ResourceManager.PrefabType.Models,
				InfoManager.Instance.modelDic[_charBoxList[i].BoxInfo.modelInfo.modelID].prefabName);

			if(_teamModels[i] != modelObj)
			{
				Destroy(_teamModels[i]);
				_teamModels[i] = null;
			}
			else
			{
				continue;
			}

			_teamModels[i] = Instantiate(modelObj, _modelTrList[i]);
		}
	}
}
