using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "Custom/Seed")]
public class Seed : ScriptableObject
{
    public string seedName; // 씨앗의 이름 (감자, 토마토, 호박, 옥수수, 당근)
    public GameObject seedPrefab; // 해당 씨앗에 대한 프리팹
    public Sprite seedSprite; // 씨앗 이미지
    public SeedType seedType;
}
public enum SeedType
{
    Carrot, Potato, Tomato, Corn, Pumpkin
}

public class FarmSystem : SystemProPerty
{
    public GameObject harvestUI; // 수확하기 UI 오브젝트 
    public GameObject plantUI; // 심기 UI
    public Transform seedContainer; // 씨앗들이 들어 있는 컨테이너 (밭의 자식 오브젝트)

    public float plantingTime = 10.0f; // 씨앗 심은 시간
    private float plantedTime;

    private bool CanPlantSeed = false;
    private int currentSeedIndex = 0;

    public List<Seed> seeds; // 다양한 종류의 씨앗 정보

    private void Start()
    {
        harvestUI.SetActive(false);
        plantUI.SetActive(false);
    }

    private void Update()
    {
        if (HasSeedInSlot() && farmField.isPlayerInFarm && farmField.currentState == FieldState.Tilled)
        {
            CanPlantSeed = true;
            plantUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                PlantSeedOnField();
            }
        }
        else
        {
            CanPlantSeed = false;
            plantUI.SetActive(false);
        }

        if (farmField.currentState == FieldState.ReadyToHarvest)
        {
            Harvest();
        }

        if (farmField.currentState == FieldState.Planted && Time.time - plantedTime > plantingTime)
        {
            ChangeState(FieldState.Growing);
        }
    }
    public void ChangeState(FieldState newState)
    {
        farmField.currentState = newState;
        if (newState == FieldState.Planted)
        {
            plantedTime = Time.time;
        }
    }
    public bool HasSeedInSlot()
    {
        // 1부터 4까지의 슬롯 번호를 반복합니다.
        for (int slotNumber = 1; slotNumber <= 4; slotNumber++)
        {
            // 해당 슬롯의 무기를 가져옵니다.
            if (playerController.equippedWeaponsMap.TryGetValue(slotNumber, out Weapon selectedWeapon))
            {
                // 해당 슬롯에 씨앗이 있는지 확인합니다.
                if (selectedWeapon.weaponType == WeaponType.Seed)
                {
                    // 슬롯에 씨앗이 있는 경우 true를 반환합니다.
                    return true;
                }
            }
        }
        // 모든 슬롯에서 씨앗을 찾지 못한 경우 false를 반환합니다.
        return false;
    }

    public void PlantSeedOnField()
    {
        if (farmField != null && farmField.currentState == FieldState.Tilled)
        {
            int slotNumberWithSeed = FindSlotWithSeed();
            if (slotNumberWithSeed != -1)
            {
                ChangeState(FieldState.Planted);
                Debug.Log("씨앗을 심었습니다!");
                plantUI.SetActive(false);

                // 씨앗을 순차적으로 활성화
                ActivateNextSeed();
            }
            else
            {
                Debug.LogWarning("슬롯에서 씨앗을 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogWarning("밭이 존재하지 않거나 갈린 상태가 아닙니다.");
        }
    }

    private int FindSlotWithSeed()
    {
        // 1부터 4까지의 슬롯 번호를 반복합니다.
        for (int slotNumber = 1; slotNumber <= 4; slotNumber++)
        {
            // 해당 슬롯의 무기를 가져옵니다.
            if (playerController.equippedWeaponsMap.TryGetValue(slotNumber, out Weapon selectedWeapon))
            {
                // 해당 슬롯에 씨앗이 있는지 확인합니다.
                if (selectedWeapon.weaponType == WeaponType.Seed)
                {
                    // 슬롯에 씨앗이 있는 경우 슬롯 번호를 반환합니다.
                    return slotNumber;
                }
            }
        }

        // 씨앗이 없는 경우 -1을 반환합니다.
        return -1;
    }

    public void Harvest()
    {
        ChangeState(FieldState.Empty);
        harvestUI.SetActive(false);
        // 여기에서 수확 로직을 구현하세요.
        // 수확한 아이템을 어딘가에 추가하거나 다른 처리를 수행하세요.
    }

    private void ActivateNextSeed()
    {
        if (currentSeedIndex < seeds.Count)
        {
            // 다음 씨앗 정보를 가져옵니다.
            Seed nextSeed = seeds[currentSeedIndex];

            // 해당 씨앗에 대한 프리팹을 생성하여 밭에 심습니다.
            GameObject plantedSeed = Instantiate(nextSeed.seedPrefab, seedContainer);

            // 설정된 위치 및 회전값을 조절할 수 있습니다.
            plantedSeed.transform.position = Vector3.zero;
            plantedSeed.transform.rotation = Quaternion.identity;

            currentSeedIndex++;
        }
    }
}
