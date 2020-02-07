using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    private const string bulletTag = "BULLET";

    private float hp = 100.0f;

    private float initHp = 100.0f;

    private GameObject bloodEffect;

    public GameObject hpBarPrefab;

    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);

    private Canvas uiCanvas;

    private Image hpBarImage;

    // Start is called before the first frame update
    void Start()
    {
        //bloodEffect = Resources.Load<GameObject>("BulletImpactFleshBigEffect");

        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();

        GameObject hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);

        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        var hpBarComp = hpBar.GetComponent<HpBar>();
        hpBarComp.targetTr = transform;
        hpBarComp.offset = hpBarOffset;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetDamaged()
    {
        hpBarImage.fillAmount = hp / initHp;

        //if (hp <= 0.0f)
        //{
        //    GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
        //    hpBarImage.color = Color.clear;

        //}

        //if (collision.collider.tag == bulletTag)
        //{
        //    ShowBloodEffect(collision);
        //    //Destroy(collision.gameObject);
        //    //collision.gameObject.SetActive(false);
        //    //hp -= collision.gameObject.GetComponent<BulletCtrl>().damage;

        //}
    }

    void ShowBloodEffect(Collision coll)
    {
        Vector3 pos = coll.contacts[0].point;

        Vector3 normal = coll.contacts[0].normal;

        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, normal);

        GameObject bloodObj = Instantiate<GameObject>(bloodEffect, pos, rot);
        Destroy(bloodObj, 1.0f);
    }
}
