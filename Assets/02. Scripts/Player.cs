using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
	[SerializeField] private float speed = 15.0f;

	public float dodgeSpeed = 5.0f;

	[SerializeField] private float dodgeDistance = -5.0f;
	public Vector3 curDesPos;

	Vector3 targetPos;

	private bool isLerpMoving = false;

	Renderer renderer;

	float m_elapsedTime = 0;
	float damageEffectSpeed = 7.0f;

	bool isDamaged = false;

	CharacterController characterController;

	public bool IsChange
	{
		get
		{
			return !EnemyGenerator.isClear && EnemyGenerator.curWayPoint.position != curDesPos;
		}
	}

	NavMeshAgent navMeshAgent;

	private void Awake()
	{
		//IsChange = false;
		curDesPos = transform.position + (Vector3.left * 20 + Vector3.back * 20);
	}


	void Start()
	{
		characterController = GetComponent<CharacterController>();
		navMeshAgent = GetComponent<NavMeshAgent>();
		renderer = GetComponentInChildren<Renderer>();

		// 시작할 땐 첫 웨이브 전까지 ↙ 방향으로 이동
		navMeshAgent.SetDestination(curDesPos);
		//navMeshAgent.SetDestination(wayPoints[0].transform.position);

	}

	public void ChangeDestination()
	{
		curDesPos = EnemyGenerator.curWayPoint.position;

		if (navMeshAgent.enabled)
		{
			navMeshAgent.SetDestination(curDesPos);
		}
	}

	public void Dodge()
	{
		navMeshAgent.enabled = false;

		targetPos = transform.position + (transform.forward * -dodgeDistance);

		isLerpMoving = true;
	}

	public void ShowDamaged()
	{
		isDamaged = true;
	}

	void Update()
	{
		if (isDamaged)
		{
			m_elapsedTime += Time.deltaTime * damageEffectSpeed;
			m_elapsedTime = Mathf.Clamp(m_elapsedTime, 0.0f, 2.0f);
			Color color = Color.Lerp(Color.black, Color.white, Mathf.PingPong(m_elapsedTime, 1));

			renderer.material.SetFloat("_R", color.r);
			renderer.material.SetFloat("_G", color.g);
			renderer.material.SetFloat("_B", color.b);

			if(m_elapsedTime >= 2.0f)
			{
				m_elapsedTime = 0.0f;
				isDamaged = false;
			}
		}


		if (IsChange)
		{
			ChangeDestination();
		}

		if (isLerpMoving)
		{
			Vector3 curPos = transform.position;

			characterController.Move((targetPos - curPos) * Time.deltaTime * dodgeSpeed);

			if (Utility.GetIsNear(curPos, targetPos))
			{
				isLerpMoving = false;
				navMeshAgent.enabled = true;
				navMeshAgent.SetDestination(curDesPos);
			}
		}
	}
}
