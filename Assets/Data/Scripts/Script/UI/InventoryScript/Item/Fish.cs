using Unity.VisualScripting;
using UnityEngine;

public class Fish : Item
{
    public enum FishType { Common, Rare, Legendary } // 물고기 유형

    [Header("물고기 속성")]
    public FishType fishType;
}

// 오징어 데이터
[CreateAssetMenu(fileName = "New Squid", menuName = "4Team Project/Fish Data/Squid", order = 5)]
public class Squid : Fish
{
    public void Initialize()
    {
        _name = "오징어";
        Description = "신선한 오징어입니다.";
        Type = ItemType.Fish;
        fishType = FishType.Common;
        Icon = Resources.Load<Sprite>("FishIcons/SquidIcon");
    }
}

// 잉어 데이터
[CreateAssetMenu(fileName = "New Trout", menuName = "4Team Project/Fish Data/Trout", order = 6)]
public class Trout : Fish
{
    public void Initialize()
    {
        _name = "잉어";
        Description = "신선한 잉어입니다.";
        Type = ItemType.Fish;
        fishType = FishType.Common;
        Icon = Resources.Load<Sprite>("FishIcons/TroutIcon");
    }
}

// 복어 데이터
[CreateAssetMenu(fileName = "New Pufferfish", menuName = "4Team Project/Fish Data/Pufferfish", order = 7)]
public class Pufferfish : Fish
{
    public void Initialize()
    {
        _name = "복어";
        Description = "신선한 복어입니다.";
        Type = ItemType.Fish;
        fishType = FishType.Common;
        Icon = Resources.Load<Sprite>("FishIcons/PufferfishIcon");
    }
}

// 조개 데이터
[CreateAssetMenu(fileName = "New Clam", menuName = "4Team Project/Fish Data/Clam", order = 8)]
public class Clam : Fish
{
    public void Initialize()
    {
        _name = "조개";
        Description = "신선한 조개입니다.";
        Type = ItemType.Fish;
        fishType = FishType.Common;
        Icon = Resources.Load<Sprite>("FishIcons/ClamIcon");
    }
}
// 새우 데이터
[CreateAssetMenu(fileName = "New Shirimp", menuName = "4Team Project/Fish Data/Shirimp", order = 8)]
public class Shirimp : Fish
{
    public void Initialize()
    {
        _name = "새우";
        Description = "신선한 새우입니다.";
        Type = ItemType.Fish;
        fishType = FishType.Common;
        Icon = Resources.Load<Sprite>("FishIcons/ShirimpIcon");
    }
}

