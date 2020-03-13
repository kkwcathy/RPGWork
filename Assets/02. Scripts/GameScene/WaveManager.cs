using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    delegate void TeamHandler(List<Character> targetList);
    TeamHandler teamHandler;

    EnemyGenerator enemyGenerator;
    public FollowCamera followCam;

    [SerializeField] List<Character> _enemyList = null;

	bool _isGameStart = false;
	bool _isWaveCleared () => _enemyList.Count <= 0;

	Character curEnemy;
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

    public void AddEnemy(Character enemy)
    {
        _enemyList.Add(enemy);
    }

    public void DeleteEnemy(Character enemy)
    {
        _enemyList.Remove(enemy);
    }

    void Update()
    {
		if (_isGameStart)
		{
            CheckDeath();
            teamHandler(_enemyList);
		}

	}

    public void CheckDeath()
    {
        for(int i = 0; i < _enemyList.Count; ++i)
        {
            if(_enemyList[i].isDead)
            {
                DeleteEnemy(_enemyList[i]);
                //teamHandler(_enemyList);
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
