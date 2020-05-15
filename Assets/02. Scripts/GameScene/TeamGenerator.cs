using System.Collections.Generic;
using UnityEngine;

// 플레이어 팀 생성 클래스
public class TeamGenerator : CharacterGenerator
{
	List<int> playerIDList;

	public GameObject _charUIPrefab;
	public Transform[] _charUIPanels; // CharUIPrefab이 생성될 위치들
	
	private void Init()
	{
		playerIDList = new List<int>();

		playerIDList.Add(1);
		playerIDList.Add(2);
		playerIDList.Add(3);
	}

	void Start()
    {
		tr = GetComponent<Transform>();

		Init();
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
}
