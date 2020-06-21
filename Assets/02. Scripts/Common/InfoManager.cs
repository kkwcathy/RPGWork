using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 관련 정보 보유하는 클래스
public class InfoManager : MonoBehaviour
{
	private static InfoManager _instance;

	// CSV 파일을 불러와 저장한 정보들을 담는 Dictionary
	public Dictionary<int, AttackInfo> attackInfoDic; // 공격 정보
	public Dictionary<int, TeamCharInfo> teamInfoDic; // 
	public Dictionary<int, ModelInfo> modelDic;
	public Dictionary<int, MapInfo> mapDic;

	public List<int> playerIDList;

	public int _mapID;

	public int MapID
	{
		get { return _mapID; }
		set { _mapID = value; }
	}

	private void Awake()
	{
		attackInfoDic = new Dictionary<int, AttackInfo>();
		teamInfoDic = new Dictionary<int, TeamCharInfo>();
		modelDic = new Dictionary<int, ModelInfo>();
		mapDic = new Dictionary<int, MapInfo>();

		playerIDList = new List<int>();
	}

	// SingleTon
	public static InfoManager Instance
	{
		get
		{
			if(_instance == null)
			{
				GameObject infoManager = new GameObject("InfoManager", typeof(InfoManager));

				DontDestroyOnLoad(infoManager);
				_instance = infoManager.GetComponent<InfoManager>();
				//_instance = new InfoManager();
				new GameInfoReader().ReadGameInfo();
			}

			return _instance;
		}
	}

	public void AddAttackDic(string[] keys)
	{
		AttackInfo info;

		info.attackID = int.Parse(keys[0]);
		info.attackName = keys[1];
		info.attackType = (AttackType)int.Parse(keys[2]);
		info.skillPower = float.Parse(keys[3]);
		info.minDistance = float.Parse(keys[4]);
		info.coolTime = float.Parse(keys[5]);
		info.effectName = keys[6];
		info.imgName = keys[7];

		attackInfoDic.Add(info.attackID, info);
	}

	public void AddTeamDic(string[] keys)
	{
		TeamCharInfo info;

		info.charID = int.Parse(keys[0]);
		info.modelID = int.Parse(keys[1]);
		info.level = int.Parse(keys[2]);
		info.maxHp = float.Parse(keys[3]);
		info.power = float.Parse(keys[4]);
		info.defence = float.Parse(keys[5]);

		teamInfoDic.Add(info.charID, info);
	}

	public void AddModelDic(string[] keys)
	{
		ModelInfo info;

		info.modelID = int.Parse(keys[0]);
		info.modelName = keys[1];

		string[] skillKeys = keys[2].Split('/');

		info.skillIDs = new int[skillKeys.Length];

		for(int i = 0; i < skillKeys.Length; ++i)
		{
			info.skillIDs[i] = int.Parse(skillKeys[i]);
		}

		info.prefabName = keys[3];

		info.imgName = keys[4];

		modelDic.Add(info.modelID, info);
	}

	public void AddMapDic(string[] keys)
	{
		MapInfo info;

		info.mapLevel = int.Parse(keys[0]);

		string[] modelKeys = keys[1].Split('/');

		info.modelIDs = new int[modelKeys.Length];

		for (int i = 0; i < modelKeys.Length; ++i)
		{
			info.modelIDs[i] = int.Parse(modelKeys[i]);
		}

		info.enemyHP = float.Parse(keys[2]);
		info.enemyPower = float.Parse(keys[3]);
		info.enemyDefence = float.Parse(keys[4]);

		mapDic.Add(info.mapLevel, info);
	}
}

// 공격 타입
public enum AttackType
{
	None,
	Basic,
	Throw,
	Spin,
	Radiate,
}

// 캐릭터 타입
public enum CharType
{
	Player,
	Enemy,
}

// 캐릭터 상태 타입
public enum StateType
{
	NoTarget,
	RunToTarget,
	Fight,
	Death,
	Clear,
}

public struct AttackInfo
{
	public int attackID;
	public string attackName;
	public AttackType attackType;

	public float skillPower; // 스킬을 통해 부가되는 공격력 (원래 공격력 * 스킬 부가 공격력)
	public float minDistance; // 스킬 시전 가능 거리
	public float coolTime; // 스킬 쿨타임

	public string effectName; // 이펙트 프리팹 이름
	public string imgName;
}

public struct TeamCharInfo
{
	public int charID;
	public int modelID;

	public int level;
	public float maxHp;
	public float power;
	public float defence;
}

public struct ModelInfo
{
	public int modelID;

	public string modelName;
	public int[] skillIDs;
	public string prefabName;
	public string imgName;
}

public struct MapInfo
{
	public int mapLevel;
	public int[] modelIDs;

	public float enemyHP;
	public float enemyPower;
	public float enemyDefence;
}