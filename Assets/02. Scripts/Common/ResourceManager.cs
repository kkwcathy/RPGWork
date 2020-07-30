using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 프리팹, 이미지 등의 리소스 캐싱 클래스
public class ResourceManager : SingleTon<ResourceManager>
{
	public enum PrefabType
	{
		Effects,
		Models,
		UI,
	}

	public enum SpriteType
	{
		Models,
		Skills,
	}

	Dictionary<string, GameObject> _prefabDic;
	Dictionary<string, Sprite> _spriteDic;

	private void Awake()
	{
		_prefabDic = new Dictionary<string, GameObject>();
		_spriteDic = new Dictionary<string, Sprite>();
	}

	private void LoadSprite(string name)
	{
		_spriteDic.Add(name, Resources.Load<Sprite>("Images/Models/" + name) as Sprite);
	}

	public T GetObj<T>(Dictionary<string, T> dic, string path, string name) where T : Object
	{
		// 원하는 Object 가 Load 되지 않은 경우 Load 해주기
		if (!dic.ContainsKey(name))
		{
			dic.Add(name, Resources.Load<T>(path + "/" + name) as T);
		}

		return dic[name];
	}
	
	public Sprite GetSprite(SpriteType spriteType, string name)
	{
		string path = "Images/" + spriteType.ToString();

		return GetObj(_spriteDic, path, name);
	}

	public GameObject GetPrefab(PrefabType prefabType, string name)
	{
		string path = "Prefabs/" + prefabType.ToString();

		return GetObj(_prefabDic, path, name);
	}
}
