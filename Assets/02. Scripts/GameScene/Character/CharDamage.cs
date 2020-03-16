using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDamage : MonoBehaviour
{
	private Character _character;
	private Renderer _renderer;

	private float _blinkSpeed = 10.0f;

	private float _hp;
	private float _initHp = 100;

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
		if(other.gameObject.layer != gameObject.layer)
		{
			Damaged();
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
			Blink();
		}
	}

	virtual public void Damaged()
	{
		_hp -= 50;

		if (!_isDamaged)
		{
			_isDamaged = true;
		}
	}


	float elapsedTime = 0;

	public void Blink()
	{
		elapsedTime += Time.deltaTime * _blinkSpeed;
		elapsedTime = Mathf.Clamp(elapsedTime, 0.0f, 2.0f);
		Color color = Color.Lerp(Color.black, Color.white, Mathf.PingPong(elapsedTime, 1));

		_renderer.material.SetFloat("_R", color.r);
		_renderer.material.SetFloat("_G", color.g);
		_renderer.material.SetFloat("_B", color.b);

		if (elapsedTime >= 2.0f)
		{
			elapsedTime = 0.0f;
			_isDamaged = false;
		}
	}
}
