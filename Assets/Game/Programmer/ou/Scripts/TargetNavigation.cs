using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetNavigation : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private RectTransform rootRectTransform;

    private Rect viewRect;
    // Start is called before the first frame update
    void Start()
    {
        viewRect = GetComponent<RectTransform>().rect;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetTransform.position);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rootRectTransform, screenPos, null, out Vector2 localPoint);

        localPoint.x = Mathf.Clamp(
            localPoint.x, 
            rootRectTransform.rect.xMin + viewRect.width / 2, 
            rootRectTransform.rect.xMax - viewRect.width / 2
            );

        if (0 < screenPos.z)
        {
            localPoint.y = Mathf.Clamp(
                localPoint.y,
                rootRectTransform.rect.yMin + viewRect.height / 2,
                rootRectTransform.rect.yMax - viewRect.height / 2
                );
        }
        else
        {
            localPoint.y = rootRectTransform.rect.yMin + viewRect.height / 2;
        }

        GetComponent<RectTransform>().anchoredPosition = localPoint;
    }
}
