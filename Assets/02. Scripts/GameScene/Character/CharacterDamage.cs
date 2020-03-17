using UnityEngine;

// 캐릭터 데미지 관리 클래스
public class CharacterDamage : MonoBehaviour
{
	private Character _character;
	private Renderer _renderer;

	private float _blinkSpeed = 10.0f;

	private float _hp;
	private float _initHp = 100.0f;

	private float _elapsedTime = 0.0f;

	private bool _isDamaged = false;

	void Start()
    {
		_character = GetComponent<Character>();
		_renderer = GetComponentInChildren<Renderer>();
		_hp = _initHp;
	}

	void Update()
	{
		UpdateDo();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Skill" && other.gameObject.layer != gameObject.layer)
		{
			float power = other.gameObject.GetComponent<SkillBase>().Power;

			Damaged(power);
		}
	}

	private void UpdateDo()
	{
		if(_hp <= 0)
		{
			_character.ChangeState(Character.eStateType.Death);
		}
		else if (_isDamaged)
		{
			// 데미지를 받으면 하얗게 깜빡이기
			Blink();
		}
	}

	virtual public void Damaged(float power)
	{
		_hp -= power;
		_elapsedTime = 0.0f;

		if (!_isDamaged)
		{
			_isDamaged = true;
		}
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
