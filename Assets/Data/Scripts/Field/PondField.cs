using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PondField : AnimationSystem
{ 
    public bool isPlayerInPond = false; // 강가에 들어온 상태 여부를 나타내는 변수

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            Debug.Log("낚시 가능 장소에 들어옴");
            isPlayerInPond = true; 
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("낚시 가능 장소에서 나감");
            isPlayerInPond = false; 
        }
    }
}
