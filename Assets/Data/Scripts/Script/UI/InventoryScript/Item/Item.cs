using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="4Team Project/Create Item", order = 3)]
public class Item : ScriptableObject
{
    public enum ItemType { Equip, Food, Fish, Default, Seed }

    public int Id => _ID;
    public string Name => _name;
    public int Value;
    public string Desc => Description;
    public int Quantity => quantity;

    public int Buy => Price_Buy;
    public int Sell => Price_Sell;

    [Header("공통 아이템 속성")]
    [SerializeField] int _ID;
    [SerializeField]public string _name;
    [TextArea(3, 6)]
    [SerializeField]public string Description;         // 설명
    [SerializeField, Range(1, 99)]public int quantity; // 수량
    public ItemType Type;

    [Space(10)]
    [Header("상점가")]
    [SerializeField] int Price_Buy;              // 매입가
    [SerializeField] int Price_Sell;             // 판매가

    [Space(10)]
    [Header("바인딩")]
    public GameObject Prefab;
    [Header("아이콘")]
    public Sprite Icon;

    [Space(10)]
    [Header("씨앗 심었을때 생길 프리펩")]
    public GameObject SeedPrefab; // 해당 씨앗의 프리팹
    [Header("자랄때 변경될 프리펩")]
    public GameObject GrowingPrefab;
    [Header("다 자란 프리펩")]
    public GameObject FullyGrownPrefab;
    [Header("수확할때 사용할 아이콘")]
    public Sprite HarvestedIcon; // 수확할 때 사용할 아이콘

    [Space(10)]
    [Header("무기 데이터")]
    public Weapon[] weapondata; //무기 데이터 추가
}

    [System.Serializable]
    public class Weapon
    {
        public Sprite weaponIcon;
        public GameObject weaponPrefab;
        public WeaponType weaponType; // 무기 유형
        public int durability; // 현재 무기 내구도
    }