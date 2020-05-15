using UnityEngine;
using UnityEngine.UI;

// 캐릭터 데미지 관리 클래스
public class CharacterDamage : MonoBehaviour
{
	private Character _character;
	private Renderer _renderer;

	private float _blinkSpeed = 10.0f;

	private float _defence;
	private float _hp;
	private float _initHp;

	private float _elapsedTime = 0.0f;

	private bool _isDamaged = false;

	[SerializeField] private GameObject _hpBarPrefab = null;
	[SerializeField] private GameObject _damageTextPrefab = null;

	[SerializeField] private float _offset = 3.0f; // Hp Bar와 데미지 텍스트 높이 offset

	private GameObject _hpBar = null;
	private Image _hpImage = null;

	private Canvas _uiCanvas;

	void Start()
    {
		StartDo();
	}

	void Update()
	{
		UpdateDo();
	}

	public void SetDamageStat(float hp, float defence)
	{
		_initHp = _hp = hp;
		_defence = defence;
	}

	private void OnTriggerEnter(Collider other)
	{
		// 충돌한 오브젝트가 스킬 이펙트이고 이벡트를 발사한 캐릭터가 나와 같은 편이 아닐 때 데미지를 입음
		if(_character.GetStateType() != StateType.Death &&
			other.tag == "Skill" && 
			other.gameObject.layer != gameObject.layer)
		{
			float power = other.gameObject.GetComponent<SkillBase>().Power;

			Damaged(power);
		}
	}

	private void StartDo()
	{
		_character = GetComponent<Character>();
		_renderer = GetComponentInChildren<Renderer>();

		_uiCanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();

		_hp = _initHp;
	}

	private void UpdateDo()
	{
		if(_hp <= 0 && _character.GetStateType() != StateType.Death)
		{
			_character.ChangeState(StateType.Death);
			Destroy(_hpBar);
		}
		else if (_isDamaged)
		{
			Blink();
		}
	}

	private void GenerateHpBar()
	{
		_hpBar = Instantiate(_hpBarPrefab, _uiCanvas.transform);

		_hpImage = _hpBar.GetComponentsInChildren<Image>()[1];
		_hpBar.GetComponent<HpBar>().SetBarPosition(_character.tr, _offset);
	}

	private void SpawnDamageText(float damage)
	{
		DamageText damageText = Instantiate(_damageTextPrefab, _uiCanvas.transform).GetComponent<DamageText>();

		damageText.SetText((int)damage);
		damageText.SetPosition(_character.tr, _offset);
	}

	public void Damaged(float power)
	{
		float damage = CalculateDamage(power);

		_hp -= damage;
		_elapsedTime = 0.0f;

		// Blink 효과 제어
		if (!_isDamaged)
		{
			_isDamaged = true;
		}

		// hp Bar 가 없으면 생성
		if (_hpBar == null)
		{
			GenerateHpBar();
		}

		SpawnDamageText(damage);

		_hpImage.fillAmount = _hp / _initHp;
	}
	
	private float CalculateDamage(float power)
	{
		float damage = Random.Range(power - _defence, power - (_defence / 2));

		if(damage <= 0)
		{
			damage = 1;
		}

		return damage;
	}

	// 데미지를 받으면 하얗게 깜빡이기
	public void Blink()
	{
		_elapsedTime += Time.deltaTime * _blinkSpeed;
		_elapsedTime = Mathf.Clamp(_elapsedTime, 0.0f, 2.0f);
		Color color = Color.Lerp(Color.black, Color.white, Mathf.PingPong(_elapsedTime, 1));

		_renderer.material.SetFloat("_R", color.r);
		_renderer.material.SetFloat("_G", color.g);
		_renderer.material.SetFloat("_B", color.b);

		if (_elapsedTime >= 2.0f)
		{
			_elapsedTime = 0.0f;
			_isDamaged = false;
		}
	}
}
