using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum FieldState
{
    Empty, Tilled, Planted, Growing, ReadyToHarvest
}

public class FarmField : SystemProPerty
{
    public FieldState currentState = FieldState.Empty; // ���� ���� ����
    public List<Item> plantedSeeds = new List<Item>(); // �翡 �ɱ� ���� ���
    public int maxSeedCount = 9; // �ִ� ���� �� �ִ� ���� ����
    public List<Transform> seedPositions = new List<Transform>(); // ������ ���� ��ġ ���

    public bool isPlayerInFarm = false; // �翡 ���Դ��� Ȯ�� ��
                                        
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInFarm = true;
            Debug.Log("�翡 ����");
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInFarm = false;
            Debug.Log("�翡�� ����"); 
        }
    }
}
