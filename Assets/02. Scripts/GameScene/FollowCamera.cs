using System.Collections;
using UnityEngine;

// 3인칭 카메라 제어 클래스
public class FollowCamera : MonoBehaviour
{
	private Transform _tr;

	private Transform _player;
	private Transform _enemy;

	private Transform _target;

	public float _moveDamping = 10.0f;

	private Vector3 _followPos;
	private Vector3 _followAmount; // 플레이어와 멀어진 만큼을 나타내는 벡터
 
	[SerializeField] private Vector3 _zoomAmount = new Vector3(2, -5, 3); 
	[SerializeField] private float _zoomDistance = 20.0f; // 메인 플레이어가 적과 이만큼 가까워 질 때 카메라 zoom
	
	void Start()
	{
		_tr = GetComponent<Transform>();
		_target = _player;

		_followAmount = new Vector3(_tr.position.x, _tr.position.y, _tr.position.z);
	}

	private void LateUpdate()
	{
		if(_target != null)
		{
			_followPos = _target.position + _followAmount;

			_tr.position = Vector3.Lerp(_tr.position, _followPos, Time.deltaTime * _moveDamping);
		}
	}

	public void SetMainPlayer(Transform player)
	{
		_target = _player = player;
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

	// wave 시작시 카메라 움직임 바꾸기
	public void ChangeTarget(float showTime)
	{
		StartCoroutine(FocusEnemy(showTime));
	}

	public IEnumerator FocusEnemy(float showTime)
	{
		// 생성된 적 비추기
		_target = _enemy;

		yield return new WaitForSeconds(showTime);

		// 생성된 적을 비추는 시간이 끝나면 다시 플레이어 비추기
		_target = _player;

		yield return new WaitUntil(IsZoomStart);

		// 웨이브가 시작되고 플레이어와 적이 가까워지면 zoom
		Zoom();
	}

	public bool IsZoomStart()
	{
		if(_enemy == null)
		{
			return false;
		}

		return Vector3.Distance(_player.position, _enemy.position) < _zoomDistance; 
	}
}
