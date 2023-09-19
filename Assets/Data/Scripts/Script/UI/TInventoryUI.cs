using UnityEngine;
using UnityEngine.EventSystems;

public class TInventoryUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private bool IsDragging = false;
    private RectTransform _Rtf;
    private Canvas _Canvas;

    private void Start()
    {
        //참조 값을 불러옵니다.
        _Rtf = GetComponent<RectTransform>();
        _Canvas = GetComponentInParent<Canvas>();

        // UI의 위치를 화면 중앙으로 설정합니다.
        Vector3 centerOfScreen = new Vector3(Screen.width / 2f, Screen.height / 1.2f, 0f);
        _Rtf.position = centerOfScreen;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!IsDragging)
            return;

        // 드래그 중인 UI의 위치를 업데이트
        _Rtf.anchoredPosition += eventData.delta / _Canvas.scaleFactor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsDragging = false;
    }
}
