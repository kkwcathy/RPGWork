using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab = null;

    public GameObject MonsterPoints;
    Transform[] spawnPoints;
    int[] spawnNumArray;

    const int MAX_SPAWN = 4;
    const int MIN_SPAWN = 1;

    int curWave = 0;

	//public static bool isClear = true;
	//public static Transform curWayPoint;

	public bool isWaveStart = false;

	Enemy curEnemy;

	

	//System.Func<bool> isWaveCleared = () => isClear;
	
	// Start is called before the first frame update
	void Start()
	{
        InitSpawnSet();
		//StartCoroutine(RunWaves());

	}

    void InitSpawnSet()
    {
        spawnPoints = MonsterPoints.GetComponentsInChildren<Transform>();
        spawnNumArray = new int[spawnPoints.Length];

        for (int i = 0; i < spawnNumArray.Length; ++i)
        {
            spawnNumArray[i] = Random.Range(MIN_SPAWN, MAX_SPAWN);
        }
    }


    
	public void GenerateEnemy(List<Enemy> enemyList)
	{
        for(int i = 0; i< spawnNumArray[curWave]; ++i)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[curWave]);

            curEnemy = enemy.GetComponent<Enemy>();
            enemy.transform.parent = transform;

            enemyList.Add(enemy.GetComponent<Enemy>());
        }
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
