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

	//public static bool isClear = true;
	//public static Transform curWayPoint;

	//public bool isWaveStart = false;

	//Enemy curEnemy;

	

	//System.Func<bool> isWaveCleared = () => isClear;
	
	// Start is called before the first frame update
	void Start()
	{
        InitSpawnSet();
		//StartCoroutine(RunWaves());

	}

    void InitSpawnSet()
    {
        spawnPoints = new List<Transform>(MonsterPoints.GetComponentsInChildren<Transform>());
		Debug.Log(spawnPoints.Count);
		spawnPoints.RemoveAt(0);
		Debug.Log(spawnPoints.Count);
		spawnNumArray = new int[spawnPoints.Count];

		_maxWave = spawnPoints.Count;

        for (int i = 0; i < spawnNumArray.Length; ++i)
        {
            spawnNumArray[i] = Random.Range(MIN_SPAWN, MAX_SPAWN);
        }
    }
	
	public Transform GenerateEnemy(List<Enemy> enemyList)
	{
        for(int i = 0; i< spawnNumArray[_curWave]; ++i)
        {
			Vector3 generatePos = spawnPoints[_curWave].position + (Vector3.forward * i*2);

			GameObject enemy = Instantiate(enemyPrefab, generatePos, Quaternion.identity);
            enemy.transform.parent = transform;
            enemyList.Add(enemy.GetComponent<Enemy>());
        }

		return spawnPoints[_curWave];
		//enemyList.Add(curEnemy);
	}

    // Update is called once per frame
    void Update()
    {
		//if (isWaveStart && curEnemy.isDead)
		//{
  //          Debug.Log("dfafd");
  //          isClear = true;
		//}
	}
}
