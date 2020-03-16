using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	delegate void CharAddTargetHandler(Character character);
	delegate void CharDeleteTargetHandler(Character character);

	delegate void PlayerExploreHandler(Vector3 point);

	CharAddTargetHandler _charAddTarget;
	CharDeleteTargetHandler _charDeleteTarget;

	PlayerExploreHandler _playerExplore;

	public EnemyGenerator enemyGenerator;
    public FollowCamera followCam;

	private List<Character> _charList = new List<Character>();

	private Character _headPlayer;
	private int _maxWave = 0;

	private float _waveDelayTime = 4.0f;
	private float _spawnRunTime = 1.0f;

	private void Awake()
	{
		Init();
	}

	private void Start()
	{
		StartCoroutine(RunWaves());
	}

	private void Init()
	{
		Character[] characters = GameObject.Find("Team").GetComponentsInChildren<Character>();
		
		_headPlayer = characters[0];
		followCam.SetMainPlayer(_headPlayer.tr);

		for (int i = 0; i < characters.Length; ++i)
		{
			_playerExplore += new PlayerExploreHandler(characters[i].SetExplorePoint);
			_charAddTarget += new CharAddTargetHandler(characters[i].AddTarget);
			_charDeleteTarget += new CharDeleteTargetHandler(characters[i].DeleteTarget);

			_charList.Add(characters[i]);
		}

		_maxWave = enemyGenerator.GetMaxWave();
		_playerExplore(enemyGenerator.GetCurSpawnPoint().position);
	}

    private void ChangeWave()
    {
		List<Character> enemyList = enemyGenerator.GenerateEnemy();

		for (int i = 0; i < enemyList.Count; ++i)
		{
			_charAddTarget += new CharAddTargetHandler(enemyList[i].AddTarget);

			_charList.Add(enemyList[i]);
		}

		followCam.SetMainEnemy(enemyList[0].tr);
		followCam.ChangeTarget(_spawnRunTime);
	}

	private void AddTarget()
	{
		for(int i = 0; i < _charList.Count; ++i)
		{
			_charAddTarget(_charList[i]);
		}
	}

    private void DeleteTarget(Character target)
    {
		_charAddTarget -= target.AddTarget;
		_charDeleteTarget -= target.DeleteTarget;

		if(IsPlayer(target))
		{
			_playerExplore -= target.SetExplorePoint;
		}

		_charDeleteTarget(target);
    }

	private bool IsWaveClear()
	{
		for(int i = 0; i < _charList.Count; ++i)
		{
			if(_charList[i].GetStateType() == Character.eStateType.Death)
			{
				DeleteTarget(_charList[i]);
				_charList.Remove(_charList[i]);

				--i;
			}
		}

		return _charList.TrueForAll(IsPlayer);
	}
	
	IEnumerator RunWaves()
    {
		while (enemyGenerator.GetCurWave() < _maxWave)
		{
			_playerExplore(enemyGenerator.GetCurSpawnPoint().position);

			yield return new WaitForSeconds(_waveDelayTime);

			ChangeWave();

			yield return new WaitForSeconds(_spawnRunTime);

			AddTarget();

			yield return new WaitUntil(IsWaveClear);

			_playerExplore(enemyGenerator.GetCurSpawnPoint().position);

			followCam.Unzoom();
			enemyGenerator.AddWave();
		}
    }

	private bool IsPlayer(Character character)
	{
		return character.GetCharType() == Character.eCharType.Player;
	}
}
