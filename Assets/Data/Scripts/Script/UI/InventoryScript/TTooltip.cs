using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아이템의 툴팁을 나타내기 위해 만들어졌습니다.
/// </summary>
public class TTooltip : MonoBehaviour
{
    [Header("바인딩")]
    [SerializeField] private GameObject TooltipPanel;
    [SerializeField] private Text ItemNameTxT;
    [SerializeField] private Text ItemDescTxT;
    [SerializeField] private Text ItemOptionTxT;

    private void Awake()
    {
        //최초 실행시 툴팁 창을 감춰둡니다.
       // HideTooltip();
    }

    public void HideTooltip() => TooltipPanel.SetActive(false);
    public void ShowTooltip(Item item)
    {
        TooltipPanel.SetActive(true);

        ItemNameTxT.text = item.Name;
        ItemDescTxT.text = item.Desc;

        if (item.Type == Item.ItemType.Equip)
            ItemOptionTxT.text = "장착 가능";
        if (item.Type == Item.ItemType.Food)
       //     ItemOptionTxT.text = "사용시 스태미너 회복:" + item.Value;
        if (item.Type == Item.ItemType.Default)
            ItemOptionTxT.text = "제작재료";
    }
}
