using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private List<Transform> wayPoints;

	public static bool isClear = true;
	public static Transform curWayPoint;

	GameObject curEnemy;

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

		isClear = false;

		for(int i = 0; i < wayPoints.Count; ++i)
		{
			camera.ChangeTarget(wayPoints[i]);
			curWayPoint = wayPoints[i];
			GenerateEnemy(wayPoints[i]);

			yield return new WaitUntil(isWaveCleared);

			isClear = false;
		}
	}

	public void GenerateEnemy(Transform generatePos)
	{
		GameObject enemy = Instantiate(enemyPrefab, generatePos);
		curEnemy = enemy;
	}
    // Update is called once per frame
    void Update()
    {
		// 임시 클리어 조건
		if(curEnemy && curEnemy.transform.position.x != curWayPoint.transform.position.x)
		{
			isClear = true;
		}
    }
}
