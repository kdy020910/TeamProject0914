using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TDragSlot : MonoBehaviour
{
    public static TDragSlot Inst;

    public TSlot dragSlot;

    [Header("���ε�"), Tooltip("�� ��ũ��Ʈ�� ���Ե� �ڱ��ڽ��� �����մϴ�.")]
    public Image dragSlotImage;
    
    private void Awake()
    {
        dragSlotImage.raycastTarget = false;
        Init();

        if (dragSlotImage.sprite == null)
        {
            SetColor(0);
            if (dragSlotImage.sprite == null)
            return;
        }
    }

    private void Init()
    {
        if (Inst == null)
        {
            Inst = this;
            if (Inst == null)
            {
                Inst = FindObjectOfType<TDragSlot>();
                Inst = new();
                DontDestroyOnLoad(Inst);
            }
        }
        return;
    }

    public void DragSetImage(Image _itemImage)
    {
        dragSlotImage.sprite = _itemImage.sprite;
        SetColor(1);
    }

    public void SetColor(float _alpha)
    {
        Color color = dragSlotImage.color;
        color.a = _alpha;
        dragSlotImage.color = color;
    }
}