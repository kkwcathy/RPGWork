using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 이펙트 부분만 나중에 참고용
public class EnemyDamage : MonoBehaviour
{
    private const string bulletTag = "BULLET";

    

    private GameObject bloodEffect;

    

    // Start is called before the first frame update
    void Start()
    {
        //bloodEffect = Resources.Load<GameObject>("BulletImpactFleshBigEffect");

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetDamaged()
    {
        

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
