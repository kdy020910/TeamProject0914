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
            Debug.Log("작업대로 왔음");
            isPlayerInDiy = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        CanDiyUi.SetActive(false);
        if (other.CompareTag("Player"))
        {
            Debug.Log("작업대에서 나감");
            isPlayerInDiy = false;
        }
    }
}
