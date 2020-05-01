using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamGenerator : CharacterGenerator
{
	List<int> playerIDList;
	public List<Transform> spots; // 임시!!!
	
	private void Awake()
	{

	}

	private void Init()
	{
		playerIDList = new List<int>();

		playerIDList.Add(1);
		//playerIDList.Add(2);
		//playerIDList.Add(3);
	}

	void Start()
    {
		tr = GetComponent<Transform>();
		Init();
		//Generate();
	}

	protected override void SetSpawnValues()
	{
		_axis = tr.position;
		_spawnAmount = playerIDList.Count;
	}

	int index = 0;

	protected override void SetCharInfo(CharacterInfo charInfo)
	{
		charInfo.charType = Character.eCharType.Player;

		TeamCharInfo teamInfo = InfoManager.Instance.teamInfoDic[playerIDList[index]];
		ModelInfo modelInfo = InfoManager.Instance.modelDic[teamInfo.modelID];

		charInfo.maxHp = teamInfo.maxHp;
		charInfo.power = teamInfo.power;
		charInfo.defence = teamInfo.defence;

		charInfo.charName = modelInfo.modelName;
		charInfo.prefabName = modelInfo.prefabName;
		charInfo.attackIDs = modelInfo.skillIDs;

		++index;
	}
}
