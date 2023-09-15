using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField] private float radius;  //반지름
    [SerializeField] private float maxDist; //거리
    private bool pickupActivated = false;
    private RaycastHit hitInfo;

    [SerializeField] private TInventory TheInventory;
    [SerializeField] private LayerMask ItemMask; // 아이템에 대한 레이어마스크 설정

    [SerializeField, Header("바인딩")]
    private Text actionText;

    private void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickup();
        }
    }

    private void CanPickup()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                bool Acquired = TheInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);

                if (Acquired)
                    Destroy(hitInfo.transform.gameObject);

                InfoDisappear();
            }
        }
    }

    private void CheckItem()
    {
        // SphereCast의 범위 내의 Item 레이어가 포함된 게임오브젝트를 판별합니다.
        Vector3 orgPos = transform.position;
        Vector3 Dir = transform.forward;
        if (Physics.SphereCast(orgPos, radius, Dir, out hitInfo, maxDist, ItemMask))
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