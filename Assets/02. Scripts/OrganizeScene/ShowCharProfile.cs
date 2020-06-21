using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCharProfile : MonoBehaviour
{
	[System.Serializable]
	private struct Profile
	{
		public Image modelImg;
		public Text modelName;

		public Text level;
		public Text maxHp;
		public Text attack;
	}

	[SerializeField] private Profile _profile;

	[SerializeField] private GameObject _instructionObj;
	[SerializeField] private GameObject _profileObj;

	public void ShowProfile(TeamCharInfo info)
	{
		if(!_profileObj.activeInHierarchy)
		{
			_instructionObj.SetActive(false);
			_profileObj.SetActive(true);
		}

		ModelInfo modelInfo = InfoManager.Instance.modelDic[info.modelID];

		_profile.modelImg.sprite = SpriteManager.Instance.GetSprite(modelInfo.imgName);
		_profile.modelName.text = modelInfo.modelName;

		_profile.level.text = info.level.ToString();
		_profile.maxHp.text = info.maxHp.ToString();
		_profile.attack.text = info.power.ToString();
	}
}
