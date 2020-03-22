using System.Collections.Generic;
using UnityEngine;

// 적 생성 클래스
public class EnemyGenerator : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab = null;
	[SerializeField] private float _spawnRadius = 0.5f;

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
		// way point들의 transform 정보를 모두 가져온 후, way point 상위 object의 transform을 제거
		spawnPoints = new List<Transform>(MonsterPoints.GetComponentsInChildren<Transform>());
		spawnPoints.RemoveAt(0); 

		// 웨이브 수는 way point의 갯수로 설정
		_maxWave = spawnPoints.Count;
	}
	
	public List<Character> GenerateEnemy()
	{
		List<Character> enemyList = new List<Character>();

        int spawnNum = Random.Range(minSpawnAmount, maxSpawnAmount + 1);
		float spawnRadius = _spawnRadius * spawnNum;

		int angle = Random.Range(0, 360);

		// 적 생성 시 일정한 간격으로 배치하기 위하여 적 갯수로 나뉜 중심각에 따라 만들어지는 호 들의 끝 좌표 마다 적을 배치
        for(int i = 0; i < spawnNum; ++i)
        {
            float x = spawnRadius * Mathf.Cos(Mathf.PI * angle / 180);
            float z = spawnRadius * Mathf.Sin(Mathf.PI * angle / 180);

            Vector3 generatePos = GetCurSpawnPoint().position + Vector3.forward * z + Vector3.right * x;

            GameObject enemy = Instantiate(enemyPrefab, generatePos, Quaternion.identity);
            enemy.transform.parent = transform;
            enemyList.Add(enemy.GetComponent<Character>());

            angle += 360 / spawnNum;
        }

		return enemyList;
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
}
