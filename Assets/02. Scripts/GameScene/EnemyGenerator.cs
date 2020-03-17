using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab = null;

    public GameObject MonsterPoints;

    public List<Transform> spawnPoints;

    [SerializeField] private int maxSpawnAmount = 4;
	[SerializeField] private int minSpawnAmount = 1;

	private int _maxWave = 0;

	int _curWave = 0;

	public int GetMaxWave () => _maxWave;

	private void Awake()
	{
		spawnPoints = new List<Transform>(MonsterPoints.GetComponentsInChildren<Transform>());
		spawnPoints.RemoveAt(0);

		_maxWave = spawnPoints.Count;
	}
	
	public List<Character> GenerateEnemy()
	{
		List<Character> enemyList = new List<Character>();

        int spawnRadius = Random.Range(minSpawnAmount, maxSpawnAmount + 1);
		int angle = Random.Range(0, 360);

        for(int i = 0; i < spawnRadius; ++i)
        {
            float x = spawnRadius * Mathf.Cos(Mathf.PI * angle / 180);
            float z = spawnRadius * Mathf.Sin(Mathf.PI * angle / 180);

            Vector3 generatePos = GetCurSpawnPoint().position + Vector3.forward * z + Vector3.right * x;

            GameObject enemy = Instantiate(enemyPrefab, generatePos, Quaternion.identity);
            enemy.transform.parent = transform;
            enemyList.Add(enemy.GetComponent<Character>());

            angle += 360 / spawnRadius;
        }

		return enemyList;
	}

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
