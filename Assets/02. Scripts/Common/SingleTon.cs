using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 싱글톤 상위 클래스
public class SingleTon<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T _instance;

	public static T Instance
	{
		get
		{
			if(_instance == null)
			{
				string objName = GetObjName();

				GameObject managerObj = GameObject.Find(objName);

				if(managerObj == null)
				{
					managerObj = new GameObject(objName);
					DontDestroyOnLoad(managerObj);
				}

				_instance = managerObj.AddComponent<T>();
				Debug.Log(typeof(T).Name + " 생성 완료");
			}

			return _instance;
		}
	}

	private static string GetObjName()
	{
		if (typeof(T).Name.Contains("Manager"))
		{
			return "ManagerObject";
		}

		return typeof(T).Name;
	}
}
