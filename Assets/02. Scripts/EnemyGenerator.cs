using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

	[SerializeField] private GameObject enemyPrefab;

	public Transform initPosition;
	public static bool isGenerated = false;

    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(GenerateEnemy());
    }

	IEnumerator GenerateEnemy()
	{
		yield return new WaitForSeconds(3.0f); // 첫 생성

		GameObject enemy = Instantiate(enemyPrefab, initPosition);
		isGenerated = true;
	}
    // Update is called once per frame
    void Update()
    {

    }
}
