using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    // 임시 몬스터 데이터
    
    delegate void TeamHandler();
    TeamHandler teamHandler;

    EnemyGenerator enemyGenerator;
    public FollowCamera camera;

    [SerializeField] List<Enemy> _enemyList;
   
    void InitTeamSet()
    {
        Player[] players = GameObject.Find("Team").GetComponentsInChildren<Player>();

        foreach(var i in players)
        {
            teamHandler += new TeamHandler(i.ChangeDestination);
        }
    }
    
    bool isWaveCleared()
    {
        return _enemyList.Count <= 0;
    }

    void Start()
    {
        // 나중에 필요없으면 인스펙터 창에서 드랙앤드롭으로 대체
        enemyGenerator = GetComponent<EnemyGenerator>();

        InitTeamSet();
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemyList.Add(enemy);
    }

    public void DeleteEnemy(Enemy enemy)
    {
        _enemyList.Remove(enemy);
    }

    void Update()
    {
        
    }

    IEnumerator RunWaves()
    {
        yield return new WaitForSeconds(3.0f);

        camera.ChangeDistance(2, -3, 3);

        isClear = false;

        for (int i = 0; i < wayPoints.Count; ++i)
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
}
