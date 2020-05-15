using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 쿨타임 수정하기
public class SkillBtn : MonoBehaviour
{
	private int _skillIndex = 0;
	private CharacterAttack _charAttack;
	private float _coolTime;

	[SerializeField] private Image _coverImg;

	public void SetBtn(CharacterAttack charAttack, int index, float coolTime)
	{
		_charAttack = charAttack;
		_skillIndex = index;
		_coolTime = coolTime;

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
