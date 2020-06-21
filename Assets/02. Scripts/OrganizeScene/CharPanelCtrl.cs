using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharPanelCtrl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
	IBeginDragHandler, IDragHandler, IEndDragHandler
{
	//private Transform _tr;
	private CharPanelBase _panelBase;
	//public List<CharBox> _charBoxList;

	// Grid 패널에서 에서 Team Set 패널로 이동하거나
	// Team Set 패널에서 같은 패널의 다른 칸으로 이동이 가능하므로 
	// Team Set 패널 정보는 Grid, Team Set 두 패널에게 모두 필요
	[SerializeField] private TeamSetPanel _attachablePanel;
	[SerializeField] private CharPanelBase _otherPanel;

	[SerializeField] private CursorImage _cursorImage;

	[SerializeField] private ShowCharProfile _showCharProfile;

	private CharBox _selectedBox;

	void Start()
	{
		_panelBase = GetComponent<CharPanelBase>();
		//_tr = GetComponent<Transform>();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		SetOutline(false); //이전에 선택되었던 박스 outline 제거

		CharBox selectedBox = _panelBase.GetSelectedBox(eventData.position);
		
		if(selectedBox == null)
		{
			return;
		}

		_selectedBox = selectedBox;

		ShowSelectedBox();

		if(!_panelBase.IsCurBoxAvailable())
		{
			_selectedBox = null;
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (_selectedBox == null || !_cursorImage.isActiveAndEnabled)
		{
			return;
		}

		CharBox newBox = _attachablePanel.GetAttachableBox(eventData.position);
		
		if (newBox != null)
		{
			//newBox.SetCharImg(_selectedBox.GetSprite());

			if(_panelBase.GetLinkedBox(_panelBase.GetIndex(_selectedBox)) == null)
			{
				_panelBase.AddLinkedBox(_panelBase.GetIndex(_selectedBox), newBox);
				_otherPanel.AddLinkedBox(_otherPanel.GetIndex(newBox), _selectedBox);
			}

			_cursorImage.MoveBegin(newBox.TrRect.center, _panelBase.UpdateBox);
		}
		else
		{
			_cursorImage.MoveBegin(_panelBase.GetOriginPos(), _panelBase.ReturnBox);

			CharBox linkedBox = _panelBase.GetLinkedBox(_panelBase.GetIndex(_selectedBox));
			
			if(linkedBox != null)
			{
				_otherPanel.DeleteLinkedBox(_otherPanel.GetIndex(linkedBox));
			}
		}

		//_selectedBox = null;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (_selectedBox == null)
		{
			return;
		}

		_cursorImage.Activate(_selectedBox.GetSprite());
		_panelBase.EmptyBox();
		_cursorImage.tr.position = eventData.position;
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (_selectedBox == null)
		{
			return;
		}

		_cursorImage.tr.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
	}

	private void ShowSelectedBox()
	{
		_showCharProfile.ShowProfile(_selectedBox.Info);

		SetOutline(true);
	}

	public void SetOutline(bool on)
	{
		// 현재 박스가 아무 정보가 없는 빈칸이면 return
		if (_selectedBox == null ||
			!_selectedBox.IsInfoSet())
		{
			return;
		}
		
		_selectedBox.SwitchOutline(on);

		CharBox linkedBox = _panelBase.GetLinkedBox(_panelBase.GetIndex(_selectedBox));

		if (linkedBox != null)
		{
			linkedBox.SwitchOutline(on);
		}
	}
}