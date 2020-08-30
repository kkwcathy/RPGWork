using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIPool : MonoBehaviour
{
	[SerializeField] private GameObject _damagetextPrefab;

	private Queue<DamageText> _damageTextPool;
	[SerializeField] private const int POOL_AMOUNT = 5;

	void Start()
    {
		StartDo();
    }

	private void StartDo()
	{
		_damageTextPool = new Queue<DamageText>();

		for (int i = 0; i < POOL_AMOUNT; ++i)
		{
			DamageText newText = SpawnDamageText();

			_damageTextPool.Enqueue(newText);
		}
		
	}

	public DamageText GetAvailableText()
	{
		DamageText curText = _damageTextPool.Peek();

		if(curText.IsTextActive)
		{
			curText = SpawnDamageText();
		}
		else
		{
			_damageTextPool.Dequeue();
		}

		_damageTextPool.Enqueue(curText);

		return curText;
	}

	private DamageText SpawnDamageText()
	{
		GameObject newTextObj = Instantiate(_damagetextPrefab, transform);
		newTextObj.SetActive(false);

		DamageText newText = newTextObj.GetComponent<DamageText>();
		
		return newText;
	}
}
