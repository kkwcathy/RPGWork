using UnityEngine;
using UnityEngine.UI;

// 스킬 버튼 UI 클래스
public class SkillBtn : MonoBehaviour
{
	private int _skillIndex = 0;
	private CharacterAttack _charAttack;
	private float _coolTime;

	[SerializeField] private Image _coverImg; // 쿨타임 차는 동안 돌아갈 회색 이미지

	public void SetBtn(CharacterAttack charAttack, int index, float coolTime)
	{
		_charAttack = charAttack;
		_skillIndex = index;
		_coolTime = coolTime;

		// 버튼 클릭시 공격을 실행하는 Listener 추가
		GetComponent<Button>().onClick.AddListener(() => { StartSkill(); });

		_coverImg.fillAmount = 0;
		_coverImg.raycastTarget = false;
	}

	private void StartSkill()
	{
		_charAttack.UseSkill(_skillIndex);

		_coverImg.fillAmount = 1.0f;
		_coverImg.raycastTarget = true;
	}

	private void Update()
	{
		if(_coverImg.fillAmount <= 0)
		{
			_coverImg.raycastTarget = false;
			return;
		}

		_coverImg.fillAmount -= Time.deltaTime * ( 1.0f / _coolTime);

	}
}
