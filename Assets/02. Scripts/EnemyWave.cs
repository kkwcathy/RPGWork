using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    // 임시 몬스터 데이터
    
    delegate void TeamHandler(Vector3 desPos);
    TeamHandler teamHandler;

    EnemyGenerator enemyGenerator;
    public FollowCamera followCam;

    [SerializeField] List<Enemy> _enemyList;

	bool _isGameStart = false;
	bool _isWaveCleared () => _enemyList.Count <= 0;

	Enemy curEnemy;
	int curEnemyIndex = 0;

	void InitTeamSet()
    {
        Player[] players = GameObject.Find("Team").GetComponentsInChildren<Player>();

        foreach(var i in players)
        {
            teamHandler += new TeamHandler(i.ChangeDestination);
        }
    }
    
    void Start()
    {
        // 나중에 필요없으면 인스펙터 창에서 드랙앤드롭으로 대체
        enemyGenerator = GetComponent<EnemyGenerator>();

        InitTeamSet();
		StartCoroutine(RunWaves());
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemyList.Add(enemy);
    }

    public void DeleteEnemy(Enemy enemy)
    {
		Debug.Log("delete");
        _enemyList.Remove(enemy);
    }

    void Update()
    {
		if (_isGameStart)
		{
			if (curEnemy.isDead)
			{
				DeleteEnemy(curEnemy);
				if(!_isWaveCleared())
					curEnemy = _enemyList[curEnemyIndex];
			}
			else
			{
				teamHandler(curEnemy.transform.position);
			}
			
		}

	}

	public void ChangeWave()
	{
		curEnemyIndex = 0;

		Transform curPoint = enemyGenerator.GenerateEnemy(_enemyList);
		curEnemy = _enemyList[curEnemyIndex];

		followCam.ChangeDistance(2, -3, 3);
		followCam.ChangeTarget(curPoint);
	}

	IEnumerator RunWaves()
    {
        yield return new WaitForSeconds(3.0f);

		_isGameStart = true;

		while (enemyGenerator.CurWave < enemyGenerator._maxWave)
		{
			ChangeWave();

			yield return new WaitUntil(_isWaveCleared);

			followCam.ChangeDistance(-2, 3, -3);
			enemyGenerator.CurWave += 1;
		}
    }
}
