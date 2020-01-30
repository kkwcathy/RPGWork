using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public GameObject charModel;
	protected GameObject model;

	public Bounds bs = new Bounds();

	protected Renderer renderer;
	float m_elapsedTime = 0;

	float damageEffectSpeed = 7.0f;
	protected bool isDamaged = false;

	public void GenerateModel()
	{
		model = Instantiate(charModel, transform);

		bs.center = transform.position;

		bs.size = Vector3.one;
		renderer = GetComponentInChildren<Renderer>();
	}

	public void Blink()
	{
		StartCoroutine(Blinkikng());
	}

	public IEnumerator Blinkikng()
	{
		Debug.Log("blink");
		
		m_elapsedTime += Time.deltaTime * damageEffectSpeed;
		m_elapsedTime = Mathf.Clamp(m_elapsedTime, 0.0f, 2.0f);
		Color color = Color.Lerp(Color.black, Color.white, Mathf.PingPong(m_elapsedTime, 1));

		renderer.material.SetFloat("_R", color.r);
		renderer.material.SetFloat("_G", color.g);
		renderer.material.SetFloat("_B", color.b);

		yield return new WaitForSeconds(1.0f);

		m_elapsedTime = 0.0f;
		Debug.Log("blink end");

	}
}
