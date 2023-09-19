// FarmSystem.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmSystem : SystemProPerty
{
    public GameObject harvestUI; // ��Ȯ�ϱ� UI ������Ʈ 
    public GameObject plantUI; // �ɱ� UI
    public GameObject CanRakeUI; // ���� ��ȣ�ۿ� 
    private bool CanPlantSeed = false;

    public FarmField[] farmFields; // �� ���� �迭

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


    // ���� 
    // -������-
    // ���� : 10
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
                            farmField.ChangePositionState(i, FieldState.Tilled); // �ش� ������ ���� ���� ����
                        }
                        break; // �� �� ���� ���� �� �̻� �� �� ������ ����
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
                    // ���� ���õ� ������ ���� ������Ʈ ��������
                    GameObject selectedSlotObject = mountingSlot.GetSelectedSlotObject();

                    // ���� ������Ʈ�κ��� TSlot ������Ʈ ��������
                    TSlot selectedSlot = selectedSlotObject.GetComponent<TSlot>();

                    if (selectedSlot != null)
                    {
                        // ���Կ� �ִ� ���� ������ ��������
                        Item selectedSeedItem = selectedSlot.item;

                        if (selectedSeedItem.Type == Item.ItemType.Seed)
                        {
                            // ���� �翡 �ɾ��� ���� ���� üũ
                            if (farmField.plantedSeeds.Count < farmField.maxSeedCount)
                            {
                                // ������ �ɴ� �۾� ����
                                farmField.positionsState[i] = FieldState.Planted;
                                farmField.plantedSeeds.Add(selectedSeedItem);
                                Debug.Log("������ �ɾ����ϴ�: " + selectedSeedItem._name);

                                // �۹��� ���� ������ ���� �Ҵ�
                                Crop crop = farmField.seedPositions[i].GetComponent<Crop>();
                                if (crop != null)
                                {
                                    crop.plantedSeed = selectedSeedItem;
                                }

                                // ������ ���� ��ġ�� ���
                                int positionIndex = farmField.plantedSeeds.Count - 1;
                                if (positionIndex < farmField.seedPositions.Length)
                                {
                                    // �ش� ��ġ�� ������ ��ġ
                                    GameObject seedPrefab = Instantiate(selectedSeedItem.SeedPrefab, farmField.seedPositions[positionIndex].position, Quaternion.identity);
                                    seedPrefab.transform.parent = farmField.seedPositions[positionIndex]; // ���� �������� ��ġ�� �ڽ����� ����
                                }
                                else
                                {
                                    Debug.LogWarning("�� �̻� ������ ��ġ�� ��ġ�� �����ϴ�.");
                                }

                                plantUI.SetActive(false);
                                break; // �� �� ������ ����
                            }
                            else
                            {
                                Debug.LogWarning("�ش� �翡�� �� �̻� ������ ���� �� �����ϴ�.");
                            }
                        }
                        else
                        {
                            Debug.LogWarning("������ ���Կ� ������ �ƴ� �ٸ� �������� �����Ǿ� �ֽ��ϴ�.");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("������ ���Կ��� TSlot ������Ʈ�� ã�� �� �����ϴ�.");
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("���� �������� �ʰų� ���� ���°� �ƴմϴ�.");
        }
    }

    private void Harvest(FarmField farmField, int positionIndex)
    {
        if (farmField.positionsState[positionIndex] == FieldState.ReadyToHarvest)
        {
            harvestUI.SetActive(true);
            // ���⿡�� ��Ȯ ������ �����մϴ�.
            // ��Ȯ�� �������� ��򰡿� �߰��ϰų� �ٸ� ó���� �����մϴ�.

            // ������ �����͸� �����ɴϴ�. ���⿡���� ���۹��� ��Ÿ���� Crop ������Ʈ�� plantedSeed�� ����մϴ�.
            Item harvestedItem = farmField.seedPositions[positionIndex].GetComponent<Crop>().plantedSeed;

            if (harvestedItem != null)
            {
                // �κ��丮�� �������� �߰��մϴ�. AcquireItem �޼��带 ����� �� �ִٰ� �����մϴ�.
                bool acquired = tinventory.AcquireItem(harvestedItem);

                if (acquired)
                {
                    // �������� ���������� �κ��丮�� �߰��� ���, ���� ���¸� �� ���·� �����մϴ�.
                    farmField.positionsState[positionIndex] = FieldState.Empty;
                }
                else
                {
                    Debug.LogWarning("�κ��丮�� �� �̻� �������� �߰��� �� �����ϴ�.");
                }
            }
            else
            {
                Debug.LogWarning("��Ȯ�� ������ �����Ͱ� �����ϴ�.");
            }
        }
    }
}