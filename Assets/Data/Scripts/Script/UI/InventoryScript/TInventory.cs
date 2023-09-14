using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TInventory : MonoBehaviour
{
    static TInventory instance  = null;
    static bool _invenAct = false;
    private static bool InventoryActivated
    {
        get => _invenAct;
        set
        {
            _invenAct = value;
            instance.GO_InventoryBase.SetActive(_invenAct);
        }
    }
    public int slotSize;

    [Header("바인딩")]
    [SerializeField] private GameObject Go_SlotsParent; // 슬롯의 부모 (Grid Layout Group 필요)
    public GameObject Prefab;            // 슬롯 프리팹
    public GameObject GO_InventoryBase;  // 인벤 (Go_SlotsParent의 부모)

    private GameObject[] SlotsGO;
    private TSlot[] slots;

    private void Awake()
    {
        instance = this;
        CloseInventory();
        CreateSlots();
    }

    private void Start()
    {
        slots = Go_SlotsParent.GetComponentsInChildren<TSlot>();
    }

    private void Update()
    {
        TryOpenInventory();
    }

    public void CreateSlots()
    {
        SlotsGO = new GameObject[slotSize];

        if (Prefab != null)
        {
            for (int i = 0; i < slotSize; i++)
            {
                GameObject NewSlot = Instantiate(Prefab);
                NewSlot.GetComponent<TSlot>();
                NewSlot.name = "ItemSlot" + i;
                NewSlot.transform.SetParent(GO_InventoryBase.transform.GetChild(0).transform);
                SlotsGO[i] = NewSlot;
            }
        }
    }    

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryActivated = !InventoryActivated;            
        }
        /*
        if (InventoryActivated)
            OPenInventory();
        else
            CloseInventory();
        */
    }

    void OPenInventory() => GO_InventoryBase.SetActive(true);
    void CloseInventory() => GO_InventoryBase.SetActive(false);

    public void AcquireItem(Item _item, int _count =1)
    {
        if (Item.ItemType.Equip != _item.Type)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.Name == _item.Name)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item,  _count);
                return;
            }
        }
    }
}
