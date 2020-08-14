using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovingGameUI 
{
	void SetUIPosition(
		Canvas uiCanvas, 
		RectTransform uiCanvasTr,
		Transform targetTr,
		float offset);

	void SetUIContent(float amount);

	void MoveUI();
}
