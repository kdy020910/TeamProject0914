using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

// 나중에 코드 줄이기
public class PlayerTrigger : AnimationSystem
{
    public GameObject stonePrefab; // 돌
    public GameObject ironOrePrefab; // 철광석      
    public GameObject goldOrePrefab; // 금광석
    public GameObject woodPrefab; // 목재 
    public GameObject branchPrefab;// 나뭇가지

    private List<Vector3> createdBranchPositions = new List<Vector3>();
    public GameObject stumpObject; // 스텀프 오브젝트
    public GameObject hitTreeObject; // 나무 오브젝트
    public GameObject hitRockObject; // 바위 오브젝트

    public float maxInteractionDistance = 2.0f; // 최대 상호작용 거리

    public GameObject DiyUI;     // 작업대 상호작용

    public GameObject ItemSlot;

    private void Start()
    {
        DiyUI.SetActive(false);
    }

    private void Update()
    {
        HandleDIyAction();
        HandleActions();

    }

    private void HandleActions()
    {
        Vector3 rayDirection = transform.forward;
        Ray ray = new Ray(transform.position, rayDirection);
        RaycastHit hit;

        //도끼
        if (mountingSlot.isAxeEquipped && Input.GetKeyDown(KeyCode.F))
        {
            if (Physics.Raycast(ray, out hit, maxInteractionDistance))
            {
                if (hit.collider.CompareTag("Tree"))
                {
                    HandleAxeAction();
                }
            }
        }
        // 삽
        if (mountingSlot.isShovelEquipped && Input.GetKeyDown(KeyCode.F))
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
        // 물뿌리개
        if (mountingSlot.isWateringCanEquipped && farmField.isPlayerInFarm && Input.GetKeyDown(KeyCode.F))
        {
            HandleWateringCanAction();
        }
    }
   

    //DIY 작업대 관련 코드
    private void HandleDIyAction()
    {
        if (diyField.isPlayerInDiy && Input.GetKeyDown(KeyCode.F))
        {
            // f 누르면 diy작업 ui가 뜸
            DiyUI.SetActive(true);
            diyField.CanDiyUi.SetActive(false);
        }
    }

    // 도끼 Trigger 관련 코드

    private int axeHitCount = 0;
    private const int maxAxeHits = 3; // 최대 칠 수 있는 횟수
    private const float branchDropProbability = 0.8f; // 나뭇가지가 나올 확률
    private List<Vector3> createdWoodPositions = new List<Vector3>(); // 이미 생성된 목재 위치 리스트

    public void OnAxingAnimationEnd()
    {
        // 현재 장착한 무기의 내구도 확인
        Weapon equippedWeapon = mountingSlot.currentEquippedWeapon;

        // 내구도 감소
        equippedWeapon.durability--;

        // 내구도가 0이면 무기 제거
        // 슬롯 안에 이미지도 사라져야함
        if (equippedWeapon.durability <= 0)
        {
            Debug.Log("앗" + mountingSlot.currentEquippedWeapon + "가(이) 부러졌다!");
            Destroy(mountingSlot.currentEquippedWeapon.weaponPrefab);
        }
        // 내구도를 PlayerPrefs에 저장
        PlayerPrefs.SetInt("EquippedWeaponDurability", equippedWeapon.durability);

        if (IsNearObject(hitTreeObject) && mountingSlot.currentEquippedWeapon != null)
        {
            axeHitCount++;
            Vector3 stumpSpawnPosition = hitTreeObject.transform.position;

            if (axeHitCount >= maxAxeHits)
            {
                if (hitTreeObject.CompareTag("Tree"))
                {
                    Destroy(hitTreeObject);
                    axeHitCount = 0;

                    // 나무 그루터기 생성
                    Instantiate(stumpObject, stumpSpawnPosition, Quaternion.identity);

                    // 목재 생성
                    for (int i = 0; i < 3; i++)
                    {
                        Vector3 woodSpawnPosition = GetNonOverlappingPosition(stumpSpawnPosition, woodPrefab, createdWoodPositions);
                        Instantiate(woodPrefab, woodSpawnPosition, Quaternion.identity);
                        Debug.Log("목재가 드랍됨!");
                    }
                }
            }
            // 나뭇가지 드랍 확률 계산
            if (hitTreeObject.CompareTag("Tree"))
            {
                if (Random.value <= branchDropProbability)
                {
                    // 나뭇가지 생성
                    Vector3 branchSpawnPosition = GetNonOverlappingPosition(stumpSpawnPosition, branchPrefab, createdBranchPositions);
                    Instantiate(branchPrefab, branchSpawnPosition, Quaternion.identity);
                    Debug.Log("나뭇가지가 드랍됨!");
                }
            }
        }
    }

