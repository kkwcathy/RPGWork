using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharPanelBase : MonoBehaviour
{
	protected List<CharBox> _charBoxList;
	protected Dictionary<int, CharBox> _linkedCharBoxDic;

	protected CharBox _curBox;
	
	virtual protected void Init()
	{
		_charBoxList = new List<CharBox>();
		_linkedCharBoxDic = new Dictionary<int, CharBox>();
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

			//Debug.Log(_charBoxList[i].gameObject.name + "in= " + _charBoxList[i].IsIn(cursorPos));
			//Debug.Log("Available= "+IsBoxAvailable(i));

			if (_charBoxList[i].IsIn(cursorPos))
			{
				_curBox = _charBoxList[i];
				//ChangeSelectedBox();

				return _curBox;
			}
		}

		return null;
	}

	public void AddLinkedBox(int key, CharBox charbox)
	{
		if (!_linkedCharBoxDic.ContainsKey(key))
		{
			_linkedCharBoxDic.Add(key, charbox);
		}
	}

	public void DeleteLinkedBox(int key)
	{
		if(_linkedCharBoxDic.ContainsKey(key))
		{
			_linkedCharBoxDic.Remove(key);
		}
	}

	public int GetIndex(CharBox box)
	{
		return _charBoxList.IndexOf(box);
	}

	public CharBox GetLinkedBox(int key)
	{
		if(!_linkedCharBoxDic.ContainsKey(key))
		{
			return null;
		}

		return _linkedCharBoxDic[key];
	}

	virtual public bool IsCurBoxAvailable()
	{
		return true;
	}

	virtual public void EmptyBox()
	{

	}

	virtual public void ReturnBox()
	{

	}

	virtual public Vector2 GetOriginPos()
	{
		return _curBox.TrRect.center;
	}

	virtual public void UpdateBox()
	{

	}

	virtual public void Attach()
	{

	}
}
