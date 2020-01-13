using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private List<Transform> wayPoints;

	public static bool isClear = false;

	System.Func<bool> isWaveCleared = () => isClear;

	public static Transform curWayPoint;
	
	public static bool isGenerated = false;

    // Start is called before the first frame update
    void Start()
    {
		Invoke("RunWaves", 3.0f);
		
    }

	IEnumerator RunWaves()
	{
		for(int i = 0; i < wayPoints.Count; ++i)
		{
			GenerateEnemy(wayPoints[i]);
			yield return new WaitUntil(isWaveCleared);
		}
	}

	public void GenerateEnemy(Transform generatePos)
	{
		GameObject enemy = Instantiate(enemyPrefab, generatePos);
	}
    // Update is called once per frame
    void Update()
    {

    }
}
