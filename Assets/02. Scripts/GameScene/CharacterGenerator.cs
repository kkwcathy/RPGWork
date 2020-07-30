using System.Collections.Generic;
using UnityEngine;

// 캐릭터 생성 상위 클래스
public class CharacterGenerator : MonoBehaviour
{
	[SerializeField] protected GameObject _charPrefab = null;
	[SerializeField] protected float _spawnRadius = 0.5f;
	[SerializeField] protected float _defaultRotation = 220.0f;

	protected Transform tr;

	protected Transform _spawnPoint = null;

	protected Vector3 _axis;
	protected int _spawnAmount;

	public List<Character> Generate()
	{
		List<Character> charList = new List<Character>();

		SetSpawnValues();

		float spawnRadius = _spawnRadius * _spawnAmount;
		int angle = GetStartAngle();

		CharacterFactory charFactory = new CharacterFactory();
		CharacterInfo charInfo = new CharacterInfo();

		// 캐릭터 생성 시 일정한 간격으로 배치하기 위하여 360도를 생성 수로 나눈 중심각을 계산하고
		// 중심각에 따라 만들어지는 호 들의 끝 좌표 마다 캐릭터를 배치
		for (int i = 0; i < _spawnAmount; ++i)
		{
			float x = spawnRadius * Mathf.Cos(Mathf.PI * angle / -180);
			float z = spawnRadius * Mathf.Sin(Mathf.PI * angle / -180);

			Vector3 spawnPos = _axis + Vector3.forward * z + Vector3.right * x;

			// 캐릭터 정보 설정
			SetCharInfo(charInfo);

			// 설정된 정보에 따라 객체 생성
			charInfo.charAI = charFactory.GetCharacterAI(charInfo.charType);
			charInfo.charAttack = charFactory.GetCharacterAttack(charInfo.attackIDs);

			// 하단 UI 생성
			PutCharUI(charInfo);

			GameObject clone = Instantiate(_charPrefab, spawnPos, Quaternion.identity);
			clone.transform.parent = tr;

			string prefabStr = InfoManager.Instance.modelDic[charInfo.modelID].prefabName;

			Instantiate(
				ResourceManager.Instance.GetPrefab(ResourceManager.PrefabType.Models, prefabStr)
				, clone.transform);

			// 설정된 정보에 따라 캐릭터 스탯 전달
			Character character = clone.GetComponent<Character>();
			character.BuildCharSetting(charInfo);
			character.tr.Rotate(Vector3.one + Vector3.up * _defaultRotation);
			charList.Add(character);

			angle += 360 / _spawnAmount;
		}

		return charList;
	}

	// 플레이어와 적이 캐릭터를 생성할 때 필요한 정보가 서로 다름
	virtual protected void SetCharInfo(CharacterInfo charInfo)
	{

	}

	virtual protected void PutCharUI(CharacterInfo charInfo)
	{

	}

	virtual protected int GetStartAngle()
	{
		return Random.Range(0, 360);
	}

	virtual protected void SetSpawnValues()
	{

	}
}
