using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
	[SerializeField] protected GameObject _charPrefab = null;
	[SerializeField] protected float _spawnRadius = 0.5f;

	protected Transform tr;

	protected Transform _spawnPoint = null;

	protected Vector3 _axis;
	protected int _spawnAmount;

	public List<Character> Generate()
	{
		List<Character> charList = new List<Character>();

		SetSpawnValues();

		float spawnRadius = _spawnRadius * _spawnAmount;
		int angle = Random.Range(0, 360);

		CharacterFactory charFactory = new CharacterFactory();
		CharacterInfo charInfo = new CharacterInfo();

		// 캐릭터 생성 시 일정한 간격으로 배치하기 위하여 생성 수로 나뉜 중심각에 따라 만들어지는 호 들의 끝 좌표 마다 캐릭터를 배치
		for (int i = 0; i < _spawnAmount; ++i)
		{
			//CharacterInfo charInfo = new CharacterInfo();

			float x = spawnRadius * Mathf.Cos(Mathf.PI * angle / 180);
			float z = spawnRadius * Mathf.Sin(Mathf.PI * angle / 180);

			Vector3 spawnPos = _axis + Vector3.forward * z + Vector3.right * x;

			SetCharInfo(charInfo);

			charInfo.charAI = charFactory.GetCharacterAI(charInfo.charType);
			charInfo.charAttack = charFactory.GetCharacterAttack(charInfo.attackIDs);

			PutCharUI(charInfo);

			GameObject clone = Instantiate(_charPrefab, spawnPos, Quaternion.identity);
			clone.transform.parent = tr;

			string prefabStr = InfoManager.Instance.modelDic[charInfo.modelID].prefabName;

			Instantiate(Resources.Load("Prefabs/Models/" + prefabStr), clone.transform);

			Character character = clone.GetComponent<Character>();
			character.BuildCharSetting(charInfo);

			charList.Add(character);

			angle += 360 / _spawnAmount;
		}

		return charList;
	}

	virtual protected void SetCharInfo(CharacterInfo charInfo)
	{

	}

	virtual protected void PutCharUI(CharacterInfo charInfo)
	{

	}

	virtual protected void SetSpawnValues()
	{

	}
}
