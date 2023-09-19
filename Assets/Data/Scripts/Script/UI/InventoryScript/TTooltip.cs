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
        //시작하면서 TooltipPanel의 이미지 Alpha값을 0으로 초기화합니다.
        SetAlpha(0);
    }

    public void SetAlpha(float _alpha)
    {
        Color color = TooltipPanel.GetComponent<Image>().color;
        color.a = _alpha;
        TooltipPanel.GetComponent<Image>().color = color;
    }

    public void HideTooltip() => TooltipPanel.SetActive(false);
    public void ShowTooltip(Item item, Vector3 pos)
    {
        // 아이템 슬롯 위에 마우스 오버와 함께 툴팁 표시가 이루어질 때 위치값을 변동시킵니다.
        pos += new Vector3(TooltipPanel.GetComponent<RectTransform>().rect.width * 0.0f,
            -TooltipPanel.GetComponent<RectTransform>().rect.height * 0.15f, 0.00f);
        TooltipPanel.transform.position = pos;

        // 슬롯에 아이템이 있을 때만 ShowTooltip을 호출합니다.
        // 단, MountSlot 태그가 없는 슬롯만 툴팁을 표시합니다.
        if (item != null && !CompareTag("MountSlot"))
        {
            TooltipPanel.SetActive(true);
            // 툴팁이 호출 될 때 알파 값을 1로 바꾸어 툴팁 이미지를 표시합니다.
            SetAlpha(1);
        }

        // 슬롯 내 아이템이 가지는 이름과 설명을 가져옵니다.
        ItemNameTxT.text = item.Name;
        ItemDescTxT.text = item.Desc;

        // 아이템들의 타입별로 특징을 표시합니다.
        if (item.Type == Item.ItemType.Equip)
        {
            // Item 내의 WeaponData 배열을 가져옵니다.
            Weapon[] weaponData = item.weapondata;

            if (weaponData != null && weaponData.Length > 0)
            {
                // 배열 내 첫 번째 무기의 내구도를 가져옵니다.
                int durability = weaponData[0].durability;

                // 내구도 값을 ItemOptionTxT에 할당합니다.
                ItemOptionTxT.text = "장착가능 | 내구도: " + durability;
                
                if(durability == 0)
                {
                    ItemOptionTxT.text = "장착가능";
                }
            }
        }

        if (item.Type == Item.ItemType.Default)
            ItemOptionTxT.text = "제작재료";
    }
}
