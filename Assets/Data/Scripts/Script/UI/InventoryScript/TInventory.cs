using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 인벤토리 스크립트는 Player에게 부착해주세요.
// 인벤토리 UI에 직접 넣게 되면
// 닫았을 때 false 상태여서 위치를 읽어오지 못해 다시 열리지 않습니다.
public class TInventory : MonoBehaviour
{
    [Tooltip("생성할 슬롯의 갯수를 입력합니다. 최대 40개까지 가능합니다."), Range(1, 40)]
    public int Slotsize;

    [Header("바인딩")]
    public GameObject Prefab;            // 슬롯 프리팹
    public GameObject InventoryUI;       // 인벤 (InventoryContents의 부모)
    [SerializeField]
    private GameObject InventoryContents; // 슬롯들의 부모

    // InventoryContents는 슬롯들의 부모로서 Grid Layout Group 컴포넌트가 필요합니다.
    // 해당 컴포넌트가 없으면 슬롯이 올바르게 정렬되지 않고 슬롯끼리 겹쳐진 채 생성됩니다.

    private GameObject[] SlotsGO;
    private TSlot[] Slots;

    //초기화를 진행합니다.
    private void Awake()
    {
        //슬롯을 먼저 생성합니다.
        CreateSlots();
        Slots = InventoryContents.GetComponentsInChildren<TSlot>();
    }

    /// <summary>
    ///  Prefab에 등록된 GameObject(prefab)를 NewSlot으로 생성합니다. Slotsize의 갯수만큼 생성됩니다.
    /// </summary>
    public void CreateSlots()
    {
        SlotsGO = new GameObject[Slotsize];

        if (Prefab != null)
        {
            for (int i = 0; i < Slotsize; i++)
            {
                GameObject NewSlot = Instantiate(Prefab);
                NewSlot.GetComponent<TSlot>();
                NewSlot.name = "ItemSlot" + i;
                NewSlot.transform.SetParent(InventoryUI.transform.GetChild(0).transform);
                SlotsGO[i] = NewSlot;
            }
        }
    }

    /// <summary>
    /// 슬롯 배열의 길이만큼 아이템을 습득합니다.
    /// </summary>
    /// <param name="_item"></param>
    /// <param name="_count"></param>
    public bool AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equip != _item.Type)
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                if (Slots[i].item != null)
                {
                    if (Slots[i].item.Name == _item.Name)
                    {
                        Slots[i].SetSlotCount(_count);
                        return true;
                    }
                }
            }
        }

        // 인벤토리에 빈 공간이 있다면 각 슬롯에 아이템을 추가합니다.
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].item == null)
            {
                Slots[i].AddItem(_item, _count);
                return true;
            }
        }

        // 슬롯 내 빈 공간이 없을 때는 습득할 수 없으므로 false를 반환합니다.
        return false;
    }
}