using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab = null;
	[SerializeField] private List<Transform> wayPoints;
    [SerializeField] private List<Enemy> enemyList = null;

	public static bool isClear = true;
	public static Transform curWayPoint;

	public bool isWaveStart = false;

	Enemy curEnemy;

	public FollowCamera camera;

	System.Func<bool> isWaveCleared = () => isClear;
	
	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(RunWaves());

	}

	IEnumerator RunWaves()
	{
		yield return new WaitForSeconds(3.0f);

        camera.ChangeDistance(2, -3, 3);

		isClear = false;

		for(int i = 0; i < wayPoints.Count; ++i)
		{
			camera.ChangeTarget(wayPoints[i]);
			curWayPoint = wayPoints[i];
			GenerateEnemy(wayPoints[i]);

			isWaveStart = true;

			yield return new WaitUntil(isWaveCleared);

			isWaveStart = false;
			isClear = false;
		}
	}
    
	public void GenerateEnemy(Transform generatePos)
	{
		GameObject enemy = Instantiate(enemyPrefab, generatePos);
		curEnemy = enemy.GetComponent<Enemy>();
		enemy.transform.parent = transform;

        enemyList.Add(curEnemy);
	}

    // Update is called once per frame
    void Update()
    {
		if (isWaveStart && curEnemy.isDead)
		{
            Debug.Log("dfafd");
            isClear = true;
		}
	}
}
