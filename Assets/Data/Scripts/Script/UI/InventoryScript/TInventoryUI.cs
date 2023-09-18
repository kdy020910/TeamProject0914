using UnityEngine;
using UnityEngine.EventSystems;

public class TInventoryUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private bool IsDragging = false;
    private RectTransform _Rtf;
    private Canvas _Canvas;

    private void Start()
    {
        _Rtf = GetComponent<RectTransform>();
        _Canvas = GetComponentInParent<Canvas>();

        // 화면의 중앙 위치를 가져옵니다.
        Vector3 centerOfScreen = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        // UI의 위치를 화면 중앙으로 설정합니다.
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

        // UI가 화면 밖으로 벗어나지 않도록 처리 (예: 화면 클램핑)
        // 아직 값을 찾는 중입니다 ㅠㅜ
        /*Vector3 newPosition = _Rtf.anchoredPosition + eventData.delta / _Canvas.scaleFactor;
        newPosition.x = Mathf.Clamp(newPosition.x, -100.0f, 800.0f); // minX와 maxX는 화면 내에서 허용되는 x 범위
        newPosition.y = Mathf.Clamp(newPosition.y, -100.0f, 600.0f); // minY와 maxY는 화면 내에서 허용되는 y 범위
        _Rtf.anchoredPosition = newPosition;*/
        // 필요한 경우 화면의 크기와 UI의 크기를 고려하여 처리
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsDragging = false;
    }
}