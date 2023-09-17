using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponType
{
    None, WateringCan, Rake,
    FragileAxe, Axe, GoldAxe, // ���� 
    FragileFishingPole, FishingPole, // ���˴�
    FragileShovel, Shovel, GoldShovel, // ��     
    Carrot, Potato,Pumpkin, Tomato, Corn
}
public class  MountingSlot : AnimationSystem
{
    public Dictionary<int, Weapon> equippedWeaponsMap = new Dictionary<int, Weapon>();

    public Transform rightHand; // ���⸦ ������ �� ��ġ
    public Image[] weaponSlotImages; // ���� �̹��� ���ε�

    public GameObject equippedWeapon; 
    public Weapon currentEquippedWeapon; // ���� ������ ����

    public bool IsNoneEquipped = false;
    public bool isShovelEquipped = false;
    public bool isAxeEquipped = false;
    public bool isFishingPoleEquipped = false;
    public bool isRakeEquipped = false;
    public bool isWateringCanEquipped = false;
    public bool isSeed = false;

    public GameObject[] slotBackgrounds; // �� ���� ��ȣ�� �ش��ϴ� bg �̹������� �迭�� ����
    public int selectedSlotNumber = -1; // ���� ���õ� ���� ��ȣ �ʱⰪ

    void Start()
    {
        IsNoneEquipped = true;
        // ��� ������ bg �̹��� ��Ȱ��ȭ
        foreach (var bg in slotBackgrounds)
        {
            bg.SetActive(false);
        }
    }

    void Update()
    {
        InputGetKeyDown();

        // ���� ���� Ű
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UnequipWeapon();
        }
    }
    public void UpdateSelectedSlotNumber()
    {
        selectedSlotNumber = GetSelectedSlotNumber();
    }

    void InputGetKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) ||
        Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedSlotNumber = GetSelectedSlotNumber();
            HandleWeaponEquipInput(); // ������ ���� ��ȣ�� ����

            // ���� ���õ� ������ bg �̹��� Ȱ��ȭ, ������ ��Ȱ��ȭ
            for (int i = 0; i < slotBackgrounds.Length; i++)
            {
                slotBackgrounds[i].SetActive(i + 1 == selectedSlotNumber);
            }

        }
    }

    void HandleWeaponEquipInput()
    {
        // ���� ���õ� ������ ���� ������Ʈ ��������
        GameObject selectedSlotObject = GetSelectedSlotObject();

        // ���� ������Ʈ�κ��� TSlot ������Ʈ ��������
         TSlot selectedSlot = selectedSlotObject.GetComponent<TSlot>();

        if (selectedSlot != null)
        {
            // ���Կ� �ִ� ���� ������ ��������
            Item selectedWeaponItem = selectedSlot.item;

            // ���� �����͸� ������� �÷��̾�� ���⸦ ����
            EquipWeapon(selectedWeaponItem);
        }
        else
        {
            AnimationParameterFalse();
            UnequipWeapon(); // �� ������ ��쿡�� ���� ����
        }
    }
    public GameObject[] slotGameObjects;
        
        // ���� ���õ� ������ ���� ������Ʈ ��������
        public GameObject GetSelectedSlotObject()
        {
            GameObject selectedSlotObject = null;

            switch (selectedSlotNumber)
            {
                case 1:
                    selectedSlotObject = slotGameObjects[0]; // �ش� ���� ������Ʈ�� ������ �����;� �մϴ�.
                    break;
                case 2:
                    selectedSlotObject = slotGameObjects[1];
                    break;
                case 3:
                    selectedSlotObject = slotGameObjects[2];
                    break;
                case 4:
                    selectedSlotObject = slotGameObjects[3];
                    break;
            }

            return selectedSlotObject;
        }
    

    public void EquipWeapon(Item item)
    { 
        UnequipWeapon(); // ���� ���� ����

        // ���� ������ ���⸦ �����մϴ�.
        currentEquippedWeapon = item.weapondata[0];

        // ���� ���� �� ��ġ ����
        equippedWeapon = Instantiate(currentEquippedWeapon.weaponPrefab, rightHand); // ���� �������� Prefab ���
        equippedWeapon.transform.localPosition = Vector3.zero;

        // ���� �ִϸ��̼� ����
        SetWeaponAnimationParameter(currentEquippedWeapon.weaponType);

        // Ư�� ���⸦ �� ��� is()Equipped�� true�� ����
        switch (currentEquippedWeapon.weaponType)
        {
            case WeaponType.None:
                IsNoneEquipped = true;
                break;
            case WeaponType.Axe:
            case WeaponType.FragileAxe:
            case WeaponType.GoldAxe:
                isAxeEquipped = true;
                break;
            case WeaponType.Shovel:
            case WeaponType.FragileShovel:
            case WeaponType.GoldShovel:
                isShovelEquipped = true;
                break;
            case WeaponType.FishingPole:
            case WeaponType.FragileFishingPole:
                isFishingPoleEquipped = true;
                break;
            case WeaponType.Rake:
                isRakeEquipped = true;
                break;
            case WeaponType.WateringCan:
                isWateringCanEquipped = true;
                break;
            case WeaponType.Carrot:
            case WeaponType.Potato:
            case WeaponType.Pumpkin:
            case WeaponType.Tomato:
            case WeaponType.Corn:     
                isSeed = true;
                break;
        }
        // �󽽷��϶� �迭 �����������Ͻ��Ѿ��� (������ �ȳ����ؾ��� (����ó��)) 
    }


    public void UnequipWeapon()
    {
        Debug.Log("UnequipWeapon method called.");
        if (equippedWeapon != null)
        {
            Destroy(equippedWeapon);
            equippedWeapon = null;

            // ���� ���� �� �ش� ���� ������ ������ false�� ����
            IsNoneEquipped = false;

            isWateringCanEquipped = false;
            isAxeEquipped = false;
            isShovelEquipped = false;
            isFishingPoleEquipped = false;
            isRakeEquipped = false;
            isSeed = false;
            // �ٸ� ���� ������ ���� ������ �߰�

            // �ִϸ��̼� �Ķ���͸� �ʱ�ȭ�մϴ�.
            AnimationParameterFalse();
        }
    }

    //.. ���� ���� �ڵ�

    public int GetSelectedSlotNumber()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            return 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            return 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            return 4;
        }
        else
        {
            return -1; // ���õ� ������ ���� ���
        }
    }
}