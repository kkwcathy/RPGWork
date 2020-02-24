using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI; // 나중에 이미지 분리하면 삭제하기

enum eState
{
    Run,
    Attack,
    Pause,
}

public class Character : ObjBase
{
	public GameObject charModel;
	protected GameObject model;
    
	protected Renderer objRenderer;

	protected bool isDamaged = false;

	public bool isDead = false;

	protected float elapsedTime = 0;
	protected float damageEffectSpeed = 10.0f;

	protected Character targetObj = null;

    protected NavMeshAgent navMeshAgent;

    public Character godjiulguya = null;

    // 데미지 관련

    private float hp = 100.0f;
    private float initHp = 100.0f;

    public GameObject hpBarPrefab;
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    private Canvas uiCanvas;
    private Image hpBarImage;


    // 공격 관련

    [SerializeField] GameObject basicSkillEffect = null;

    public void GenerateModel()
	{
		model = Instantiate(charModel, transform);

		bs.center = transform.position;

		bs.size = Vector3.one;
        objRenderer = GetComponentInChildren<Renderer>();


        // ★ 데미지 관련
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();

        GameObject hpBar = Instantiate(hpBarPrefab, uiCanvas.transform);

        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        var hpBarComp = hpBar.GetComponent<HpBar>();
        hpBarComp.targetTr = transform;
        hpBarComp.offset = hpBarOffset;

    }

    public void ChangeDestination(List<Character> targetList)
    {
        Character target = null;

        for (int i = 0; i < targetList.Count; ++i)
        {
            if (target == null
                || Vector3.Distance(target.transform.position, transform.position) >
                   Vector3.Distance(targetList[i].transform.position, transform.position))
            {
                target = targetList[i];
            }
        }

        if (target != null)
        {
            godjiulguya = target;
            navMeshAgent.SetDestination(target.transform.position);
        }
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
        GameObject skillEffect = Instantiate(basicSkillEffect, transform.position, transform.rotation);
        skillEffect.transform.parent = transform;

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

            Debug.Log("dead");
		}
	}

	public void Blink()
	{
		elapsedTime += Time.deltaTime * damageEffectSpeed;
		elapsedTime = Mathf.Clamp(elapsedTime, 0.0f, 2.0f);
		Color color = Color.Lerp(Color.black, Color.white, Mathf.PingPong(elapsedTime, 1));

		objRenderer.material.SetFloat("_R", color.r);
		objRenderer.material.SetFloat("_G", color.g);
        objRenderer.material.SetFloat("_B", color.b);

		if (elapsedTime >= 2.0f)
		{
			elapsedTime = 0.0f;
			isDamaged = false;
		}
	}
}
