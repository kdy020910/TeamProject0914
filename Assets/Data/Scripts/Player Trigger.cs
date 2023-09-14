using System.Collections.Generic;
using UnityEngine;

// ���߿� �ڵ� ���̱�
public class PlayerTrigger : SystemProPerty
{
    public GameObject stonePrefab; // ��
    public GameObject ironOrePrefab; // ö����      
    public GameObject goldOrePrefab; // �ݱ���
    public GameObject[] woodPrefabs; // ���� ����
    public GameObject branchPrefab;// ��������

    private int randomWoodPrefabIndex; // ������ ���� �ε��� ����
    private List<Vector3> createdBranchPositions = new List<Vector3>();
    public GameObject stumpObject; // ������ ������Ʈ
    public GameObject hitTreeObject; // ���� ������Ʈ
    public GameObject hitRockObject; // ���� ������Ʈ

    public float maxInteractionDistance = 2.0f; // �ִ� ��ȣ�ۿ� �Ÿ�

    public GameObject CanRakeUI; // ���� ��ȣ�ۿ� 
    public GameObject CanFishUI; // ���� ��ȣ�ۿ�
    public GameObject DiyUI;     // �۾��� ��ȣ�ۿ�
    FarmSystem farmSystem;

    private void Start()
    {
        MessageUI.SetActive(false);
        DiyUI.SetActive(false);
        CanFishUI.SetActive(false);
        CanRakeUI.SetActive(false);
        farmSystem = GetComponent<FarmSystem>();
        randomWoodPrefabIndex = Random.Range(0, woodPrefabs.Length); // ���� �������� ���� �ε��� �ʱ�ȭ
    }
    private void Update()
    {
        HandleDIyAction();
        HandleActions();
        HandleFarmFieldActions();
        HandleFishingPoleAction();
    }

    private void HandleActions()
    {
        Vector3 rayDirection = transform.forward;
        Ray ray = new Ray(transform.position, rayDirection);
        RaycastHit hit;

        //����
        if (playerController.isAxeEquipped && Input.GetKeyDown(KeyCode.F))
        {
            if (Physics.Raycast(ray, out hit, maxInteractionDistance))
            {
                if (hit.collider.CompareTag("Tree"))
                {
                    HandleAxeAction();
                }
            }
        }
        // ��
        if (playerController.isShovelEquipped && Input.GetKeyDown(KeyCode.F))
        {
            if (Physics.Raycast(ray, out hit, maxInteractionDistance))
            {
                if (hit.collider.CompareTag("Rock"))
                {
                    HandleShovelAction();
                }
                if (hit.collider.CompareTag("Stump"))
                {
                    HandleShovelAction();
                }
            }
        }
        // ���Ѹ���
        if (playerController.isWateringCanEquipped && farmField.isPlayerInFarm && Input.GetKeyDown(KeyCode.F))
        {
            HandleWateringCanAction();
        }
    }

    //����
    private void HandleAxeAction()
    {     
        myAnim.SetTrigger("Axing");   
    }
    //��
    private void HandleShovelAction()
    {
        myAnim.SetTrigger("Shoveling");
    }
    // ���Ѹ���
    private void HandleWateringCanAction()
    {
         myAnim.SetTrigger("Sprayingwater");
    }

