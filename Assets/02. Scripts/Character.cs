using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public GameObject charModel;
	protected GameObject model;

	public Bounds bs = new Bounds();

	protected Renderer renderer;

	protected bool isDamaged = false;

	public bool isDead = false;

	protected float m_elapsedTime = 0;

	protected float damageEffectSpeed = 10.0f;

	protected Character targetObj = null;

	[SerializeField] int hp = 100;

	public void GenerateModel()
	{
		model = Instantiate(charModel, transform);

		bs.center = transform.position;

		bs.size = Vector3.one;
		renderer = GetComponentInChildren<Renderer>();
	}

	public void Damaged()
	{
		hp -= 10;

		if(!isDamaged)
		{
			isDamaged = true;
		}
	}

	public void Attack()
	{
        StartCoroutine(Attacking());
	}

    IEnumerator Attacking()
    {
        while(targetObj != null)
        {
            targetObj.Damaged();

            yield return new WaitForSeconds(1.0f);
        }
    }

    

	public void UpdateDo()
	{
		if (isDamaged)
		{
			Blink();
		}

		if (hp <= 0)
		{
			isDead = true;
			Destroy(gameObject);

            Debug.Log("dfa");
		}
	}

	public void Blink()
	{
		m_elapsedTime += Time.deltaTime * damageEffectSpeed;
		m_elapsedTime = Mathf.Clamp(m_elapsedTime, 0.0f, 2.0f);
		Color color = Color.Lerp(Color.black, Color.white, Mathf.PingPong(m_elapsedTime, 1));

		renderer.material.SetFloat("_R", color.r);
		renderer.material.SetFloat("_G", color.g);
		renderer.material.SetFloat("_B", color.b);

		if (m_elapsedTime >= 2.0f)
		{
			m_elapsedTime = 0.0f;
			isDamaged = false;
		}
	}
}
