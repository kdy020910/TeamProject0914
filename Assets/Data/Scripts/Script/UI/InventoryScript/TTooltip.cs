using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �������� ������ ��Ÿ���� ���� ����������ϴ�.
/// </summary>
public class TTooltip : MonoBehaviour
{
    [Header("���ε�")]
    [SerializeField] private GameObject TooltipPanel;
    [SerializeField] private Text ItemNameTxT;
    [SerializeField] private Text ItemDescTxT;
    [SerializeField] private Text ItemOptionTxT;

    private void Awake()
    {
        //���� ����� ���� â�� ����Ӵϴ�.
       // HideTooltip();
    }

    public void HideTooltip() => TooltipPanel.SetActive(false);
    public void ShowTooltip(Item item)
    {
        TooltipPanel.SetActive(true);

        ItemNameTxT.text = item.Name;
        ItemDescTxT.text = item.Desc;

        if (item.Type == Item.ItemType.Equip)
            ItemOptionTxT.text = "���� ����";
        if (item.Type == Item.ItemType.Food)
       //     ItemOptionTxT.text = "���� ���¹̳� ȸ��:" + item.Value;
        if (item.Type == Item.ItemType.Default)
            ItemOptionTxT.text = "�������";
    }
}
