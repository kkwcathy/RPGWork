using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 나중에 이미지 분리하면 삭제하기

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

    // 데미지 관련

    private float hp = 100.0f;
    private float initHp = 100.0f;

    public GameObject hpBarPrefab;
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    private Canvas uiCanvas;
    private Image hpBarImage;


    // 공격 관련

    [SerializeField] GameObject basicSkillEffect;

    

    public void GenerateModel()
	{
		model = Instantiate(charModel, transform);

		bs.center = transform.position;

		bs.size = Vector3.one;
		renderer = GetComponentInChildren<Renderer>();


        //데미지 관련
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();

        GameObject hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);

        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        var hpBarComp = hpBar.GetComponent<HpBar>();
        hpBarComp.targetTr = transform;
        hpBarComp.offset = hpBarOffset;

    }

	public void Damaged()
	{
		hp -= 10;
        hpBarImage.fillAmount = hp / initHp;

        if (!isDamaged)
		{
			isDamaged = true;
		}
	}

	public void Attack()
	{
        StartCoroutine(Attacking());
	}

    public void BasicSkillAttack()
    {
        Instantiate(basicSkillEffect, transform.position, transform.rotation);
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
