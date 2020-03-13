using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ★ 나중에 이미지 분리하면 삭제하기

					  
// 많이 수정 예정
public class CharacterUI : MonoBehaviour
{
	public GameObject hpBarPrefab;
	public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
	protected Canvas uiCanvas;
	protected Image hpBarImage;

	protected float hp = 100.0f;
	protected float initHp = 100.0f;


	// Start is called before the first frame update
	void Start()
    {
		uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();

		GameObject hpBar = Instantiate(hpBarPrefab, uiCanvas.transform);

		hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

		var hpBarComp = hpBar.GetComponent<HpBar>();
		hpBarComp.targetTr = transform;
		hpBarComp.offset = hpBarOffset;
	}

	public void Damaged()
	{
		hpBarImage.fillAmount = hp / initHp;
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
