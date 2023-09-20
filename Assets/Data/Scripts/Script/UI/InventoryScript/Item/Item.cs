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

    [Header("���� ������ �Ӽ�")]
    [SerializeField] int _ID;
    [SerializeField]public string _name;
    [TextArea(3, 6)]
    [SerializeField]public string Description;         // ����
    [SerializeField, Range(1, 99)]public int quantity; // ����
    public ItemType Type;

    [Space(10)]
    [Header("������")]
    [SerializeField] int Price_Buy;              // ���԰�
    [SerializeField] int Price_Sell;             // �ǸŰ�

    [Space(10)]
    [Header("���ε�")]
    public GameObject Prefab;
    [Header("������")]
    public Sprite Icon;

    [Space(10)]
    [Header("���� �ɾ����� ���� ������")]
    public GameObject SeedPrefab; // �ش� ������ ������
    [Header("�ڶ��� ����� ������")]
    public GameObject GrowingPrefab;
    [Header("�� �ڶ� ������")]
    public GameObject FullyGrownPrefab;
    [Header("��Ȯ�Ҷ� ����� ������")]
    public Sprite HarvestedIcon; // ��Ȯ�� �� ����� ������

    [Space(10)]
    [Header("���� ������")]
    public Weapon[] weapondata; //���� ������ �߰�
}

    [System.Serializable]
    public class Weapon
    {
        public Sprite weaponIcon;
        public GameObject weaponPrefab;
        public WeaponType weaponType; // ���� ����
        public int durability; // ���� ���� ������
    }