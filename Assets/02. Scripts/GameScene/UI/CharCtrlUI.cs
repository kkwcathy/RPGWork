using UnityEngine;
using UnityEngine.UI;

// 캐릭터 정보 UI 클래스
public class CharCtrlUI : MonoBehaviour
{
	[SerializeField] Text[] texts; // 순서대로 캐릭터 이름, 스킬1 이름, 스킬2 이름
	[SerializeField] SkillBtn[] skillBtns;
	[SerializeField] Image[] images; // 순서대로 캐릭터 이미지, 스킬1 이미지, 스킬2 이미지

	public void SetCharUI(CharacterInfo charInfo)
	{
		ModelInfo modelInfo = InfoManager.Instance.modelDic[charInfo.modelID];
		
		texts[0].text = modelInfo.modelName;
		images[0].sprite = Resources.Load<Sprite>("Images/Models/" + modelInfo.imgName) as Sprite;

		// 기본 공격 정보는 UI에 들어가지 않으므로 attackID index는 1부터 시작
		for (int i = 1; i < charInfo.attackIDs.Length; ++i)
		{
			AttackInfo attackInfo = InfoManager.Instance.attackInfoDic[charInfo.attackIDs[i]];

			texts[i].text = attackInfo.attackName;
			skillBtns[i - 1].SetBtn(charInfo.charAttack, i - 1, attackInfo.coolTime);
			images[i].gameObject.SetActive(true);
			images[i].sprite = Resources.Load<Sprite>("Images/Skills/" + attackInfo.imgName) as Sprite;
		}
	}
}
