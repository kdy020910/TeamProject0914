using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionSystem : TSingleton<CollectionSystem>
{
    // 도감 항목과 수집 여부를 저장하는 통계 리스트
    private List<Dictionary<Item, bool>> CollectIndex;

    private void Awake()
    {
        // 씬을 이동해도 삭제되지 않도록 설정
        Init();
    }

    public CollectionSystem()
    {
        // 도감 초기화 (모든 항목을 아직 수집하지 않은 상태로 초기화)
        CollectIndex = new List<Dictionary<Item, bool>>();
    }

    // 아이템을 수집했을 때 호출하는 메서드
    public void CollectItem(Item item)
    {
        // 이미 수집한 아이템인지 확인
        bool isCollected = IsItemCollected(item);

        if (!isCollected)
        {
            // 아직 수집하지 않은 아이템이면 수집 상태를 true로 변경
            Dictionary<Item, bool> itemData = new Dictionary<Item, bool>();
            itemData[item] = true;
            CollectIndex.Add(itemData);
        }
    }

    // 아이템을 수집한 여부를 확인하는 메서드
    public bool IsItemCollected(Item item)
    {
        foreach (var itemData in CollectIndex)
        {
            if (itemData.ContainsKey(item) && itemData[item])
            {
                // 해당 아이템이 수집되었다면 true 반환
                return true;
            }
        }

        // 해당 아이템이 수집되지 않았다면 false 반환
        return false;
    }
}
