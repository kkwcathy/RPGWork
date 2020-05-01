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
	[SerializeField] private float _offset = 3.0f;

	private GameObject _hpBar = null;
	private Image _hpImage = null; 

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
		if(_character.GetStateType() != Character.eStateType.Death &&
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

		//_initHp = _character.CharInfo.maxHp;
		_hp = _initHp;
	}

	private void UpdateDo()
	{
		if(_hp <= 0 && _character.GetStateType() != Character.eStateType.Death)
		{
			Debug.Log(name + " Die...");

			_character.ChangeState(Character.eStateType.Death);
			Destroy(_hpBar);
		}
		else if (_isDamaged)
		{
			// 데미지를 받으면 하얗게 깜빡이기
			Blink();
		}
	}

	private void GenerateHpBar()
	{
		Canvas canvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();

		_hpBar = Instantiate(_hpBarPrefab, canvas.transform);

		_hpImage = _hpBar.GetComponentsInChildren<Image>()[1];
		_hpBar.GetComponent<HpBar>().SetBarPosition(_character.tr, _offset);
	}

	virtual public void Damaged(float power)
	{
		_hp -= power;
		_elapsedTime = 0.0f;

		if (!_isDamaged)
		{
			_isDamaged = true;
		}

		if (_hpBar == null)
		{
			GenerateHpBar();
		}

		_hpImage.fillAmount = _hp / _initHp;
	}
	
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
