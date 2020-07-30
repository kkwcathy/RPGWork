using UnityEngine;

// Organize Scene에서 모델 부분 비추는 카메라의 Viewport를 화면 비율에 맞게 제어하는 클래스
public class ModelViewCam : MonoBehaviour
{
	private Camera _camera;

	private const int BASE_WIDTH = 1920;
	private const int BASE_HEIGHT = 1080;

    void Start()
    {
		_camera = GetComponent<Camera>();

		CurrectRatio();
	}

	private void CurrectRatio()
	{
		int width = Screen.width;
		int height = Screen.height;

		// 기존 작업한 비율로 구한 새 스크린 width
		int baseWidth = (int)((double)height * ((double)BASE_WIDTH / (double)BASE_HEIGHT));

		// 현재 스크린 width 와 기존 비율로 구한 width 차이
		int offset = (width - baseWidth) / 2;
		
		if(offset == 0)
		{
			Debug.Log("비율 일치");
			return;
		}

		Rect newRect = new Rect(_camera.rect);

		// 카메라 ViewPort Rect 의 x 값 조정
		//		(현재 x 값에서 offset만큼 더하기) 
		newRect.x += (1.0f / width) * offset;

		// 카메라 ViewPort Rect 의 width 값 조정
		//		(기존 비율로 구한 width 일 때의 가로 길이로 바꿈)
		newRect.width = (1.0f / width) * (baseWidth * _camera.rect.width);

		_camera.rect = newRect;

	}

}
