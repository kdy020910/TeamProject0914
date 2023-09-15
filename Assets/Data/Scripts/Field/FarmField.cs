using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FieldState
{
    Empty, Tilled, Planted, Growing, ReadyToHarvest
}

public class FarmField : SystemProPerty
{
    public FieldState currentState = FieldState.Empty;
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
