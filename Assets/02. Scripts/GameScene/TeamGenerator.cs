using System.Collections.Generic;
using UnityEngine;

// 플레이어 팀 생성 클래스
public class TeamGenerator : CharacterGenerator
{
	List<int> playerIDList;

	[SerializeField] private GameObject _charUIPrefab;
	[SerializeField] private Transform[] _charUIPanels; // CharUIPrefab이 생성될 위치들

	[SerializeField] private int _startAngle;

	void Start()
    {
		tr = GetComponent<Transform>();

		playerIDList = InfoManager.Instance.playerIDList;
	}

	protected override void SetSpawnValues()
	{
		_axis = tr.position;
		_spawnAmount = playerIDList.Count; 
	}

	int index = 0;

	protected override void SetCharInfo(CharacterInfo charInfo)
	{
		charInfo.charType = CharType.Player;

		TeamCharInfo teamInfo = InfoManager.Instance.teamInfoDic[playerIDList[index]];
		ModelInfo modelInfo = InfoManager.Instance.modelDic[teamInfo.modelID];

		charInfo.maxHp = teamInfo.maxHp;
		charInfo.power = teamInfo.power;
		charInfo.defence = teamInfo.defence;

		charInfo.modelID = modelInfo.modelID;
		charInfo.attackIDs = modelInfo.skillIDs;
	}

	protected override void PutCharUI(CharacterInfo charInfo)
	{
		GameObject charUI = Instantiate(_charUIPrefab, _charUIPanels[index]);

		charUI.GetComponent<CharCtrlUI>().SetCharUI(charInfo);
		++index;
	}

	protected override int GetStartAngle()
	{
		return _startAngle;
	}
}
