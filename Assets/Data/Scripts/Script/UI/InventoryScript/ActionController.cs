using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : SystemProPerty
{
    [SerializeField] private float range; // 습득 거리
    private bool pickupActivated = false;
    private RaycastHit hitInfo;

    [SerializeField] private TInventory TheInventory;
    [SerializeField] private LayerMask ItemMask; // 아이템에 대한 레이어마스크 설정

    [SerializeField, Header("바인딩")]
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
        actionText.text = " 키를 눌러 \n" + hitInfo.transform.GetComponent<ItemPickUp>().item.Name + " 습득 ";
    }

    private void InfoDisappear()
    {
        pickupActivated = false;
        actionText.transform.parent.gameObject.SetActive(false);
        actionText.gameObject.SetActive(false);
    }
    /*
    private CollectionSystem CollectIdxSystem = new CollectionSystem();

    // 아이템 수집에 따른 도감 활성화
    private void Collecting()
    {
        CollectionSystem CollecSystem = new CollectionSystem();

        CollectionItem cItem = new CollectionItem { ID = 1, Name = "Item" };
        // 아이템 수집 여부 확인
        bool isItemCollected = CollectIdxSystem.IsItemCollected(cItem); // true
        // 아이템 수집
        if (isItemCollected)
            CollectIdxSystem.CollectItem(cItem);
    }*/
}