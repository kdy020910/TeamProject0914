using UnityEngine;
using UnityEngine.EventSystems;

public class TInventoryUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private bool IsDragging = false;
    private RectTransform _Rtf;
    private Canvas _Canvas;

    private void Start()
    {
        //���� ���� �ҷ��ɴϴ�.
        _Rtf = GetComponent<RectTransform>();
        _Canvas = GetComponentInParent<Canvas>();

        // UI�� ��ġ�� ȭ�� �߾����� �����մϴ�.
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

        // �巡�� ���� UI�� ��ġ�� ������Ʈ
        _Rtf.anchoredPosition += eventData.delta / _Canvas.scaleFactor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsDragging = false;
    }
}
