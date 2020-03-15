using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDamage : MonoBehaviour
{
	private Renderer _renderer;
	private float _blinkSpeed = 10.0f;

	private bool _isDamaged = false;

	// Start is called before the first frame update
	void Start()
    {
		_renderer = GetComponentInChildren<Renderer>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer != gameObject.layer)
		{
			Damaged();
		}
	}

	virtual public void Damaged()
	{
		//hp -= 10;

		if (!_isDamaged)
		{
			_isDamaged = true;
		}
	}

	// Update is called once per frame
	void Update()
    {
		if (_isDamaged)
		{
			Blink();
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
