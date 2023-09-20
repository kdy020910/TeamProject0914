using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : SystemProPerty
{
    [SerializeField] private float range; // ���� �Ÿ�
    private bool pickupActivated = false;
    private RaycastHit hitInfo;

    [SerializeField] private TInventory TheInventory;
    [SerializeField] private LayerMask ItemMask; // �����ۿ� ���� ���̾��ũ ����

    [SerializeField, Header("���ε�")]
    private Text actionText;

    public Player player;
    private void Update()
    {
        CheckItem();
        TryAction();
    }

    public void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickup();
        }
    }

    public void CanPickup()
    {
        if (pickupActivated)
        {
            myAnim.SetTrigger("Picking");
            if (hitInfo.transform != null)
            {
                bool Acquired = TheInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);

                if (Acquired)
                    Destroy(hitInfo.transform.gameObject);

                InfoDisappear();
            }
        }
    }

    public void CheckItem()
    {
        Vector3 y = new Vector3(0, 0.3f, 0);

        if (Physics.Raycast(transform.position, player.dirVec, out hitInfo, range, ItemMask))
        {
            ItemInfoAppear();
        }
        else
            InfoDisappear();
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.transform.parent.gameObject.SetActive(true);
        actionText.gameObject.SetActive(true);
        actionText.text = " Ű�� ���� \n" + hitInfo.transform.GetComponent<ItemPickUp>().item.Name + " ���� ";
    }

    private void InfoDisappear()
    {
        pickupActivated = false;
        actionText.transform.parent.gameObject.SetActive(false);
        actionText.gameObject.SetActive(false);
    }
    /*
    private CollectionSystem CollectIdxSystem = new CollectionSystem();

    // ������ ������ ���� ���� Ȱ��ȭ
    private void Collecting()
    {
        CollectionSystem CollecSystem = new CollectionSystem();

        CollectionItem cItem = new CollectionItem { ID = 1, Name = "Item" };
        // ������ ���� ���� Ȯ��
        bool isItemCollected = CollectIdxSystem.IsItemCollected(cItem); // true
        // ������ ����
        if (isItemCollected)
            CollectIdxSystem.CollectItem(cItem);
    }*/
}