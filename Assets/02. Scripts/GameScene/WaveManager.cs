using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	// 캐릭터 타겟 리스트 추가/삭제
	delegate void CharAddTargetHandler(Character character);
	delegate void CharDeleteTargetHandler(Character character);

	// 플레이어 탐험 좌표 설정 및 클리어 이벤트 실행
	delegate void PlayerExploreHandler(Vector3 point);
	delegate void PlayerClearHandler();

	CharAddTargetHandler _charAddTarget;
	CharDeleteTargetHandler _charDeleteTarget;

	PlayerExploreHandler _playerExplore;
	PlayerClearHandler _playerClear;

	public EnemyGenerator enemyGenerator;
    public FollowCamera followCam;

	// 현재 스테이지에 존재하는 캐릭터 리스트
	private List<Character> _charList = new List<Character>();

	private Character _headPlayer;
	private int _maxWave = 0;

	private float _waveStartDelayTime = 4.0f; // 새 웨이브 시작 전 딜레이 시간
	private float _spawnRunTime = 1.0f; // 적 생성 이벤트 소요 시간

	private void Awake()
	{
		Init();
	}

	private void Start()
	{
		StartCoroutine(RunWaves());
	}

	// Team 밑에 있는 플레이어들을 가져와 기본 설정 추가해주기
	private void Init()
	{
		Character[] characters = GameObject.Find("Team").GetComponentsInChildren<Character>();
		
		_headPlayer = characters[0];
		followCam.SetMainPlayer(_headPlayer.tr);

		for (int i = 0; i < characters.Length; ++i)
		{
			_playerExplore += new PlayerExploreHandler(characters[i].SetExplorePoint);
			_playerClear += new PlayerClearHandler(characters[i].Clear);

			_charAddTarget += new CharAddTargetHandler(characters[i].AddTarget);
			_charDeleteTarget += new CharDeleteTargetHandler(characters[i].DeleteTarget);
			
			_charList.Add(characters[i]);
		}

		_maxWave = enemyGenerator.GetMaxWave();
		_playerExplore(enemyGenerator.GetCurSpawnPoint().position);
	}

	// 웨이브 변경
    private void ChangeWave()
    {
		// 적 생성
		List<Character> enemyList = enemyGenerator.GenerateEnemy();

		for (int i = 0; i < enemyList.Count; ++i)
		{
			_charAddTarget += new CharAddTargetHandler(enemyList[i].AddTarget);
			_charDeleteTarget += new CharDeleteTargetHandler(enemyList[i].DeleteTarget);

			_charList.Add(enemyList[i]);
		}

		// 생성된 적으로 포커스 변경
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

		if (IsPlayer(target))
		{
			_playerExplore -= target.SetExplorePoint;
			_playerClear -= target.Clear;
		}

		// 캐릭터가 죽어서 삭제되면 다른 캐릭터들의 타켓리스트에서 삭제해주기
		_charDeleteTarget(target);
    }

	// 캐릭터 리스트의 모든 캐릭터들의 죽음 여부를 검사하여 죽은 캐릭터를 모두 리스트에서 삭제하고
	// 남은 캐릭터들의 타입이 모두 플레이어이면(적이 모두 없어지면) 웨이브 클리어
	private bool IsWaveFinished()
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

		return _charList.TrueForAll(IsPlayer) || _charList.TrueForAll(IsEnemy);
	}

	// 웨이브 실행
	IEnumerator RunWaves()
    {
		while (enemyGenerator.GetCurWave() < _maxWave)
		{
			_playerExplore(enemyGenerator.GetCurSpawnPoint().position);

			yield return new WaitForSeconds(_waveStartDelayTime);

			ChangeWave();

			yield return new WaitForSeconds(_spawnRunTime);

			AddTarget();

			yield return new WaitUntil(IsWaveFinished);

			// 적 밖에 남지 않으면 스테이지 실패이므로 break;
			if(_charList.TrueForAll(IsEnemy))
			{
				break;
			}

			followCam.SetMainEnemy(null);
			followCam.Unzoom();
			enemyGenerator.AddWave();
		}

		// 웨이브가 모두 진행된 채로 게임이 끝나면 클리어
		if(enemyGenerator.GetCurWave() == _maxWave)
		{
			_playerClear();
		}
		
    }

	private bool IsPlayer(Character character)
	{
		return character.GetCharType() == Character.eCharType.Player;
	}

	private bool IsEnemy(Character character)
	{
		return character.GetCharType() == Character.eCharType.Enemy;
	}
}
