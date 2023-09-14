using Unity.VisualScripting;
using UnityEngine;

public class Fish : Item
{
    public enum FishType { Common, Rare, Legendary } // ����� ����

    [Header("����� �Ӽ�")]
    public FishType fishType;
}

// ��¡�� ������
[CreateAssetMenu(fileName = "New Squid", menuName = "4Team Project/Fish Data/Squid", order = 5)]
public class Squid : Fish
{
    public void Initialize()
    {
        _name = "��¡��";
        Description = "�ż��� ��¡���Դϴ�.";
        Type = ItemType.Fish;
        fishType = FishType.Common;
        Icon = Resources.Load<Sprite>("FishIcons/SquidIcon");
    }
}

// �׾� ������
[CreateAssetMenu(fileName = "New Trout", menuName = "4Team Project/Fish Data/Trout", order = 6)]
public class Trout : Fish
{
    public void Initialize()
    {
        _name = "�׾�";
        Description = "�ż��� �׾��Դϴ�.";
        Type = ItemType.Fish;
        fishType = FishType.Common;
        Icon = Resources.Load<Sprite>("FishIcons/TroutIcon");
    }
}

// ���� ������
[CreateAssetMenu(fileName = "New Pufferfish", menuName = "4Team Project/Fish Data/Pufferfish", order = 7)]
public class Pufferfish : Fish
{
    public void Initialize()
    {
        _name = "����";
        Description = "�ż��� �����Դϴ�.";
        Type = ItemType.Fish;
        fishType = FishType.Common;
        Icon = Resources.Load<Sprite>("FishIcons/PufferfishIcon");
    }
}

// ���� ������
[CreateAssetMenu(fileName = "New Clam", menuName = "4Team Project/Fish Data/Clam", order = 8)]
public class Clam : Fish
{
    public void Initialize()
    {
        _name = "����";
        Description = "�ż��� �����Դϴ�.";
        Type = ItemType.Fish;
        fishType = FishType.Common;
        Icon = Resources.Load<Sprite>("FishIcons/ClamIcon");
    }
}
// ���� ������
[CreateAssetMenu(fileName = "New Shirimp", menuName = "4Team Project/Fish Data/Shirimp", order = 8)]
public class Shirimp : Fish
{
    public void Initialize()
    {
        _name = "����";
        Description = "�ż��� �����Դϴ�.";
        Type = ItemType.Fish;
        fishType = FishType.Common;
        Icon = Resources.Load<Sprite>("FishIcons/ShirimpIcon");
    }
}

