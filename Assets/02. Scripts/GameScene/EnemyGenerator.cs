using System.Collections.Generic;
using UnityEngine;

// 적 생성 클래스
public class EnemyGenerator : CharacterGenerator
{
    public GameObject MonsterPoints;

    public List<Transform> spawnPoints;

	// Wave 마다 생성되는 적 마리수의 최소값과 최대값
    [SerializeField] private int maxSpawnAmount = 4;
	[SerializeField] private int minSpawnAmount = 1;

	private int _maxWave = 0;
	private int _curWave = 0;

	public int GetMaxWave () => _maxWave;

	private void Awake()
	{
		
	}

	private void Start()
	{
		tr = GetComponent<Transform>();

		// way point들의 transform 정보를 모두 가져온 후, way point 상위 object의 transform을 제거
		spawnPoints = new List<Transform>(MonsterPoints.GetComponentsInChildren<Transform>());
		spawnPoints.RemoveAt(0);

		// 웨이브 수는 way point의 갯수로 설정
		_maxWave = spawnPoints.Count;
	}

	// 플레이어의 탐험 좌표를 알려주기 위해 현재 way point 좌표 반환
	public Transform GetCurSpawnPoint()
	{
		return spawnPoints[_curWave];
	}

	public int GetCurWave()
	{
		return _curWave;
	}

	public int MaxWave()
	{
		return _maxWave;
	}

	public void AddWave()
	{
		++_curWave;
	}

	protected override void SetSpawnValues()
	{
		_axis = GetCurSpawnPoint().position;
		_spawnAmount = Random.Range(minSpawnAmount, maxSpawnAmount + 1);
	}

	protected override void SetCharInfo(CharacterInfo charInfo)
	{
		charInfo.charType = CharType.Enemy;
		
		MapInfo mapInfo = InfoManager.Instance.mapDic[InfoManager.Instance.MapID];
		ModelInfo modelInfo;

		ModelInfo[] modelInfos = new ModelInfo[mapInfo.modelIDs.Length];

		for(int i = 0; i < modelInfos.Length; ++i)
		{
			modelInfos[i] = InfoManager.Instance.modelDic[mapInfo.modelIDs[i]];
		}

		int index = Random.Range(0, modelInfos.Length);

		modelInfo = modelInfos[index];

		charInfo.maxHp = mapInfo.enemyHP;
		charInfo.power = mapInfo.enemyPower;
		charInfo.defence = mapInfo.enemyDefence;

		charInfo.modelID = modelInfo.modelID;
		charInfo.attackIDs = modelInfo.skillIDs;

		_defaultRotation = Random.Range(0, 360);
	}
}
