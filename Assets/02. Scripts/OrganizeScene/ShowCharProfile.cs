using UnityEngine;
using UnityEngine.UI;

// Organize Scene에서 박스 클릭 시 보여지는 캐릭터 정보 제어 클래스
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

	public void ShowProfile(CharBoxInfo info)
	{
		_instructionObj.SetActive(false);
		_profileObj.SetActive(true);

		TeamCharInfo charInfo = info.charInfo;
		ModelInfo modelInfo = info.modelInfo;

		_profile.modelImg.sprite = ResourceManager.Instance.GetSprite(ResourceManager.SpriteType.Models, modelInfo.imgName);
		_profile.modelName.text = modelInfo.modelName;

		_profile.level.text = info.levelText;
		_profile.maxHp.text = charInfo.maxHp.ToString();
		_profile.attack.text = charInfo.power.ToString();
	}

	public void CloseProfile()
	{
		_instructionObj.SetActive(true);
		_profileObj.SetActive(false);
	}
}
