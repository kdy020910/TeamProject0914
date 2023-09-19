using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectionSlot : MonoBehaviour
{
    public enum CollectType { Tool, Crops, Fish }

    public TextMeshPro ItemNameTxT;
    public TextMeshPro ItemDescTxT;
    public Image CollectIcon;
    public Button CollectButton;

    [Header("바인딩 - 수집될 아이템 정보")]
    [SerializeField] private CollectType collectType;
    [SerializeField] private Item CollectItem;

    // 도감 아이템 데이터 설정
    public void SetItem(Item item)
    {
        //매개변수의 참조값 지정
        CollectItem = item;

        // UI 요소에 아이템 정보 표시
        ItemNameTxT.text = CollectItem.Name;
        ItemDescTxT.text = CollectItem.Desc;
        CollectIcon.sprite = CollectItem.Icon;

        if (CollectIcon.sprite == null)
            return;
    }

    // 아이템 수집 여부 확인
    private bool IsItemCollected()
    {
        // 여기에 도감 시스템의 수집 여부 확인 로직을 작성
        return CollectionSystem.Instance.IsItemCollected(CollectItem);
    }

    /*private void UpdateIcon(Item item)
    {
        // 아이템과 관련된 도감 슬롯을 찾아서
        // 해당 도감 슬롯의 이미지 컴포넌트의 스프라이트를 변경
        foreach (var slot in )
        {
            if (slot.GetAssociatedItem() == item)
            {
                if (!IsItemCollected())
                { 
                    CollectIcon.color = new Color(0f, 0f, 0f, 1f);
                    ItemNameTxT.text = "???";
                    ItemDescTxT.text = "아직 수집되지 않은 정보입니다.";
                }

                if (IsItemCollected())
                    CollectIcon.color = new Color(1f, 1f, 1f, 1f);

                break;
            }
        }
    }*/

    public Item GetCollectItem()
    {
        return CollectItem;
    }
}