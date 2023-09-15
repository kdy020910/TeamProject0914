using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField] private float radius;  //������
    [SerializeField] private float maxDist; //�Ÿ�
    private bool pickupActivated = false;
    private RaycastHit hitInfo;

    [SerializeField] private TInventory TheInventory;
    [SerializeField] private LayerMask ItemMask; // �����ۿ� ���� ���̾��ũ ����

    [SerializeField, Header("���ε�")]
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
        // SphereCast�� ���� ���� Item ���̾ ���Ե� ���ӿ�����Ʈ�� �Ǻ��մϴ�.
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