using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PanelType
{
	None,
	CharGrid,
	TeamSet,
}

// Organize Scene 마우스 작업 제어 클래스
public class CharPanelOrganize : MonoBehaviour, IPointerDownHandler,
	IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private Dictionary<PanelType, CharPanelBase> _panelDic;

	[SerializeField] private CursorImage _cursorImage;
	[SerializeField] private ShowCharProfile _showCharProfile;
	
	private PanelType _selectedPanelType;
	private CharBox _selectedBox;

	void Start()
	{
		BuildPanelDic();
	}

	private void BuildPanelDic()
	{
		_panelDic = new Dictionary<PanelType, CharPanelBase>();

		foreach(var i in GetComponentsInChildren<CharPanelBase>())
		{
			_panelDic.Add(i.GetPanelType(), i);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		// 기존에 선택된 박스가 있을 경우 흰색 아웃라인 숨기기
		if(_selectedPanelType != PanelType.None)
		{
			_showCharProfile.CloseProfile();

			SetOutline(false);
		}

		// 선택 정보 초기화
		_selectedPanelType = PanelType.None;
		_selectedBox = null;

		// 선택된 박스 불러오기
		foreach(PanelType key in _panelDic.Keys)
		{
			_selectedBox = _panelDic[key].GetSelectedBox(eventData.position);

			if(_selectedBox != null)
			{
				_selectedPanelType = key;
				break;
			}
		}

		if (_selectedPanelType == PanelType.None)
		{
			return;
		}

		// 선택 박스 존재시 해당 박스의 캐릭터 정보를 우측 상단에 출력
		ShowSelectedBox();

	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		// 선택된 박스가 없을 경우 드래그 하지 않음
		if (_selectedPanelType == PanelType.None)
		{
			return;
		}

		// 팀 세팅으로 옮겨진 박스는 캐릭터 리스트에서 이동 불가
		if(_selectedPanelType == PanelType.CharGrid &&
			_selectedBox.BoxInfo.LinkedBox != null)
		{
			return;
		}

		_cursorImage.Activate(_selectedBox.BoxInfo.charSprite);
		_panelDic[_selectedPanelType].DisableBox();
		_cursorImage.tr.position = eventData.position;

		if(_selectedPanelType == PanelType.TeamSet)
		{
			_selectedBox = _selectedBox.BoxInfo.LinkedBox;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!_cursorImage.isActiveAndEnabled)
		{
			return;
		}

		_cursorImage.tr.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (!_cursorImage.isActiveAndEnabled)
		{
			return;
		}

		TeamSetPanel teamSetPanel = (TeamSetPanel)_panelDic[PanelType.TeamSet];
		CharBox attachableBox = teamSetPanel.GetAttachableBox(eventData.position);

		// 팀 편성 박스에 부착 가능 박스 여부에 따른 작업
		// 시작 지점이 CharGrid 였을 경우 커서 박스 원위치
		// 시작 지점이 TeamSet 였을 경우 커서 박스는 기존 링크됐던 CharGrid의 위치로 돌아감과 동시에 링크 해제 (편성 해제)
		if (attachableBox != null)
		{
			_selectedBox.BoxInfo.LinkedBox = attachableBox;

			_cursorImage.MoveBegin(
				attachableBox.TrRect.center, 
				_panelDic[_selectedPanelType].UpdateBox);
		}
		else
		{
			_cursorImage.MoveBegin(
				_panelDic[_selectedPanelType].GetOriginPos(), 
				_panelDic[_selectedPanelType].ReturnBox);
		}

	}

	private void ShowSelectedBox()
	{
		_showCharProfile.ShowProfile(_selectedBox.BoxInfo);

		SetOutline(true);
	}

	// 흰 아웃라인으로 강조하는 부분 제어
	public void SetOutline(bool on)
	{
		_selectedBox.SwitchOutline(on);

		CharBox linkedBox = _selectedBox.BoxInfo.LinkedBox;

		if(linkedBox != null)
		{
			linkedBox.SwitchOutline(on);
		}

	}
}