    // 삽 Trigger 관련 코드
    private int ShovelHitCount = 0;
    public void OnShovelingAnimationEnd()
    {
        // 현재 장착한 무기의 내구도 확인
        Weapon equippedWeapon = mountingSlot.currentEquippedWeapon;

        // 내구도 감소
        equippedWeapon.durability--;

        // 내구도가 0이면 무기 제거
        // 슬롯 안에 이미지도 사라져야함
        if (equippedWeapon.durability <= 0)
        {
            Debug.Log("앗" + mountingSlot.currentEquippedWeapon + "가(이) 부러졌다!");
            Destroy(mountingSlot.currentEquippedWeapon.weaponPrefab);
        }

        if (IsNearObject(hitRockObject) && mountingSlot.currentEquippedWeapon != null)
        {
            ShovelHitCount++;

        if (ShovelHitCount >= 4)
        {
            if (hitRockObject.CompareTag("Rock"))
            {
                // 돌 제거
                Destroy(hitRockObject);
                ShovelHitCount = 0;

                for (int i = 0; i < 4; i++)
                {
                    float randomValue = Random.value; // 0과 1 사이의 랜덤값 생성

                    if (randomValue <= 0.6f) // 돌이 나올 확률 60%
                    {
                        Vector3 spawnPosition = hitRockObject.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0.0f, Random.Range(-0.5f, 0.5f));
                        Instantiate(stonePrefab, spawnPosition, Quaternion.identity);
                        Debug.Log("돌이 드랍됨!");
                    }
                    if (randomValue <= 0.3f) // 철 광석이 나올 확률 30%
                    {
                        Vector3 spawnPosition = hitRockObject.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0.0f, Random.Range(-0.5f, 0.5f));
                        Instantiate(ironOrePrefab, spawnPosition, Quaternion.identity);
                        Debug.Log("철 광석이 드랍됨!");
                    }
                    if (randomValue <= 0.1f) // 금 광석이 나올 확률 10%
                    {
                        Vector3 spawnPosition = hitRockObject.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0.0f, Random.Range(-0.5f, 0.5f));
                        Instantiate(goldOrePrefab, spawnPosition, Quaternion.identity);
                        Debug.Log("금 광석이 드랍됨!");
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
                    // 삽으로 그루터기 삭제
                    Destroy(stumpObject);
                }
            }
        }
    }

    // 오브젝트 거리 체크
    private bool IsNearObject(GameObject targetObject)
    {
        if (targetObject == null)
            return false;

        float distance = Vector3.Distance(transform.position, targetObject.transform.position);
        return distance <= maxInteractionDistance;
    }

    // GetNonOverlappingPosition 메서드 수정
    private Vector3 GetNonOverlappingPosition(Vector3 initialPosition, GameObject prefab, List<Vector3> existingPositions)
    {
        Collider prefabCollider = prefab.GetComponent<Collider>();
        if (prefabCollider == null)
        {
            Debug.LogError("Prefab does not have a Collider component.");
            return initialPosition;
        }

        Vector3 spawnPosition = initialPosition;

        // 몇 번 시도해도 겹치지 않는 위치를 찾도록 루프
        int maxAttempts = 10;
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            // 랜덤한 위치로부터 미세한 오프셋을 생성하여 새로운 위치를 시도합니다.
            Vector3 offset = new Vector3(Random.Range(-0.7f, 0.7f), 0.0f, Random.Range(-0.7f, 0.7f));
            spawnPosition = initialPosition + offset;

            // 기존 위치와 겹치지 않는지 확인합니다.
            bool isOverlapping = IsOverlapping(spawnPosition, prefabCollider, existingPositions);

            // 겹치지 않으면 루프를 빠져나옵니다.
            if (!isOverlapping)
            {
                existingPositions.Add(spawnPosition); // 새로운 위치를 리스트에 추가합니다.
                break;
            }
        }

        return spawnPosition;
    }

    private bool IsOverlapping(Vector3 position, Collider colliderToCheck, List<Vector3> existingPositions)
    {
        // 이미 생성된 아이템 위치와 비교하여 겹치는지 확인
        foreach (Vector3 existingPosition in existingPositions)
        {
            float distance = Vector3.Distance(position, existingPosition);
            if (distance < colliderToCheck.bounds.extents.x * 2.0f) // Collider의 크기를 고려하여 겹친다고 판단할 최소 거리를 계산합니다.
            {
                return true; // 겹침
            }
        }

        return false; // 겹치지 않음
    }
}
