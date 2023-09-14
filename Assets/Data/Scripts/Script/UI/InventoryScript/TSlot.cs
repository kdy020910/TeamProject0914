using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.UIElements;
using Unity.VisualScripting;

public class TSlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    // private GuideUI guide;
    public Item item;
    public Fish fish;

    public int ItemCount;

    [Header("���ε�")]
    public Image itemImage;
    [SerializeField] private Text Text_Count;
    [SerializeField] private GameObject Go_CountImage;

    private void Start()
    {
        //CallRef();
        //������ ���Գ� �����͸� ��� ����Ӵϴ�.
        //ClearSlot();
    }
    //������ �������� ���� ���� ? 0 : 1
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    //������ ȹ�� (_count�� ������ �ǹ�)
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        ItemCount = _count;
        itemImage.sprite = item.Icon;
        if (item != null) // �������� ���� ���� �̹����� ���İ��� Ȱ��ȭ�Ͽ� ǥ���մϴ�.
            SetColor(1);

        if (item.Type != Item.ItemType.Equip)
        {
            Go_CountImage.SetActive(true);
            Text_Count.text = ItemCount.ToString();
        }
 
        else
        {
            Text_Count.text = "0";
            Go_CountImage.SetActive(false);
        }
    }
    public void Addfish(Fish _fish, int count = 1)
    {
        fish = _fish;
        ItemCount = count;
        itemImage.sprite = _fish.Icon; // ����� �������� ������ ����

        if (fish != null)
            SetColor(1);

        if (fish.Type != Item.ItemType.Fish)
        {
            Go_CountImage.SetActive(true);
            Text_Count.text = ItemCount.ToString();
        }
        else
        {
            Text_Count.text = "0";
            Go_CountImage.SetActive(false);
        }
    }



    //������ ����
    public void SetSlotCount(int _count)
    {
        ItemCount += _count;
        Text_Count.text = ItemCount.ToString();

        // ������ ������ 0���϶�� ������ ��� ���� ���
        if (ItemCount <= 0)
            ClearSlot();
    }

    // ��� ���� ���
    public void ClearSlot()
    {
        item = null;
        ItemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        Text_Count.text = "0";
        Go_CountImage.SetActive(false);
    }

    //(���Կ� �ִ� ��������) ���콺 ��Ŭ���� ���� ���
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                /*
                                if(item.Type == Item.ItemType.Equip)
                                {
                                    // ���� ���� ���

                                    guide.GuideMessage
                                        (item.Name + "��(��) �����Ϸ� ������\n" +
                                        "�������� �ʾ� ���� ������ ���� �����ϴ�.");
                                }
                                if (item.Type == Item.ItemType.Default)
                                {
                                    //guide.GuideMessage
                                        (item.Name + "��(��) ���� ����� �� �����ϴ�.\n" +
                                        "��� ���� ���� �̿��غ�����.");
                                }
                                if (item.Type == Item.ItemType.Food)
                                {
                                    guide.GuideMessage
                                        (item.Name + "��(��) ����Ͽ����ϴ�.\n" +
                                        "���¹̳ʰ� +" + item.Value + " ȸ���Ǿ����ϴ�.");

                                    SetSlotCount(-1);
                                }*/
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            TDragSlot.Inst.dragSlot = this;

            TDragSlot.Inst.DragSetImage(itemImage);

            TDragSlot.Inst.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
            TDragSlot.Inst.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        TDragSlot.Inst.SetColor(0);
        TDragSlot.Inst.dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (TDragSlot.Inst.dragSlot != null)
            ChangeSlot();
    }

    // ���� �� �����۳��� Swap
    private void ChangeSlot(Item newItem = null)
    {
        Item _tempItem = newItem;
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
}
/*
    public void CallRef()
    {
        if (guide == null)
        {
            guide = FindObjectOfType<GuideUI>().GetComponent<GuideUI>();

            if (guide == null) // �������� null�ΰ��� ���� ���� Ž��
            {
                print
                      ("guide�� �������� ã�� �� ���� null�̹Ƿ� �����մϴ�." +
                      "GuideUI Script Component�� ������ GameObject�� �ִ��� Ȯ�����ּ���.");
                return;
            }
        }
        if (guide != null)
            return;
    }
}*/
