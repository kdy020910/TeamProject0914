using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmSystem : SystemProPerty
{
    public GameObject harvestUI; // ��Ȯ�ϱ� UI ������Ʈ 
    public GameObject plantUI; // �ɱ� UI

    private bool CanPlantSeed = false;
    private int currentSeedIndex = 0;

    public FarmField[] farmFields; // �� ���� �迭

    private void Start()
    {
        harvestUI.SetActive(false);
        plantUI.SetActive(false);

      
    }

    private void Update()
    {
        // farmFields�� ��ȸ�ϸ� ������ ���¸� Ȯ��
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

    // �� ���� ����
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
                        farmField.currentState = FieldState.Planted;
                        farmField.plantedSeeds.Add(selectedSeedItem);
                        Debug.Log("������ �ɾ����ϴ�: " + selectedSeedItem._name);

                        // ������ ������ ���� ��ġ�� ���� ��ġ
                        foreach (Transform spawnPosition in farmField.seedPositions)
                        {
                            Instantiate(selectedSeedItem.SeedPrefab, spawnPosition.position, Quaternion.identity);
                        }

                        plantUI.SetActive(false);
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
        else
        {
            Debug.LogWarning("���� �������� �ʰų� ���� ���°� �ƴմϴ�.");
        }
    }

    public void Harvest(FarmField farmField)
    {
        ChangeState(FieldState.Empty);
        harvestUI.SetActive(false);
        // ���⿡�� ��Ȯ ������ �����ϼ���.
        // ��Ȯ�� �������� ��򰡿� �߰��ϰų� �ٸ� ó���� �����ϼ���.
    }
}
