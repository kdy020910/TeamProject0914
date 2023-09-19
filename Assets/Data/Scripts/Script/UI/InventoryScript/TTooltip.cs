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
        //�����ϸ鼭 TooltipPanel�� �̹��� Alpha���� 0���� �ʱ�ȭ�մϴ�.
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
        // ������ ���� ���� ���콺 ������ �Բ� ���� ǥ�ð� �̷���� �� ��ġ���� ������ŵ�ϴ�.
        pos += new Vector3(TooltipPanel.GetComponent<RectTransform>().rect.width * 0.0f,
            -TooltipPanel.GetComponent<RectTransform>().rect.height * 0.15f, 0.00f);
        TooltipPanel.transform.position = pos;

        // ���Կ� �������� ���� ���� ShowTooltip�� ȣ���մϴ�.
        // ��, MountSlot �±װ� ���� ���Ը� ������ ǥ���մϴ�.
        if (item != null && !CompareTag("MountSlot"))
        {
            TooltipPanel.SetActive(true);
            // ������ ȣ�� �� �� ���� ���� 1�� �ٲپ� ���� �̹����� ǥ���մϴ�.
            SetAlpha(1);
        }

        // ���� �� �������� ������ �̸��� ������ �����ɴϴ�.
        ItemNameTxT.text = item.Name;
        ItemDescTxT.text = item.Desc;

        // �����۵��� Ÿ�Ժ��� Ư¡�� ǥ���մϴ�.
        if (item.Type == Item.ItemType.Equip)
        {
            // Item ���� WeaponData �迭�� �����ɴϴ�.
            Weapon[] weaponData = item.weapondata;

            if (weaponData != null && weaponData.Length > 0)
            {
                // �迭 �� ù ��° ������ �������� �����ɴϴ�.
                int durability = weaponData[0].durability;

                // ������ ���� ItemOptionTxT�� �Ҵ��մϴ�.
                ItemOptionTxT.text = "�������� | ������: " + durability;
                
                if(durability == 0)
                {
                    ItemOptionTxT.text = "��������";
                }
            }
        }

        if (item.Type == Item.ItemType.Default)
            ItemOptionTxT.text = "�������";
    }
}
