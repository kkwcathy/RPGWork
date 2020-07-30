using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Organize Scene 에서 드래그 작업 가능한 패널들의 상위 클래스
public class CharPanelBase : MonoBehaviour
{
	[SerializeField] protected PanelType _panelType;

	protected List<CharBox> _charBoxList;
	protected CharBox _curBox;
	
	virtual protected void Init()
	{
		_charBoxList = new List<CharBox>();
	}

	public CharBox GetSelectedBox(Vector2 cursorPos)
	{
		for (int i = 0; i < _charBoxList.Count; ++i)
		{
			// 만일 셀의 영역이 지정되지 않았다면(초기) 지정해주기
			if (_charBoxList[i].IsRectEmpty())
			{
				_charBoxList[i].SetRect();
			}

			if (_charBoxList[i].IsIn(cursorPos) && 
				_charBoxList[i].BoxInfo != null)
			{
				_curBox = _charBoxList[i];
				return _curBox;
			}
		}

		return null;
	}

	// 드래그 시 기존 위치의 박스 형태 변환
	virtual public void DisableBox()
	{

	}

	// 박스 상태 초기화
	virtual public void ReturnBox()
	{

	}

	// 링크에 따른 박스 형태 변화
	virtual public void UpdateBox()
	{

	}

	// CharListGrid에 포함됐던 박스의 기존 위치
	virtual public Vector2 GetOriginPos()
	{
		return _curBox.TrRect.center;
	}

	public PanelType GetPanelType()
	{
		return _panelType;
	}
	
}
