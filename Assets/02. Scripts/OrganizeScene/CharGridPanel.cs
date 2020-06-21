using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharGridPanel : CharPanelBase
{
	[SerializeField] private int _maxBoxCount = 20;

	[SerializeField] private GameObject _charBoxPrefab;
	[SerializeField] private GameObject _emptyImgPrefab;
	[SerializeField] private Color _fadeColor;

	private Transform _tr;

	// Start is called before the first frame update
	private void Start()
	{
		_tr = GetComponent<Transform>();

		Init();
	}

	protected override void Init()
	{
		base.Init();

		Dictionary<int, TeamCharInfo> _teamDic = InfoManager.Instance.teamInfoDic;

		for (int i = 0; i < _teamDic.Count; ++i)
		{
			CharBox charBox = Instantiate(_charBoxPrefab, _tr).GetComponent<CharBox>();
			charBox.SetBoxInfo(_teamDic[i + 1]);

			_charBoxList.Add(charBox);
		}

		//남은 칸은 empty 이미지로 채우기
		for (int i = 0; i < (_maxBoxCount - _teamDic.Count); ++i)
		{
			Instantiate(_emptyImgPrefab, _tr);
		}
	}

	public override bool IsCurBoxAvailable()
	{
		return !_linkedCharBoxDic.ContainsValue(_curBox);
	}

	public override void EmptyBox()
	{
		_curBox.CharImgColor = _fadeColor;
	}

	public override void ReturnBox()
	{
		_curBox.CharImgColor = Color.white;
	}

	public override void UpdateBox()
	{
		ReturnBox();

		_curBox.ContainerColor = _linkedCharBoxDic[GetIndex(_curBox)].ContainerColor;

		CharBox linkedBox = _linkedCharBoxDic[GetIndex(_curBox)];

		linkedBox.SetBoxInfo(_curBox.Info);

		//linkedBox.SetCharImg(_curBox.GetSprite());
		//linkedBox.Info = _curBox.Info;
		linkedBox.SwitchOutline(true);

		//_curBox.SwitchOutline(true);
		//_linkedCharBoxDic[GetIndex(_curBox)].SetCharImg(_curBox.GetSprite());
	}


}
