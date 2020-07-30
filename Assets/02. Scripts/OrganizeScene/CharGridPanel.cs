using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Organize Scene에서 전체 캐릭터 리스트를 조회하는 Grid의 제어 클래스
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

	public override void DisableBox()
	{
		_curBox.CharImgFadeColor = _fadeColor;
	}

	public override void ReturnBox()
	{
		_curBox.CharImgFadeColor = Color.white;
	}

	public override void UpdateBox()
	{
		ReturnBox();

		CharBox linkedBox = _curBox.BoxInfo.LinkedBox;

		// 등록된 팀 컨테이너의 색깔로 현재 박스의 색깔을 바꿔줌
		_curBox.ContainerColor = linkedBox.ContainerColor;

		// 팀 컨테이너에 현재 박스의 정보 넣기
		linkedBox.BoxInfo = _curBox.BoxInfo.CopyCharBoxInfo();
		linkedBox.BoxInfo.LinkedBox = _curBox;
		linkedBox.SwitchOutline(true);
	}
}
