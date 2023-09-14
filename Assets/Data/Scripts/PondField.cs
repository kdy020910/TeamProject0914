using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PondField : AnimationSystem
{ 
    public bool isPlayerInPond = false; // ������ ���� ���� ���θ� ��Ÿ���� ����

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            Debug.Log("���� ���� ��ҿ� ����");
            isPlayerInPond = true; 
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("���� ���� ��ҿ��� ����");
            isPlayerInPond = false; 
        }
    }
}
