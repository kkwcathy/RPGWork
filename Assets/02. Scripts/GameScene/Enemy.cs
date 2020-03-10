using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ★ 나중에 이미지 분리하면 삭제하기

public class Enemy : Character
{
	[SerializeField] List<Character> _playerList = null;
	// Start is called before the first frame update
	void Start()
    {
		
		StartDo();

		_animator = GetComponentInChildren<Animator>();
		// ★ 플레이어들을 모두 가져와 리스트에 넣어줌 나중에 더 다듬기

		Character[] c = GameObject.Find("Team").GetComponentsInChildren<Player>();

		for (int i = 0; i < c.Length; i++)
		{
			_playerList.Add(c[i]);
		}
		

		// ★ 데미지 관련
		uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();

		GameObject hpBar = Instantiate(hpBarPrefab, uiCanvas.transform);

		hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

		var hpBarComp = hpBar.GetComponent<HpBar>();
		hpBarComp.targetTr = transform;
		hpBarComp.offset = hpBarOffset;

		navMeshAgent.isStopped = true;
		StartCoroutine(StartDelay());
	}

	// ★나중에 다른곳으로 빼서 활용
	IEnumerator StartDelay()
	{
		yield return new WaitForSeconds(Utility.spawnDelayTime);

		navMeshAgent.isStopped = false;
		
	}

    // Update is called once per frame
    void Update()
    {
		UpdateDo();

		if (!isDead && !navMeshAgent.isStopped)
		{
			ChangeDestination(_playerList);
		}

		//bs.center = transform.position;
    }

	public override void Damaged()
	{
		base.Damaged();
		hpBarImage.fillAmount = hp / initHp;
	}
}
