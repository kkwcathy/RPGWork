using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjBase : MonoBehaviour
{
    public Bounds bs;

    public Bounds GetBounds()
    {
        return bs;
    }

    public void Init()
    {
		bs.size = Vector3.one;
		//Debug.Log("bounds set");
    }

    public void BoundsUpdate()
    {
        bs.center = transform.position;
    }
}
