using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab = null;

    public GameObject MonsterPoints;

    List<Transform> spawnPoints;
    int[] spawnNumArray;

    const int MAX_SPAWN = 4;
    const int MIN_SPAWN = 1;

	public int _maxWave = 0;

	int _curWave = 0;

	public int CurWave
	{
		get { return _curWave; }
		set { _curWave = value; }
	}

	public int GetMaxWave () => _maxWave;

	void Start()
	{
        InitSpawnSet();
	}

    void InitSpawnSet()
    {
        spawnPoints = new List<Transform>(MonsterPoints.GetComponentsInChildren<Transform>());
		spawnPoints.RemoveAt(0);

		spawnNumArray = new int[spawnPoints.Count];

		_maxWave = spawnPoints.Count;

        for (int i = 0; i < spawnNumArray.Length; ++i)
        {
            spawnNumArray[i] = Random.Range(MIN_SPAWN, MAX_SPAWN);
        }
    }
	
	public Transform GenerateEnemy(List<Character> enemyList)
	{
        int spawnRadius = spawnNumArray[_curWave];
        int angle = Random.Range(0, 360);

        for(int i = 0; i < spawnRadius; ++i)
        {
            float x = spawnRadius * Mathf.Cos(Mathf.PI * angle / 180);
            float z = spawnRadius * Mathf.Sin(Mathf.PI * angle / 180);

            Vector3 generatePos = spawnPoints[_curWave].position + Vector3.forward * z + Vector3.right * x;

            GameObject enemy = Instantiate(enemyPrefab, generatePos, Quaternion.identity);
            enemy.transform.parent = transform;
            enemyList.Add(enemy.GetComponent<Enemy>());

            angle += 360 / spawnRadius;
        }

		return spawnPoints[_curWave];
	}

}
