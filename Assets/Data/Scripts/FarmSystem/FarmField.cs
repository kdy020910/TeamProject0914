using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FieldState
{
    Empty, Tilled, Planted, Growing, ReadyToHarvest
}

public class FarmField : MonoBehaviour
{
    public FieldState[] positionsState; // 각 포지션의 상태 배열
    public List<Item> plantedSeeds = new List<Item>(); // 밭에 심긴 씨앗 목록
    public int maxSeedCount = 9; // 최대 심을 수 있는 씨앗 개수
    public Transform[] seedPositions; // 씨앗을 심을 위치 목록

    public bool isPlayerInFarm = false; // 밭에 들어왔는지 확인 함

    private void Start()
    {
        positionsState = new FieldState[seedPositions.Length];
        for (int i = 0; i < positionsState.Length; i++)
        {
            positionsState[i] = FieldState.Empty;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInFarm = true;
            Debug.Log("밭에 들어옴");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInFarm = false;
            Debug.Log("밭에서 나감");
        }
    }

    public void ChangePositionState(int positionIndex, FieldState newState)
    {
        if (positionIndex >= 0 && positionIndex < positionsState.Length)
        {
            positionsState[positionIndex] = newState;
        }
        else
        {
            Debug.LogWarning("잘못된 포지션 인덱스입니다.");
        }
    }  
}