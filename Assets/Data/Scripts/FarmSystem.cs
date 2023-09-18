using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmSystem : SystemProPerty
{
    public GameObject harvestUI; // 수확하기 UI 오브젝트 
    public GameObject plantUI; // 심기 UI

    private bool CanPlantSeed = false;
    private int currentSeedIndex = 0;

    public FarmField[] farmFields; // 밭 정보 배열

    private void Start()
    {
        harvestUI.SetActive(false);
        plantUI.SetActive(false);

      
    }

    private void Update()
    {
        // farmFields를 순회하며 각각의 상태를 확인
        foreach (FarmField farmField in farmFields)
        {
            if (farmField.isPlayerInFarm && farmField.currentState == FieldState.Tilled)
            {
                CanPlantSeed = true;
                plantUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    PlantSeedOnField(farmField);
                }
            }
            else
            {
                CanPlantSeed = false;
                plantUI.SetActive(false);
            }

            if (farmField.currentState == FieldState.ReadyToHarvest)
            {
                Harvest(farmField);
            }
        }
    }

    // 밭 상태 변경
    public void ChangeState(FieldState newState)
    {
        farmField.currentState = newState;
        if (newState == FieldState.Planted)
        {

        }
    }

    private void PlantSeedOnField(FarmField farmField)
    {
        if (farmField != null && farmField.currentState == FieldState.Tilled)
        {
            // 현재 선택된 슬롯의 게임 오브젝트 가져오기
            GameObject selectedSlotObject = mountingSlot.GetSelectedSlotObject();

            // 슬롯 오브젝트로부터 TSlot 컴포넌트 가져오기
            TSlot selectedSlot = selectedSlotObject.GetComponent<TSlot>();

            if (selectedSlot != null)
            {
                // 슬롯에 있는 무기 데이터 가져오기
                Item selectedSeedItem = selectedSlot.item;

                if (selectedSeedItem.Type == Item.ItemType.Seed)
                {
                    // 현재 밭에 심어진 씨앗 개수 체크
                    if (farmField.plantedSeeds.Count < farmField.maxSeedCount)
                    {
                        // 씨앗을 심는 작업 수행
                        farmField.currentState = FieldState.Planted;
                        farmField.plantedSeeds.Add(selectedSeedItem);
                        Debug.Log("씨앗을 심었습니다: " + selectedSeedItem._name);

                        // 선택한 씨앗을 심을 위치에 씨앗 배치
                        foreach (Transform spawnPosition in farmField.seedPositions)
                        {
                            Instantiate(selectedSeedItem.SeedPrefab, spawnPosition.position, Quaternion.identity);
                        }

                        plantUI.SetActive(false);
                    }
                    else
                    {
                        Debug.LogWarning("해당 밭에는 더 이상 씨앗을 심을 수 없습니다.");
                    }
                }
                else
                {
                    Debug.LogWarning("선택한 슬롯에 씨앗이 아닌 다른 아이템이 장착되어 있습니다.");
                }
            }
            else
            {
                Debug.LogWarning("선택한 슬롯에서 TSlot 컴포넌트를 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogWarning("밭이 존재하지 않거나 갈린 상태가 아닙니다.");
        }
    }

    public void Harvest(FarmField farmField)
    {
        ChangeState(FieldState.Empty);
        harvestUI.SetActive(false);
        // 여기에서 수확 로직을 구현하세요.
        // 수확한 아이템을 어딘가에 추가하거나 다른 처리를 수행하세요.
    }
}
