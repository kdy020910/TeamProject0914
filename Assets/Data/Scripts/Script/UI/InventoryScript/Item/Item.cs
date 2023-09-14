using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="4Team Project/Create Item", order = 3)]
public class Item : ScriptableObject
{
    public enum ItemType { Equip, Food, Fish, Default }

    public int Id => _ID;
    public string Name => _name;
    public string Desc => Description;
    public int Quantity => quantity;

    public int Buy => Price_Buy;
    public int Sell => Price_Sell;

    [Header("���� ������ �Ӽ�")]
    [SerializeField] int _ID;
    [SerializeField]public string _name;
    [TextArea(3, 6)]
    [SerializeField]public string Description;         // ����
    [SerializeField, Range(1, 99)] int quantity; // ����
    public ItemType Type;

    [Space(10)]
    [Header("������")]
    [SerializeField] int Price_Buy;              // ���԰�
    [SerializeField] int Price_Sell;             // �ǸŰ�

    [Space(10)]
    [Header("���ε�")]
    public GameObject Prefab;
    public Sprite Icon;
    
    [Header("���� ������")]
    public Weapon[] weapondata; //���� ������ �߰�
}

    [System.Serializable]
    public class Weapon
    {
    [SerializeField] public Sprite weaponIcon;
    [SerializeField] public GameObject weaponPrefab;
    [SerializeField] public WeaponType weaponType; // ���� ����
    [SerializeField] public int durability; // ���� ���� ������
    }

