using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiyField : SystemProPerty
{
    public GameObject CanDiyUi;
    public bool isPlayerInDiy = false;
    private void Start()
    {
        CanDiyUi.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanDiyUi.SetActive(true);
            Debug.Log("�۾���� ����");
            isPlayerInDiy = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        CanDiyUi.SetActive(false);
        if (other.CompareTag("Player"))
        {
            Debug.Log("�۾��뿡�� ����");
            isPlayerInDiy = false;
        }
    }
}
