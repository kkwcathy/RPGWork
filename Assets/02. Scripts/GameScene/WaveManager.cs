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

	[SerializeField] private List<Character> _charList = null;

	private int _maxWave = 0;
	private float DelayTime = 3.0f;
	private float spawnRunTime = 1.0f;


	private void Awake()
	{
		Init();
	}

	private void Init()
	{
		Character[] characters = GameObject.Find("Team").GetComponentsInChildren<Character>();

		for (int i = 0; i < characters.Length; ++i)
		{
			_playerExplore += new PlayerExploreHandler(characters[i].SetExplorePoint);
			_charAddTarget += new CharAddTargetHandler(characters[i].AddTarget);
			_charDeleteTarget += new CharDeleteTargetHandler(characters[i].DeleteTarget);

			_charList.Add(characters[i]);
		}

		Debug.Log("C");

		_maxWave = enemyGenerator.GetMaxWave();
		_playerExplore(enemyGenerator.GetCurSpawnPoint().position);
	}

	private void Start()
    {
		StartCoroutine(RunWaves());
	}

    private void SetEnemy()
    {
		List<Character> enemyList = enemyGenerator.GenerateEnemy();

		for (int i = 0; i < enemyList.Count; ++i)
		{
			_charAddTarget += new CharAddTargetHandler(enemyList[i].AddTarget);

			_charList.Add(enemyList[i]);
		}
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
			if(_charList[i].IsDead())
			{
				DeleteTarget(_charList[i]);
				_charList.Remove(_charList[i]);

				--i;
			}
		}

		return _charList.TrueForAll(IsPlayer);
	}

	private void FixedUpdate()
	{
		Debug.Log("B");
	}
	private void ChangeWave()
	{
		SetEnemy();

		followCam.ChangeDistance(2, -3, 3);
		followCam.ChangeTarget(enemyGenerator.GetCurSpawnPoint());
	}

	IEnumerator RunWaves()
    {
		while (enemyGenerator.GetCurWave() < _maxWave)
		{
			yield return new WaitForSeconds(DelayTime);

			ChangeWave();

			yield return new WaitForSeconds(Utility.spawnDelayTime);

			AddTarget();

			yield return new WaitUntil(IsWaveClear);

			_playerExplore(enemyGenerator.GetCurSpawnPoint().position);

			followCam.ChangeDistance(-2, 3, -3);
			enemyGenerator.AddWave();
		}
    }

	private bool IsPlayer(Character character)
	{
		return character.GetCharType() == Character.CharType.Player;
	}
}
