using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager
{
	private static SpriteManager _instance;

	public static SpriteManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new SpriteManager();
			}

			return _instance;
		}
	}

	Dictionary<string, Sprite> _spriteDic;

	private SpriteManager()
	{
		_spriteDic = new Dictionary<string, Sprite>();
	}

	private void LoadSprite(string name)
	{
		//string imgName = InfoManager.Instance.modelDic[id].imgName;

		_spriteDic.Add(name, Resources.Load<Sprite>("Images/Models/" + name) as Sprite);
	}

	public Sprite GetSprite(string name)
	{
		// 원하는 모델의 sprite가 Load 되지 않은 경우 Load 해주기
		if(!_spriteDic.ContainsKey(name))
		{
			LoadSprite(name);
		}

		return _spriteDic[name];
	}
}
