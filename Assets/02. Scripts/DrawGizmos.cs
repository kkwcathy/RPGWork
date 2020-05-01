using UnityEngine;

// Gizmos 그리기 클래스 (필요한 object에만 추가)
public class DrawGizmos : MonoBehaviour
{
	public Color color = Color.red;
	public float radius = 0.1f;

	private void OnDrawGizmos()
	{
		Gizmos.color = color;
		Gizmos.DrawSphere(transform.position, radius);
	}
}