    private void HandleFishingPoleAction()
    {
        if (pondField.isPlayerInPond && playerController.isFishingPoleEquipped)
        {
            CanFishUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                myAnim.SetTrigger("Fishing");
            }
        }
        else
        {
            CanFishUI.SetActive(false);
        }
    }
    // ���� 
    // -������-
    // ���� : 10
    private void HandleFarmFieldActions()
    {
        if (playerController.isRakeEquipped && farmField.isPlayerInFarm && farmField.currentState == FieldState.Empty)
        {
            CanRakeUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                myAnim.SetTrigger("Raking");
                farmSystem.ChangeState(FieldState.Tilled);
            }
        }
        else
        {
            CanRakeUI.SetActive(false);
        }
    }

    //DIY �۾��� ���� �ڵ�
    private void HandleDIyAction()
    {
        if (diyField.isPlayerInDiy && Input.GetKeyDown(KeyCode.F))
        {
            // f ������ diy�۾� ui�� ��
            DiyUI.SetActive(true);
            diyField.CanDiyUi.SetActive(false);
        }
    }

    // ���� Trigger ���� �ڵ�

    private int axeHitCount = 0;
    private const int maxAxeHits = 3; // �ִ� ĥ �� �ִ� Ƚ��
    private const float branchDropProbability = 0.8f; // ���������� ���� Ȯ��
    private List<Vector3> createdWoodPositions = new List<Vector3>(); // �̹� ������ ���� ��ġ ����Ʈ

    public void OnAxingAnimationEnd()
    {
        // ���� ������ ������ ������ Ȯ��
        Weapon equippedWeapon = playerController.currentEquippedWeapon;

        // ������ ����
        equippedWeapon.durability--;

        // �������� 0�̸� ���� ����
        // ���� �ȿ� �̹����� ���������
        if (equippedWeapon.durability <= 0)
        {
            //playerController.UnequipWeapon();
        }
        // �������� PlayerPrefs�� ����
        PlayerPrefs.SetInt("EquippedWeaponDurability", equippedWeapon.durability);

        if (IsNearObject(hitTreeObject) && playerController.currentEquippedWeapon != null)
        {
            axeHitCount++;
            Vector3 stumpSpawnPosition = hitTreeObject.transform.position;

            if (axeHitCount >= maxAxeHits)
            {
                if (hitTreeObject.CompareTag("Tree"))
                {
                    Destroy(hitTreeObject);
                    axeHitCount = 0;

                    // ���� �׷��ͱ� ����
                    Instantiate(stumpObject, stumpSpawnPosition, Quaternion.identity);

                    // ���� ����
                    for (int i = 0; i < 3; i++)
                    {
                        Vector3 woodSpawnPosition = GetNonOverlappingPosition(stumpSpawnPosition, woodPrefabs[randomWoodPrefabIndex], createdWoodPositions);
                        Instantiate(woodPrefabs[randomWoodPrefabIndex], woodSpawnPosition, Quaternion.identity);
                        Debug.Log("���簡 �����!");
                    }
                }
            }
            // �������� ��� Ȯ�� ���
            if (hitTreeObject.CompareTag("Tree"))
            {
                if (Random.value <= branchDropProbability)
                {
                    // �������� ����
                    Vector3 branchSpawnPosition = GetNonOverlappingPosition(stumpSpawnPosition, branchPrefab, createdBranchPositions);
                    Instantiate(branchPrefab, branchSpawnPosition, Quaternion.identity);
                    Debug.Log("���������� �����!");
                }
            }
        }
    }

    // �� Trigger ���� �ڵ�
    private int ShovelHitCount = 0;

    public void OnShovelingAnimationEnd()
    {
        // ���� ������ ������ ������ Ȯ��
        Weapon equippedWeapon = playerController.currentEquippedWeapon;

        // ������ ����
        equippedWeapon.durability--;

        // �������� 0�̸� ���� ����
        // ���� �ȿ� �̹����� ���������
        if (equippedWeapon.durability <= 0)
        {
            //playerController.UnequipWeapon();
        }

        if (IsNearObject(hitRockObject) && playerController.currentEquippedWeapon != null)
        {
            ShovelHitCount++;

        if (ShovelHitCount >= 4)
        {
            if (hitRockObject.CompareTag("Rock"))
            {
                // �� ����
                Destroy(hitRockObject);
                ShovelHitCount = 0;

                for (int i = 0; i < 4; i++)
                {
                    float randomValue = Random.value; // 0�� 1 ������ ������ ����

                    if (randomValue <= 0.6f) // ���� ���� Ȯ�� 60%
                    {
                        Vector3 spawnPosition = hitRockObject.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0.0f, Random.Range(-0.5f, 0.5f));
                        Instantiate(stonePrefab, spawnPosition, Quaternion.identity);
                        Debug.Log("���� �����!");
                    }
                    if (randomValue <= 0.3f) // ö ������ ���� Ȯ�� 30%
                    {
                        Vector3 spawnPosition = hitRockObject.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0.0f, Random.Range(-0.5f, 0.5f));
                        Instantiate(ironOrePrefab, spawnPosition, Quaternion.identity);
                        Debug.Log("ö ������ �����!");
                    }
                    if (randomValue <= 0.1f) // �� ������ ���� Ȯ�� 10%
                    {
                        Vector3 spawnPosition = hitRockObject.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0.0f, Random.Range(-0.5f, 0.5f));
                        Instantiate(goldOrePrefab, spawnPosition, Quaternion.identity);
                        Debug.Log("�� ������ �����!");
                    }
                }
            }
                
        }   
    }
        Vector3 rayDirection = transform.forward;
        Ray ray = new Ray(transform.position, rayDirection);
        RaycastHit hit;

        if (IsNearObject(stumpObject))
        {
            if (Physics.Raycast(ray, out hit, maxInteractionDistance))
            {
                if (hit.collider.CompareTag("Stump"))
                {
                    // ������ �׷��ͱ� ����
                    Destroy(stumpObject);
                }
            }
        }
    }

    // ����
    // - ������ -
    // ������ ���˴� : 10
    // ���˴� : 30 
    // �ݳ��˴� : 90

    // �����ð�(�ִϸ��̼� �ð� ����~����) ���� �� ���� Ȯ�� ����
    // ����� ���� - [ ��¡��, �׾�, ����, ����, ����
    // Ȯ�� -  0.25 ��¡��, 0.3 �׾�,  0.05 ����, 0.25 ����, 0.15 ����

    // ���� ���� or ���� üũ
    // ���� �� �ش� ������Ʈ �κ��丮 �߰� **(�κ��丮 �ý��� �۾� �Ϸ� �� �۾� ����)**
    // ���� �� ��������Ʈ �̸��� �� ���Ҵ�! �佺Ʈ �޼��� ���
    // ���� �� �ƹ��͵� ���� ���ߴ�! �佺Ʈ �޼��� ���
    // ���� �Ǵ� ���� ���� ���ô� ������ 1 ����
    // ���ô� ������ 0�϶�, ���ô밡 �η�����! �佺Ʈ �޼��� ���

    // ���� ���� trigger �ڵ�
    public void OnFishingAnimationEnd()
    {
        // ���� ������ ������ ������ Ȯ��
        Weapon equippedWeapon = playerController.currentEquippedWeapon;

        // ������ ����
        equippedWeapon.durability--;

        // �������� 0�̸� ���� ����
        if (equippedWeapon.durability <= 0)
        {
            //playerController.UnequipWeapon();
            ShowToastMessage("���ô밡 �η�����!");
            return;
        }

        if (pondField.isPlayerInPond && playerController.currentEquippedWeapon != null)
        {
            // ���� ���� �Ǵ� ���� Ȯ�� ���
            float randomValue = Random.value;
            float squidProbability = 0.25f;
            float troutProbability = squidProbability + 0.3f;
            float pufferfishProbability = troutProbability + 0.05f;
            float clamProbability = pufferfishProbability + 0.25f;

            if (randomValue <= squidProbability)
            {
                // ��¡� ������ ��
                ShowToastMessage("��¡� ���Ҵ�!");
                AddToInventory("��¡��"); // �κ��丮�� �߰� 
            }
            else if (randomValue <= troutProbability)
            {
                // �׾ ������ ��
                ShowToastMessage("�׾ ���Ҵ�!");
                AddToInventory("�׾�"); // �κ��丮�� �߰� 
            }
            else if (randomValue <= pufferfishProbability)
            {
                // ��� ������ ��
                ShowToastMessage("��� ���Ҵ�!");
                AddToInventory("����"); // �κ��丮�� �߰� 
            }
            else if (randomValue <= clamProbability)
            {
                // ������ ������ ��
                ShowToastMessage("������ ���Ҵ�!");
                AddToInventory("����"); // �κ��丮�� �߰� 
            }
            else
            {
                // ���� ������ �� (����)
                ShowToastMessage("�ƹ��͵� ���� ���ߴ�!");
            }


        }
    }
    public GameObject MessageUI; // �޼��� ui
    void ShowToastMessage(string message)
    {
        MessageUI.SetActive(true);
    }

    void AddToInventory(string itemName)
    {
        /*
        if (inventory != null)
        {
            // �������� �߰��� �� �ʿ��� �����͸� �����մϴ�.
            ItemData itemData = new ItemData();
            itemData.name = itemName;

            // �κ��丮�� �������� �߰��մϴ�.
            inventory.AddItem(itemData);

            // �������� ���������� �߰��Ǿ��ٴ� �޽����� ����ϰų�
            // �ٸ� ó���� ������ �� �ֽ��ϴ�.
            Debug.Log(itemName + "�� �κ��丮�� �߰��߽��ϴ�.");
        }*/
    }

    // ������Ʈ �Ÿ� üũ
    private bool IsNearObject(GameObject targetObject)
    {
        if (targetObject == null)
            return false;

        float distance = Vector3.Distance(transform.position, targetObject.transform.position);
        return distance <= maxInteractionDistance;
    }

    // GetNonOverlappingPosition �޼��� ����
    private Vector3 GetNonOverlappingPosition(Vector3 initialPosition, GameObject prefab, List<Vector3> existingPositions)
    {
        Collider prefabCollider = prefab.GetComponent<Collider>();
        if (prefabCollider == null)
        {
            Debug.LogError("Prefab does not have a Collider component.");
            return initialPosition;
        }

        Vector3 spawnPosition = initialPosition;

        // �� �� �õ��ص� ��ġ�� �ʴ� ��ġ�� ã���� ����
        int maxAttempts = 10;
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            // ������ ��ġ�κ��� �̼��� �������� �����Ͽ� ���ο� ��ġ�� �õ��մϴ�.
            Vector3 offset = new Vector3(Random.Range(-0.7f, 0.7f), 0.0f, Random.Range(-0.7f, 0.7f));
            spawnPosition = initialPosition + offset;

            // ���� ��ġ�� ��ġ�� �ʴ��� Ȯ���մϴ�.
            bool isOverlapping = IsOverlapping(spawnPosition, prefabCollider, existingPositions);

            // ��ġ�� ������ ������ �������ɴϴ�.
            if (!isOverlapping)
            {
                existingPositions.Add(spawnPosition); // ���ο� ��ġ�� ����Ʈ�� �߰��մϴ�.
                break;
            }
        }

        return spawnPosition;
    }

    private bool IsOverlapping(Vector3 position, Collider colliderToCheck, List<Vector3> existingPositions)
    {
        // �̹� ������ ������ ��ġ�� ���Ͽ� ��ġ���� Ȯ��
        foreach (Vector3 existingPosition in existingPositions)
        {
            float distance = Vector3.Distance(position, existingPosition);
            if (distance < colliderToCheck.bounds.extents.x * 2.0f) // Collider�� ũ�⸦ ����Ͽ� ��ģ�ٰ� �Ǵ��� �ּ� �Ÿ��� ����մϴ�.
            {
                return true; // ��ħ
            }
        }

        return false; // ��ġ�� ����
    }
}
