using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
	ObjBase objBounds;
	Bounds bs;
    ObjBase[] obj;

    void Start()
    {
		objBounds = transform.GetComponent<ObjBase>();
    }
    
    private void CheckInstersect()
    {
        obj = transform.parent.GetComponent<Player>().enemies;

        foreach(var i in obj)
        {
            if (bs.Intersects(i.bs))
            {
                Debug.Log("dhk");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
		bs = objBounds.GetBounds();
		CheckInstersect();
    }
}
