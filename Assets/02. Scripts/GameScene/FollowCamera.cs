using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
	private Transform _tr;

	private Transform _player;
	private Transform _enemy;

	private Transform _target;

	public float _moveDamping = 15.0f;

	bool _isEnemyFocus = false;
	
	private Vector3 _followPos;
	private Vector3 _followAmount;

	[SerializeField] private Vector3 _zoomAmount = new Vector3(2, -5, 3);
	[SerializeField] private float _zoomDistance = 20.0f;
	
	void Start()
	{
		_tr = GetComponent<Transform>();
		_target = _player;

		_followAmount = new Vector3(_tr.position.x, _tr.position.y, _tr.position.z);
	}

	private void LateUpdate()
	{
		_followPos = _target.position + _followAmount;

		_tr.position = Vector3.Lerp(_tr.position, _followPos, Time.deltaTime * _moveDamping);
	}

	public void SetMainPlayer(Transform player)
	{
		_player = player;
	}

	public void SetMainEnemy(Transform enemy)
	{
		_enemy = enemy;
	}

	public void Zoom()
	{
		_followAmount += _zoomAmount;
	}

	public void Unzoom()
	{
		_followAmount -= _zoomAmount;
	}

	public void ChangeTarget(float showTime)
	{
		_isEnemyFocus = true;

		StartCoroutine(FocusEnemy(showTime));
	}

	public IEnumerator FocusEnemy(float showTime)
	{
		_target = _enemy;

		yield return new WaitForSeconds(showTime);

		_target = _player;

		yield return new WaitUntil(IsZoomStart);

		Zoom();
	}

	public bool IsZoomStart()
	{
		return Vector3.Distance(_player.position, _enemy.position) < _zoomDistance; 
	}
}
