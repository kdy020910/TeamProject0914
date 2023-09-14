using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "Custom/Seed")]
public class Seed : ScriptableObject
{
    public string seedName; // ������ �̸� (����, �丶��, ȣ��, ������, ���)
    public GameObject seedPrefab; // �ش� ���ѿ� ���� ������
    public Sprite seedSprite; // ���� �̹���
    public SeedType seedType;
}
public enum SeedType
{
    Carrot, Potato, Tomato, Corn, Pumpkin
}

public class FarmSystem : SystemProPerty
{
    public GameObject harvestUI; // ��Ȯ�ϱ� UI ������Ʈ 
    public GameObject plantUI; // �ɱ� UI
    public Transform seedContainer; // ���ѵ��� ��� �ִ� �����̳� (���� �ڽ� ������Ʈ)

    public float plantingTime = 10.0f; // ���� ���� �ð�
    private float plantedTime;

    private bool CanPlantSeed = false;
    private int currentSeedIndex = 0;

    public List<Seed> seeds; // �پ��� ������ ���� ����

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
        // 1���� 4������ ���� ��ȣ�� �ݺ��մϴ�.
        for (int slotNumber = 1; slotNumber <= 4; slotNumber++)
        {
            // �ش� ������ ���⸦ �����ɴϴ�.
            if (playerController.equippedWeaponsMap.TryGetValue(slotNumber, out Weapon selectedWeapon))
            {
                // �ش� ���Կ� ������ �ִ��� Ȯ���մϴ�.
                if (selectedWeapon.weaponType == WeaponType.Seed)
                {
                    // ���Կ� ������ �ִ� ��� true�� ��ȯ�մϴ�.
                    return true;
                }
            }
        }
        // ��� ���Կ��� ������ ã�� ���� ��� false�� ��ȯ�մϴ�.
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
                Debug.Log("������ �ɾ����ϴ�!");
                plantUI.SetActive(false);

                // ������ ���������� Ȱ��ȭ
                ActivateNextSeed();
            }
            else
            {
                Debug.LogWarning("���Կ��� ������ ã�� �� �����ϴ�.");
            }
        }
        else
        {
            Debug.LogWarning("���� �������� �ʰų� ���� ���°� �ƴմϴ�.");
        }
    }

    private int FindSlotWithSeed()
    {
        // 1���� 4������ ���� ��ȣ�� �ݺ��մϴ�.
        for (int slotNumber = 1; slotNumber <= 4; slotNumber++)
        {
            // �ش� ������ ���⸦ �����ɴϴ�.
            if (playerController.equippedWeaponsMap.TryGetValue(slotNumber, out Weapon selectedWeapon))
            {
                // �ش� ���Կ� ������ �ִ��� Ȯ���մϴ�.
                if (selectedWeapon.weaponType == WeaponType.Seed)
                {
                    // ���Կ� ������ �ִ� ��� ���� ��ȣ�� ��ȯ�մϴ�.
                    return slotNumber;
                }
            }
        }

        // ������ ���� ��� -1�� ��ȯ�մϴ�.
        return -1;
    }

    public void Harvest()
    {
        ChangeState(FieldState.Empty);
        harvestUI.SetActive(false);
        // ���⿡�� ��Ȯ ������ �����ϼ���.
        // ��Ȯ�� �������� ��򰡿� �߰��ϰų� �ٸ� ó���� �����ϼ���.
    }

    private void ActivateNextSeed()
    {
        if (currentSeedIndex < seeds.Count)
        {
            // ���� ���� ������ �����ɴϴ�.
            Seed nextSeed = seeds[currentSeedIndex];

            // �ش� ���ѿ� ���� �������� �����Ͽ� �翡 �ɽ��ϴ�.
            GameObject plantedSeed = Instantiate(nextSeed.seedPrefab, seedContainer);

            // ������ ��ġ �� ȸ������ ������ �� �ֽ��ϴ�.
            plantedSeed.transform.position = Vector3.zero;
            plantedSeed.transform.rotation = Quaternion.identity;

            currentSeedIndex++;
        }
    }
}
