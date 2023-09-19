using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FieldState
{
    Empty, Tilled, Planted, Growing, ReadyToHarvest
}

public class FarmField : MonoBehaviour
{
    public FieldState[] positionsState; // �� �������� ���� �迭
    public List<Item> plantedSeeds = new List<Item>(); // �翡 �ɱ� ���� ���
    public int maxSeedCount = 9; // �ִ� ���� �� �ִ� ���� ����
    public Transform[] seedPositions; // ������ ���� ��ġ ���

    public bool isPlayerInFarm = false; // �翡 ���Դ��� Ȯ�� ��

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
            Debug.Log("�翡 ����");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInFarm = false;
            Debug.Log("�翡�� ����");
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
            Debug.LogWarning("�߸��� ������ �ε����Դϴ�.");
        }
    }  
}