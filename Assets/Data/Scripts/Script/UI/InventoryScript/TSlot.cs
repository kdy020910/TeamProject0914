using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// TSlot�� ������ ������ ��ɵ��� ��� ������ �ֽ��ϴ�.
/// ������ �巡��, ������ ���, ������ ������ ���� ī��Ʈ ��.
/// </summary>
public class TSlot : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private GuideUI guide;
    private TTooltip tooltip;
    public Item item;
    public int ItemCount;

    [Header("���ε�")]
    public Image itemImage;
    [SerializeField]
    private Text Text_Count;
    [SerializeField]
    private GameObject Go_CountImage;

    // �ʱ�ȭ�� �����մϴ�.
    private void Awake()
    {
        // ���� Script Component�� ������ ������Ʈ ��ü�� ã�� �ҷ��� �� �����մϴ�.
        if (guide == null || tooltip == null)
        {
            guide = FindObjectOfType<GuideUI>().GetComponent<GuideUI>();
            tooltip = FindObjectOfType<TTooltip>().GetComponent<TTooltip>();
            if (guide == null || tooltip == null) // null ����ó��
            {
                Debug.Log("���! GuideUI �Ǵ� TTooltip ��ũ��Ʈ�� ������ ���ӿ�����Ʈ�� ã�� �� �����ϴ�.");
                return;
            }
        }

        if (item == null)   // ���Կ� item ���� ���� ���¶��
        {
            SetColor(0);    // ������ ������ �̹����� ��Ȱ��ȭ�ϰ�,
            Go_CountImage.SetActive(false);
            // �������� ������ ���� ��� �̹����� �Բ� ��Ȱ��ȭ�մϴ�.
        }
    }

    /// <summary>
    /// ������ �������� ������ �����մϴ�. 0: ����, 1: ������
    /// </summary>
    /// <param name="_alpha"></param>
    private void SetColor(float _alpha)
    {
        if (!CompareTag("MountSlot"))
        {
            Color color = itemImage.color;
            color.a = _alpha;
            itemImage.color = color;
        }
    }

    /// <summary>
    /// �������� ȹ���մϴ�. (_count�� ������ �ǹ��մϴ�)
    /// </summary>
    /// <param name="_item"></param>
    /// <param name="_count"></param>
    public void AddItem(Item _item, int _count = 1)
    {
        // Item Ŭ������ ������� �����͸� �����Ͽ� ���� ����ϴ�.
        item = _item;
        ItemCount = _count;
        itemImage.sprite = item.Icon;

        if (item != null) // �������� ���� ���� �̹����� ���İ��� Ȱ��ȭ�Ͽ� ǥ���մϴ�.
            SetColor(1);

       
            Go_CountImage.SetActive(true);
            Text_Count.text = ItemCount.ToString();
        
        
            //Text_Count.text = "0";
            //Go_CountImage.SetActive(false);
        
    }

    /// <summary>
    /// �� ������ ������ ������ ���ϴ�.
    /// </summary>
    /// <param name="_count"></param>
    public void SetSlotCount(int _count, string itemName = null)
    {
        ItemCount += _count;
        Text_Count.text = ItemCount.ToString();

        // ������ ������ 0���϶�� ������ ��� ���� ���
        if (ItemCount <= 0)
            ClearSlot();

        // ������ �̸��� �־����� �ش� �������� ������ ����
        if (!string.IsNullOrEmpty(itemName))
        {
            // �κ��丮���� �ش� �������� ã�Ƽ� ������ ����
            TSlot[] inventorySlots = FindObjectsOfType<TSlot>();
            foreach (TSlot slot in inventorySlots)
            {
                if (slot.item != null && slot.item.Name == itemName)
                {
                    slot.ItemCount += _count;
                    slot.Text_Count.text = slot.ItemCount.ToString();

                    if (slot.ItemCount <= 0)
                        slot.ClearSlot();
                }
            }
        }
    }


    /// <summary>
    /// ������ ���� ��� ���� ������ ���ϴ�.
    /// </summary>
    public void ClearSlot()
    {
        item = null;
        ItemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        Text_Count.text = "0";
        Go_CountImage.SetActive(false);
    }

    /// <summary>
    /// ���� ���� ���콺�� ��Ŭ���� �� �����մϴ�.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            UseItem();
    }

    /// <summary>
    /// ���Կ� �������� ���� ���, �������� ����մϴ�.
    /// </summary>
    public void UseItem()
    {
        if (item != null)
        {
            if (item.Type == Item.ItemType.Equip)
            {
                // ���� ���� ���
                guide.GuideMessage
                    (item.Name + "�� �����Ϸ���\n" +
                    "���������� �Űܼ� ������ּ���.");
            }
            else if (item.Type == Item.ItemType.Default)
            {
                guide.GuideMessage
                    (item.Name + "��(��) ���� ����� �� �����ϴ�.\n" +
                    "��� ���� ���� �̿��غ�����.");
            }
           /* else if (item.Type == Item.ItemType.Food)
            {
                guide.GuideMessage
                    (item.Name + "��(��) ����Ͽ����ϴ�.\n" +
                    "���¹̳ʰ� +" + item.Value + " ȸ���Ǿ����ϴ�.");

                // ��� ������ �������� ���� �� SlotCount ���� 1�� �����ϴ�.
                SetSlotCount(-1);
            }*/
        }
    }

    /// <summary>
    /// �������� �巡�׸� ������ �� ȣ��˴ϴ�.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            TDragSlot.Inst.dragSlot = this;
            TDragSlot.Inst.DragSetImage(itemImage);

            TDragSlot.Inst.transform.position = eventData.position;
        }
    }

    /// <summary>
    /// �巡������ �� ��ġ���� ���콺 ������ ��ġ������ �����մϴ�.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
            TDragSlot.Inst.transform.position = eventData.position;
    }

    /// <summary>
    /// �巡�׸� �Ϸ��Ͽ��� �� ȣ��˴ϴ�.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        TDragSlot.Inst.SetColor(0);
        TDragSlot.Inst.dragSlot = null;
        //������ TSlot ��ũ��Ʈ�� ���� ��ü�� ���, ó�� �巡�׸� �����ߴ� ��ġ�� �ǵ��ư��ϴ�.
    }

    /// <summary>
    /// �巡���� ��ġ�� ���� ���� ��ũ��Ʈ�� ���� ��ü�� �� ȣ��˴ϴ�.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        if (TDragSlot.Inst.dragSlot != null)
            ChangeSlot();
    }

    // ���Կ� �������� �ִٸ� ���԰��� ������ ��ġ�� ��ȯ�մϴ�.
    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = ItemCount;

        AddItem(TDragSlot.Inst.dragSlot.item, TDragSlot.Inst.dragSlot.ItemCount);
        // �巡���Ϸ��� ���Կ� �������� ������
        // �ش� ������ ������ ���� �ӽ÷� �����Ͽ� �����صα� ���� �ν��Ͻ�ȭ �� ��,
        // tempItem �̶�� �ӽ� ���纻�� ���� ���԰��� ������ ��ȯ�մϴ�.

        if (_tempItem != null)
            TDragSlot.Inst.dragSlot.AddItem(_tempItem, _tempItemCount);

        else TDragSlot.Inst.dragSlot.ClearSlot();
        //���� �ٲٷ��� ����� ���ٴ� ���� �������� ������ �ǹ��ϹǷ� ���� ������ �������� �ʰ� ���ϴ�.
    }

    /// <summary>
    /// ������ ���� ǥ��. �������� �ִ� ���Կ� ���콺�� ������ �� ȣ��˴ϴ�.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
            tooltip.ShowTooltip(item);
        if (item == null)
            return;
    }

    /// <summary>
    /// ������ ���� ����. �������� ���� �����̰ų�, ������ ������ ��ġ���� ���콺�� ����� ȣ��˴ϴ�.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (item != null)
            tooltip.HideTooltip();
        else
            tooltip.HideTooltip();
    }
}