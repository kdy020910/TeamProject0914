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
    public List<Item>[] plantedSeedsList; // �� �����ǿ� ���� ���� ��� �迭
    public int maxSeedCount = 9; // �ִ� ���� �� �ִ� ���� ����
    public Transform[] seedPositions; // ������ ���� ��ġ ���

    public bool isPlayerInFarm = false; // �翡 ���Դ��� Ȯ�� ��

    private void Start()
    {
        positionsState = new FieldState[seedPositions.Length];
        plantedSeedsList = new List<Item>[seedPositions.Length]; // �� �����ǿ� ���� ����Ʈ �迭 �ʱ�ȭ
        for (int i = 0; i < positionsState.Length; i++)
        {
            positionsState[i] = FieldState.Empty;
            plantedSeedsList[i] = new List<Item>(); // �� �����ǿ� ���� ����Ʈ �ʱ�ȭ
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