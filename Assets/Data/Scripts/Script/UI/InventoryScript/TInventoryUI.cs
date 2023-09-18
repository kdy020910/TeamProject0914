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

        // ȭ���� �߾� ��ġ�� �����ɴϴ�.
        Vector3 centerOfScreen = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        // UI�� ��ġ�� ȭ�� �߾����� �����մϴ�.
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

        // UI�� ȭ�� ������ ����� �ʵ��� ó�� (��: ȭ�� Ŭ����)
        // ���� ���� ã�� ���Դϴ� �Ф�
        /*Vector3 newPosition = _Rtf.anchoredPosition + eventData.delta / _Canvas.scaleFactor;
        newPosition.x = Mathf.Clamp(newPosition.x, -100.0f, 800.0f); // minX�� maxX�� ȭ�� ������ ���Ǵ� x ����
        newPosition.y = Mathf.Clamp(newPosition.y, -100.0f, 600.0f); // minY�� maxY�� ȭ�� ������ ���Ǵ� y ����
        _Rtf.anchoredPosition = newPosition;*/
        // �ʿ��� ��� ȭ���� ũ��� UI�� ũ�⸦ ����Ͽ� ó��
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsDragging = false;
    }
}