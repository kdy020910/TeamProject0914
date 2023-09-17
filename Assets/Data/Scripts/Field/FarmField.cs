using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum FieldState
{
    Empty, Tilled, Planted, Growing, ReadyToHarvest
}

public class FarmField : SystemProPerty
{
    public FieldState currentState = FieldState.Empty; // 현재 밭의 상태
    public List<Item> plantedSeeds = new List<Item>(); // 밭에 심긴 씨앗 목록
    public int maxSeedCount = 9; // 최대 심을 수 있는 씨앗 개수
    public List<Transform> seedPositions = new List<Transform>(); // 씨앗을 심을 위치 목록

    public bool isPlayerInFarm = false; // 밭에 들어왔는지 확인 함
                                        
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInFarm = true;
            Debug.Log("밭에 들어옴");
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInFarm = false;
            Debug.Log("밭에서 나감"); 
        }
    }
}
