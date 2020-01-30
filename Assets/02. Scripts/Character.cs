using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public GameObject charModel;
	protected GameObject model;

	SkinnedMeshRenderer smr;
	public Bounds bounds;

	public void GenerateModel()
	{
		model = Instantiate(charModel, transform);
		smr = GetComponentInChildren<SkinnedMeshRenderer>();

	}
}
