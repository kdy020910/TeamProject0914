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

    [Header("바인딩")]
    public Image itemImage;
    [SerializeField] private Text Text_Count;
    [SerializeField] private GameObject Go_CountImage;

    private void Start()
    {
        //CallRef();
        //구동시 슬롯내 데이터를 모두 비워둡니다.
        //ClearSlot();
    }
    //아이템 아이콘의 투명도 조절 ? 0 : 1
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    //아이템 획득 (_count는 갯수를 의미)
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        ItemCount = _count;
        itemImage.sprite = item.Icon;
        if (item != null) // 아이템이 있을 때만 이미지의 알파값을 활성화하여 표기합니다.
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
        itemImage.sprite = _fish.Icon; // 물고기 데이터의 아이콘 설정

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



    //아이템 갯수
    public void SetSlotCount(int _count)
    {
        ItemCount += _count;
        Text_Count.text = ItemCount.ToString();

        // 아이템 갯수가 0이하라면 슬롯의 모든 값을 비움
        if (ItemCount <= 0)
            ClearSlot();
    }

    // 모든 값을 비움
    public void ClearSlot()
    {
        item = null;
        ItemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        Text_Count.text = "0";
        Go_CountImage.SetActive(false);
    }

    //(슬롯에 있는 아이템의) 마우스 우클릭을 통한 사용
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                /*
                                if(item.Type == Item.ItemType.Equip)
                                {
                                    // 도구 장착 기능

                                    guide.GuideMessage
                                        (item.Name + "을(를) 착용하려 했지만\n" +
                                        "구현되지 않아 아직 착용할 수는 없습니다.");
                                }
                                if (item.Type == Item.ItemType.Default)
                                {
                                    //guide.GuideMessage
                                        (item.Name + "은(는) 직접 사용할 수 없습니다.\n" +
                                        "대신 제작 재료로 이용해보세요.");
                                }
                                if (item.Type == Item.ItemType.Food)
                                {
                                    guide.GuideMessage
                                        (item.Name + "을(를) 사용하였습니다.\n" +
                                        "스태미너가 +" + item.Value + " 회복되었습니다.");

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

    // 슬롯 내 아이템끼리 Swap
    private void ChangeSlot(Item newItem = null)
    {
        Item _tempItem = newItem;
        int _tempItemCount = ItemCount;

        AddItem(TDragSlot.Inst.dragSlot.item, TDragSlot.Inst.dragSlot.ItemCount);
        // 드래그하려는 슬롯에 아이템이 있으면
        // 해당 슬롯의 아이템 값을 임시로 복사하여 저장해두기 위해 인스턴스화 한 뒤,
        // tempItem 이라는 임시 복사본을 통해 슬롯간의 정보를 교환합니다.

        if (_tempItem != null)
            TDragSlot.Inst.dragSlot.AddItem(_tempItem, _tempItemCount);

        else TDragSlot.Inst.dragSlot.ClearSlot();

        //만약 바꾸려는 대상이 없다는 것은 아이템이 없음을 의미하므로 슬롯 정보를 복사하지 않고 비웁니다.
    }
}
/*
    public void CallRef()
    {
        if (guide == null)
        {
            guide = FindObjectOfType<GuideUI>().GetComponent<GuideUI>();

            if (guide == null) // 참조값이 null인가에 대한 이중 탐색
            {
                print
                      ("guide의 참조값을 찾을 수 없어 null이므로 리턴합니다." +
                      "GuideUI Script Component가 부착된 GameObject가 있는지 확인해주세요.");
                return;
            }
        }
        if (guide != null)
            return;
    }
}*/
