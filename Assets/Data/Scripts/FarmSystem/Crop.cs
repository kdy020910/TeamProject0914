using System.Collections.Generic;
using UnityEngine;

public enum CropState
{
    Planted,        // �ɾ��� ����
    Growing,        // �ڶ�� �� ����
    ReadyToHarvest      // ������ �ڶ� ����
}

public class Crop : SystemProPerty
{
    public string cropID;

    // Crop Ŭ���� ������ positionIndex ������ ����
    public int positionIndex;

    [SerializeField]
    private CropState currentState = CropState.Planted;
    public float growthTime = 60f;      // ������� �ɸ��� �ð� 

    // ������ �κ�: �� �۹����� � ������ �ɾ����� ��Ÿ���� ����
    public Item plantedSeed;

    public float elapsedTime = 0f; // �������� elapsedTime�� ����

    private void Start()
    {
        // ������ ó�� ���� �� elapsedTime �ʱ�ȭ
        elapsedTime = 0f;
    }
    public void Update()
    {
        if (positionIndex >= 0 && positionIndex < farmField.positionsState.Length &&
            farmField.positionsState[positionIndex] == FieldState.Planted)
        {
            elapsedTime += Time.deltaTime;

            // growthTime�� ������ ������ Growing ���·� ����
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
        // ���� ���¿� ���� ������ �ı� ���θ� �����մϴ�.
        if (currentState == CropState.Planted)
        {
            // �ɾ��� ���¿��� �ڶ�� �� ���·� ����� �� �ɾ��� ���� �������� �ı�
            Destroy(transform.GetChild(0).gameObject);
            // �۹� ���¸� �翡 �ݿ�
            if (farmField != null)
            {
                farmField.ChangePositionState(positionIndex, FieldState.Growing);
            }
        }
        else if (currentState == CropState.Growing)
        {
            // �ڶ�� �� ���¿��� ������ �ڶ� ���·� ����� �� �ڶ�� �� ���� �������� �ı�
            Destroy(transform.GetChild(0).gameObject);
            // �۹� ���¸� �翡 �ݿ�
            if (farmField != null)
            {
                farmField.ChangePositionState(positionIndex, FieldState.ReadyToHarvest);
            }
        }

        // ���¿� ���� �ùٸ� �������� �����մϴ�.
        GameObject newPrefab = null;
        if (newState == CropState.Growing)
        {
            newPrefab = Instantiate(plantedSeed.GrowingPrefab, Vector3.zero, Quaternion.identity);
        }
        else if (newState == CropState.ReadyToHarvest)
        {
            newPrefab = Instantiate(plantedSeed.FullyGrownPrefab, Vector3.zero, Quaternion.identity);
        }

        // ������ �������� ���� Crop�� �ڽ����� �����մϴ�.
        if (newPrefab != null)
        {
            newPrefab.transform.parent = transform;
            newPrefab.transform.localPosition = Vector3.zero; // ���� �������� (0, 0, 0)���� ����
            newPrefab.transform.localRotation = Quaternion.identity; // ���� ȸ���� �⺻������ ����
        }

        // ���� ����
        currentState = newState;
    }
}