// FarmSystem.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmSystem : SystemProPerty
{
    public GameObject harvestUI; // 수확하기 UI 오브젝트 
    public GameObject plantUI; // 심기 UI
    public GameObject CanRakeUI; // 갈퀴 상호작용 
    private bool CanPlantSeed = false;

    public FarmField[] farmFields; // 밭 정보 배열

    private void Start()
    {
        CanRakeUI.SetActive(false);
        harvestUI.SetActive(false);
        plantUI.SetActive(false);
    }

    private void Update()
    {
        HandleFarmFieldActions();

        foreach (FarmField farmField in farmFields)
        {
            if (farmField.isPlayerInFarm)
            {
                bool canPlant = false;

                for (int i = 0; i < farmField.positionsState.Length; i++)
                {
                    if (farmField.positionsState[i] == FieldState.Tilled)
                    {
                        canPlant = true;
                        break;
                    }
                }

                CanPlantSeed = canPlant;
                plantUI.SetActive(canPlant);

                if (Input.GetKeyDown(KeyCode.F) && canPlant)
                {
                    PlantSeedOnField(farmField);
                }

                for (int i = 0; i < farmField.positionsState.Length; i++)
                {
                    if (farmField.positionsState[i] == FieldState.ReadyToHarvest)
                    {
                        harvestUI.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            Harvest(farmField, i);
                            harvestUI.SetActive(false);
                        }
                    }
                }
            }
        }
    }


    // 갈퀴 
    // -내구도-
    // 갈퀴 : 10
    private void HandleFarmFieldActions()
    {
        if (mountingSlot.isRakeEquipped)
        {
            foreach (FarmField farmField in farmFields)
            {
                for (int i = 0; i < farmField.positionsState.Length; i++)
                {
                    if (farmField.positionsState[i] == FieldState.Empty)
                    {
                        CanRakeUI.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            myAnim.SetTrigger("Raking");
                            farmField.ChangePositionState(i, FieldState.Tilled); // 해당 포지션 갈퀴 상태 변경
                        }
                        break; // 한 번 갈고 나면 더 이상 갈 수 없도록 종료
                    }
                }
            }
        }
        else
        {
            CanRakeUI.SetActive(false);
        }
    }

    private void PlantSeedOnField(FarmField farmField)
    {
        if (farmField != null)
        {
            for (int i = 0; i < farmField.positionsState.Length; i++)
            {
                if (farmField.positionsState[i] == FieldState.Tilled)
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
                                farmField.positionsState[i] = FieldState.Planted;
                                farmField.plantedSeeds.Add(selectedSeedItem);
                                Debug.Log("씨앗을 심었습니다: " + selectedSeedItem._name);

                                // 작물에 씨앗 아이템 정보 할당
                                Crop crop = farmField.seedPositions[i].GetComponent<Crop>();
                                if (crop != null)
                                {
                                    crop.plantedSeed = selectedSeedItem;
                                }

                                // 다음에 심을 위치를 계산
                                int positionIndex = farmField.plantedSeeds.Count - 1;
                                if (positionIndex < farmField.seedPositions.Length)
                                {
                                    // 해당 위치에 씨앗을 배치
                                    GameObject seedPrefab = Instantiate(selectedSeedItem.SeedPrefab, farmField.seedPositions[positionIndex].position, Quaternion.identity);
                                    seedPrefab.transform.parent = farmField.seedPositions[positionIndex]; // 씨앗 프리팹을 위치의 자식으로 설정
                                }
                                else
                                {
                                    Debug.LogWarning("더 이상 씨앗을 배치할 위치가 없습니다.");
                                }

                                plantUI.SetActive(false);
                                break; // 한 번 심으면 종료
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
            }
        }
        else
        {
            Debug.LogWarning("밭이 존재하지 않거나 갈린 상태가 아닙니다.");
        }
    }

    private void Harvest(FarmField farmField, int positionIndex)
    {
        if (farmField.positionsState[positionIndex] == FieldState.ReadyToHarvest)
        {
            harvestUI.SetActive(true);
            // 여기에서 수확 로직을 구현합니다.
            // 수확한 아이템을 어딘가에 추가하거나 다른 처리를 수행합니다.

            // 아이템 데이터를 가져옵니다. 여기에서는 농작물을 나타내는 Crop 컴포넌트의 plantedSeed를 사용합니다.
            Item harvestedItem = farmField.seedPositions[positionIndex].GetComponent<Crop>().plantedSeed;

            if (harvestedItem != null)
            {
                // 인벤토리에 아이템을 추가합니다. AcquireItem 메서드를 사용할 수 있다고 가정합니다.
                bool acquired = tinventory.AcquireItem(harvestedItem);

                if (acquired)
                {
                    // 아이템을 성공적으로 인벤토리에 추가한 경우, 밭의 상태를 빈 상태로 변경합니다.
                    farmField.positionsState[positionIndex] = FieldState.Empty;
                }
                else
                {
                    Debug.LogWarning("인벤토리에 더 이상 아이템을 추가할 수 없습니다.");
                }
            }
            else
            {
                Debug.LogWarning("수확할 아이템 데이터가 없습니다.");
            }
        }
    }
}