using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    private Camera uiCamera;

    private Canvas uiCanvas;

    private RectTransform rectParent;

    private RectTransform rectHp;

    [HideInInspector]
    public Vector3 offset = Vector3.zero;

    [HideInInspector]
    public Transform targetTr;

    // Start is called before the first frame update
    void Start()
    {
        uiCanvas = GetComponentInParent<Canvas>();
        uiCamera = uiCanvas.worldCamera;

        rectParent = uiCanvas.GetComponent<RectTransform>();
        rectHp = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset);

        if (screenPos.z < 0.0f)
        {
            screenPos *= -1.0f;
        }

        var localPos = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);
        rectHp.localPosition = localPos;
    }
}
