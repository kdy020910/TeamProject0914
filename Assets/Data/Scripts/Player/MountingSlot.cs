using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponType
{
    None, WateringCan, Rake,
    FragileAxe, Axe, GoldAxe, // 도끼 
    FragileFishingPole, FishingPole, // 낚싯대
    FragileShovel, Shovel, GoldShovel, // 삽     
    Carrot, Potato,Pumpkin, Tomato, Corn
}
public class  MountingSlot : AnimationSystem
{
    public Dictionary<int, Weapon> equippedWeaponsMap = new Dictionary<int, Weapon>();

    public Transform rightHand; // 무기를 장착할 손 위치
    public Image[] weaponSlotImages; // 슬롯 이미지 바인딩

    public GameObject equippedWeapon; 
    public Weapon currentEquippedWeapon; // 현재 장착된 무기

    public bool IsNoneEquipped = false;
    public bool isShovelEquipped = false;
    public bool isAxeEquipped = false;
    public bool isFishingPoleEquipped = false;
    public bool isRakeEquipped = false;
    public bool isWateringCanEquipped = false;
    public bool isSeed = false;

    public GameObject[] slotBackgrounds; // 각 슬롯 번호에 해당하는 bg 이미지들을 배열로 저장
    public int selectedSlotNumber = -1; // 현재 선택된 슬롯 번호 초기값

    void Start()
    {
        IsNoneEquipped = true;
        // 모든 슬롯의 bg 이미지 비활성화
        foreach (var bg in slotBackgrounds)
        {
            bg.SetActive(false);
        }
    }

    void Update()
    {
        InputGetKeyDown();

        // 무기 해제 키
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
            HandleWeaponEquipInput(); // 선택한 슬롯 번호를 전달

            // 현재 선택된 슬롯의 bg 이미지 활성화, 나머지 비활성화
            for (int i = 0; i < slotBackgrounds.Length; i++)
            {
                slotBackgrounds[i].SetActive(i + 1 == selectedSlotNumber);
            }

        }
    }

    void HandleWeaponEquipInput()
    {
        // 현재 선택된 슬롯의 게임 오브젝트 가져오기
        GameObject selectedSlotObject = GetSelectedSlotObject();

        // 슬롯 오브젝트로부터 TSlot 컴포넌트 가져오기
         TSlot selectedSlot = selectedSlotObject.GetComponent<TSlot>();

        if (selectedSlot != null)
        {
            // 슬롯에 있는 무기 데이터 가져오기
            Item selectedWeaponItem = selectedSlot.item;

            // 무기 데이터를 기반으로 플레이어에게 무기를 장착
            EquipWeapon(selectedWeaponItem);
        }
        else
        {
            AnimationParameterFalse();
            UnequipWeapon(); // 빈 슬롯일 경우에도 무기 해제
        }
    }
    public GameObject[] slotGameObjects;
        
        // 현재 선택된 슬롯의 게임 오브젝트 가져오기
        public GameObject GetSelectedSlotObject()
        {
            GameObject selectedSlotObject = null;

            switch (selectedSlotNumber)
            {
                case 1:
                    selectedSlotObject = slotGameObjects[0]; // 해당 슬롯 오브젝트의 참조를 가져와야 합니다.
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
        UnequipWeapon(); // 기존 무기 해제

        // 현재 장착된 무기를 저장합니다.
        currentEquippedWeapon = item.weapondata[0];

        // 무기 생성 및 위치 설정
        equippedWeapon = Instantiate(currentEquippedWeapon.weaponPrefab, rightHand); // 무기 데이터의 Prefab 사용
        equippedWeapon.transform.localPosition = Vector3.zero;

        // 무기 애니메이션 설정
        SetWeaponAnimationParameter(currentEquippedWeapon.weaponType);

        // 특정 무기를 든 경우 is()Equipped를 true로 설정
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
        // 빈슬롯일때 배열 참조값을리턴시켜야함 (오류가 안나게해야함 (예외처리)) 
    }


    public void UnequipWeapon()
    {
        Debug.Log("UnequipWeapon method called.");
        if (equippedWeapon != null)
        {
            Destroy(equippedWeapon);
            equippedWeapon = null;

            // 무기 해제 시 해당 무기 유형의 변수를 false로 설정
            IsNoneEquipped = false;

            isWateringCanEquipped = false;
            isAxeEquipped = false;
            isShovelEquipped = false;
            isFishingPoleEquipped = false;
            isRakeEquipped = false;
            isSeed = false;
            // 다른 무기 유형에 대한 설정도 추가

            // 애니메이션 파라미터를 초기화합니다.
            AnimationParameterFalse();
        }
    }

    //.. 슬롯 관련 코드

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
            return -1; // 선택된 슬롯이 없는 경우
        }
    }
}