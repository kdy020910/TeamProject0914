using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField] private float range; // 습득 거리
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
            if(hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.Name + "을 획득하였습니다");
                TheInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitInfo.transform.gameObject);
                InfoDisappear();
            }
        }
    }

    private void CheckItem()
    {
        Vector3 y = new(0, 0, 0);
        if (Physics.Raycast(transform.position + y, transform.TransformDirection(Vector3.forward), out hitInfo, range, ItemMask))
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
}
