using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// TSlot은 아이템 슬롯의 기능들을 모두 가지고 있습니다.
/// 아이템 드래그, 아이템 사용, 아이템 갯수에 대한 카운트 등.
/// </summary>
public class TSlot : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    #region 필드값과 해시값
    private GuideUI guide;
    private TTooltip tooltip;
    public Item item;
    public int ItemCount;

    // 해싱 변환
    private readonly int hashTag_tc = "TrashCan".GetHashCode();
    private readonly int hashTag_ms = "MountSlot".GetHashCode();
    #endregion

    [Header("바인딩")]
    public Image itemImage;
    [SerializeField]
    private GameObject Go_CountImage;
    [SerializeField]
    private Text Text_Count;

    #region Awake - 초기화 진행
    // 초기화를 진행합니다.
    private void Awake()
    {
        // 다음 Script Component가 부착된 오브젝트 객체를 찾아 불러온 뒤 참조합니다.
        if (guide == null || tooltip == null)
        {
            guide = FindObjectOfType<GuideUI>().GetComponent<GuideUI>();
            tooltip = FindObjectOfType<TTooltip>().GetComponent<TTooltip>();

            if (guide == null || tooltip == null)
            {
                Debug.Log("경고! GuideUI 또는 TTooltip 스크립트를 포함한 게임오브젝트를 찾을 수 없습니다.");
                return;
            }
        }

        // transCan을 예외처리하여 참조값을 초기화하고 리턴합니다.
        if (gameObject.tag.GetHashCode() == hashTag_tc)
        {
            Go_CountImage = null;
            Text_Count = null;
            return;
        }

        // 슬롯에 item 값이 없는 상태라면 슬롯이 갖는 모든 이미지를 비활성화합니다.
        if (item == null)
        {
            SetColor(0);
            Go_CountImage.SetActive(false);
        }
    }
    #endregion

    #region 아이템 획득, 갯수, 비우기, 이미지 알파값 표시
    /// <summary>
    /// 아이템 아이콘의 투명도를 조절합니다. 0: 투명, 1: 불투명
    /// </summary>
    /// <param name="_alpha"></param>
    private void SetColor(float _alpha)
    {
        //태그를 통해 mountSlot과 trashCan을 예외처리합니다.
        if (gameObject.tag.GetHashCode() != hashTag_ms
            && gameObject.tag.GetHashCode() != hashTag_tc)
        {
            Color color = itemImage.color;
            color.a = _alpha;
            itemImage.color = color;
        }
    }

    /// <summary>
    /// 아이템을 획득합니다. (_count는 갯수를 의미합니다)
    /// </summary>
    /// <param name="_item"></param>
    /// <param name="_count"></param>
    public void AddItem(Item _item, int _count = 1)
    {
        // Item 클래스로 만들어진 데이터를 참조하여 값을 담습니다.
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

    /// <summary>
    /// 각 슬롯의 아이템 갯수를 셉니다.
    /// </summary>
    /// <param name="_count"></param>
    public void SetSlotCount(int _count)
    {
        ItemCount += _count;
        Text_Count.text = ItemCount.ToString();

        // 아이템 갯수가 0이하라면 슬롯의 모든 값을 비움
        if (ItemCount <= 0)
            ClearSlot();
    }

    /// <summary>
    /// 슬롯이 갖는 모든 값을 비웁니다. (같은 의미 Remove)
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
    #endregion

    #region 마우스 우클릭 이벤트와 아이템 사용 메서드
    /// <summary>
    /// 슬롯 위에 마우스를 우클릭할 때 동작합니다.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            UseItem();
    }

    /// <summary>
    /// 슬롯에 아이템이 있을 경우, 아이템을 사용합니다.
    /// </summary>
    public void UseItem()
    {
        if (item != null)
        {
            if (item.Type == Item.ItemType.Equip)
            {
                // 도구 장착 기능
                guide.GuideMessage
                    (item.Name + "을 착용하려면\n" +
                    "퀵슬롯으로 옮겨서 사용해주세요.");
            }
            else if (item.Type == Item.ItemType.Default)
            {
                guide.GuideMessage
                    (item.Name + "은(는) 직접 사용할 수 없습니다.\n" +
                    "대신 제작 재료로 이용해보세요.");
            }
            /* else if (item.Type == Item.ItemType.Food)
            {
                guide.GuideMessage
                    (item.Name + "을(를) 사용하였습니다.\n" +
                    "스태미너가 +" + item.Value + " 회복되었습니다.");

                // 사용 가능한 아이템을 썼을 때 SlotCount 값을 1씩 내립니다.
                SetSlotCount(-1);
            }*/
        }
    }
    #endregion

    #region 드래그 시작부터 완료까지의 인터페이스
    /// <summary>
    /// 아이템의 드래그를 시작할 때 호출됩니다.
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
    /// 드래그중일 때 위치값을 마우스 포인터 위치값으로 유지합니다.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
            TDragSlot.Inst.transform.position = eventData.position;
    }

    /// <summary>
    /// 드래그를 완료하였을 때 호출됩니다.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        TDragSlot.Inst.SetColor(0);
        TDragSlot.Inst.dragSlot = null;
        //동일한 TSlot 스크립트가 없는 개체인 경우, 처음 드래그를 시작했던 위치로 되돌아갑니다.
    }

    /// <summary>
    /// 드래그한 위치가 같은 슬롯 스크립트를 가진 개체일 때 호출됩니다.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        if (TDragSlot.Inst.dragSlot != null)
        {
            // TrashCan에 아이템을 드래그할 때, 아이템 삭제
            if (gameObject.tag.GetHashCode() == hashTag_tc)
                TDragSlot.Inst.dragSlot.ClearSlot();
            else ChangeSlot();
        }
    }
    #endregion

    #region 슬롯간의 아이템값 교환 메서드
    // 슬롯에 아이템이 있다면 슬롯간의 아이템 위치를 교환합니다.
    private void ChangeSlot()
    {
        Item _tempItem = item;
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
    #endregion

    #region 마우스 오버 시작과 끝일 때 아이템 툴팁 호출을 포함하는 메서드
    /// <summary>
    /// 아이템 툴팁 표시. 아이템이 있는 슬롯에 마우스를 오버할 때 호출됩니다.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 orgPos = transform.position;

        if (item == null)
            return;
        else if (gameObject.tag.GetHashCode() == hashTag_ms)
            return;
        //if (item != null)
            //tooltip.ShowTooltip(item, orgPos);
    }

    /// <summary>
    /// 아이템 툴팁 숨김. 아이템이 없는 슬롯이거나, 슬롯의 아이템 위치에서 마우스가 벗어나면 호출됩니다.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (item != null)
            tooltip.HideTooltip();
        else
            return;
    }
    #endregion
}