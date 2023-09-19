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

    [Header("���ε� - ������ ������ ����")]
    [SerializeField] private CollectType collectType;
    [SerializeField] private Item CollectItem;

    // ���� ������ ������ ����
    public void SetItem(Item item)
    {
        //�Ű������� ������ ����
        CollectItem = item;

        // UI ��ҿ� ������ ���� ǥ��
        ItemNameTxT.text = CollectItem.Name;
        ItemDescTxT.text = CollectItem.Desc;
        CollectIcon.sprite = CollectItem.Icon;

        if (CollectIcon.sprite == null)
            return;
    }

    // ������ ���� ���� Ȯ��
    private bool IsItemCollected()
    {
        // ���⿡ ���� �ý����� ���� ���� Ȯ�� ������ �ۼ�
        return CollectionSystem.Instance.IsItemCollected(CollectItem);
    }

    /*private void UpdateIcon(Item item)
    {
        // �����۰� ���õ� ���� ������ ã�Ƽ�
        // �ش� ���� ������ �̹��� ������Ʈ�� ��������Ʈ�� ����
        foreach (var slot in )
        {
            if (slot.GetAssociatedItem() == item)
            {
                if (!IsItemCollected())
                { 
                    CollectIcon.color = new Color(0f, 0f, 0f, 1f);
                    ItemNameTxT.text = "???";
                    ItemDescTxT.text = "���� �������� ���� �����Դϴ�.";
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