using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �κ��丮 ��ũ��Ʈ�� Player���� �������ּ���.
// �κ��丮 UI�� ���� �ְ� �Ǹ�
// �ݾ��� �� false ���¿��� ��ġ�� �о���� ���� �ٽ� ������ �ʽ��ϴ�.
public class TInventory : MonoBehaviour
{
    [Tooltip("������ ������ ������ �Է��մϴ�. �ִ� 40������ �����մϴ�."), Range(1, 40)]
    public int Slotsize;

    [Header("���ε�")]
    public GameObject Prefab;            // ���� ������
    public GameObject InventoryUI;       // �κ� (InventoryContents�� �θ�)
    [SerializeField]
    private GameObject InventoryContents; // ���Ե��� �θ�

    // InventoryContents�� ���Ե��� �θ�μ� Grid Layout Group ������Ʈ�� �ʿ��մϴ�.
    // �ش� ������Ʈ�� ������ ������ �ùٸ��� ���ĵ��� �ʰ� ���Գ��� ������ ä �����˴ϴ�.

    private GameObject[] SlotsGO;
    public TSlot[] Slots;

    // �κ��丮 ��ũ��Ʈ�� �ٸ� ��ũ��Ʈ���� ������ �� ����� ���� �ν��Ͻ�
    //�ʱ�ȭ�� �����մϴ�.
    private void Awake()
    {
        //������ ���� �����մϴ�.
        CreateSlots();
        Slots = InventoryContents.GetComponentsInChildren<TSlot>();
    }

    /// <summary>
    ///  Prefab�� ��ϵ� GameObject(prefab)�� NewSlot���� �����մϴ�. Slotsize�� ������ŭ �����˴ϴ�.
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
    /// ���� �迭�� ���̸�ŭ �������� �����մϴ�.
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

        // �κ��丮�� �� ������ �ִٸ� �� ���Կ� �������� �߰��մϴ�.
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].item == null)
            {
                Slots[i].AddItem(_item, _count);
                return true;
            }
        }

        // ���� �� �� ������ ���� ���� ������ �� �����Ƿ� false�� ��ȯ�մϴ�.
        return false;
    }
}