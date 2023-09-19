using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionSystem : TSingleton<CollectionSystem>
{
    // ���� �׸�� ���� ���θ� �����ϴ� ��� ����Ʈ
    private List<Dictionary<Item, bool>> CollectIndex;

    private void Awake()
    {
        // ���� �̵��ص� �������� �ʵ��� ����
        Init();
    }

    public CollectionSystem()
    {
        // ���� �ʱ�ȭ (��� �׸��� ���� �������� ���� ���·� �ʱ�ȭ)
        CollectIndex = new List<Dictionary<Item, bool>>();
    }

    // �������� �������� �� ȣ���ϴ� �޼���
    public void CollectItem(Item item)
    {
        // �̹� ������ ���������� Ȯ��
        bool isCollected = IsItemCollected(item);

        if (!isCollected)
        {
            // ���� �������� ���� �������̸� ���� ���¸� true�� ����
            Dictionary<Item, bool> itemData = new Dictionary<Item, bool>();
            itemData[item] = true;
            CollectIndex.Add(itemData);
        }
    }

    // �������� ������ ���θ� Ȯ���ϴ� �޼���
    public bool IsItemCollected(Item item)
    {
        foreach (var itemData in CollectIndex)
        {
            if (itemData.ContainsKey(item) && itemData[item])
            {
                // �ش� �������� �����Ǿ��ٸ� true ��ȯ
                return true;
            }
        }

        // �ش� �������� �������� �ʾҴٸ� false ��ȯ
        return false;
    }
}
