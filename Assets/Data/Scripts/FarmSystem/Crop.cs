using System.Collections.Generic;
using UnityEngine;

public enum CropState
{
    Planted,        // 심어진 상태
    Growing,        // 자라는 중 상태
    ReadyToHarvest      // 완전히 자란 상태
}

public class Crop : SystemProPerty
{
    public string cropID;

    // Crop 클래스 내에서 positionIndex 변수를 정의
    public int positionIndex;

    [SerializeField]
    private CropState currentState = CropState.Planted;
    public float growthTime = 60f;      // 성장까지 걸리는 시간 

    // 수정된 부분: 각 작물별로 어떤 씨앗을 심었는지 나타내는 변수
    public Item plantedSeed;

    public float elapsedTime = 0f; // 독립적인 elapsedTime을 가짐

    private void Start()
    {
        // 씨앗을 처음 심을 때 elapsedTime 초기화
        elapsedTime = 0f;
    }
    public void Update()
    {
        if (positionIndex >= 0 && positionIndex < farmField.positionsState.Length &&
            farmField.positionsState[positionIndex] == FieldState.Planted)
        {
            elapsedTime += Time.deltaTime;

            // growthTime의 절반이 지나면 Growing 상태로 변경
            if (elapsedTime >= growthTime / 2)
            {
                ChangeState(CropState.Growing);
            }
        }
        else
        {
            if (currentState == CropState.Growing)
            {
                elapsedTime += Time.deltaTime;

                if (elapsedTime >= growthTime)
                {
                    ChangeState(CropState.ReadyToHarvest);
                }
            }
        }
    }

    public void ChangeState(CropState newState)
    {
        // 이전 상태에 따라 프리팹 파괴 여부를 결정합니다.
        if (currentState == CropState.Planted)
        {
            // 심어진 상태에서 자라는 중 상태로 변경될 때 심어진 상태 프리팹을 파괴
            Destroy(transform.GetChild(0).gameObject);
            // 작물 상태를 밭에 반영
            if (farmField != null)
            {
                farmField.ChangePositionState(positionIndex, FieldState.Growing);
            }
        }
        else if (currentState == CropState.Growing)
        {
            // 자라는 중 상태에서 완전히 자란 상태로 변경될 때 자라는 중 상태 프리팹을 파괴
            Destroy(transform.GetChild(0).gameObject);
            // 작물 상태를 밭에 반영
            if (farmField != null)
            {
                farmField.ChangePositionState(positionIndex, FieldState.ReadyToHarvest);
            }
        }

        // 상태에 따라 올바른 프리팹을 생성합니다.
        GameObject newPrefab = null;
        if (newState == CropState.Growing)
        {
            newPrefab = Instantiate(plantedSeed.GrowingPrefab, Vector3.zero, Quaternion.identity);
        }
        else if (newState == CropState.ReadyToHarvest)
        {
            newPrefab = Instantiate(plantedSeed.FullyGrownPrefab, Vector3.zero, Quaternion.identity);
        }

        // 생성한 프리팹을 현재 Crop의 자식으로 설정합니다.
        if (newPrefab != null)
        {
            newPrefab.transform.parent = transform;
            newPrefab.transform.localPosition = Vector3.zero; // 로컬 포지션을 (0, 0, 0)으로 설정
            newPrefab.transform.localRotation = Quaternion.identity; // 로컬 회전을 기본값으로 설정
        }

        // 상태 변경
        currentState = newState;
    }
}