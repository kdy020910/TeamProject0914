using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class FishingTrigger : SystemProPerty
{
    public GameObject toastMessageUI;
    public Text toastMessage;
    public GameObject CanFishUI; // ���� ��ȣ�ۿ�

    Player player;
    private void Start()
    {
        toastMessageUI.SetActive(false);
        CanFishUI.SetActive(false);
    }
    private void Update()
    {
        HandleFishingPoleAction();
    }
    private void HandleFishingPoleAction()
    {
        if (pondField.isPlayerInPond && mountingSlot.isFishingPoleEquipped)
        {
            CanFishUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                myAnim.SetTrigger("Fishing");
            }
        }
        else
        {
            CanFishUI.SetActive(false);
        }
    }
    public bool SetIsMoving()
    {
        myAnim.SetBool("IsMoving", true);
        return true; // ���������� �����Ǿ��� �� true�� ��ȯ
    }
    public void OnFishingAnimationStart(Player _player)
    {
        player = _player;
        player = FindObjectOfType<Player>();
        player.Speed = 0;
    }
    // ���� ���� �Ǵ� ���� üũ �� ������ �߰�
    public void OnFishingAnimationEnd()
    {
        Weapon equippedWeapon = mountingSlot.currentEquippedWeapon;
        equippedWeapon.durability--;

        if (equippedWeapon.durability == 0)
        {
            // ������ �����ؾ���, ���� ��ü�� ���������
            Debug.Log("������ �η�����");
            ShowToastMessage("���ô밡 �η�����!");
            return;
        }

        if (pondField.isPlayerInPond && mountingSlot.currentEquippedWeapon != null)
        {
            float randomValue = Random.value;
            float squidProbability = 0.20f;       // ��¡�� Ȯ�� 20%
            float troutProbability = 0.25f;      // �׾� Ȯ�� 25%
            float pufferfishProbability = 0.05f; // ���� Ȯ�� 5%
            float clamProbability = 0.30f;      // ���� Ȯ�� 30%
            float shrimpProbability = 0.20f;    // ���� Ȯ�� 20%

            if (randomValue <= squidProbability)
            {
              //  Debug.Log("��¡� ���Ҵ�!");
                ShowToastMessage("��¡� ���Ҵ�!");
                Squid squidData = ScriptableObject.CreateInstance<Squid>();
                squidData.Initialize();

                tinventory.AcquireItem(squidData, 1);
            }
            else if (randomValue <= (squidProbability + troutProbability))
            {
              // Debug.Log("�׾ ���Ҵ�!");
                ShowToastMessage("�׾ ���Ҵ�!");
                Trout troutData = ScriptableObject.CreateInstance<Trout>();
                troutData.Initialize();

                tinventory.AcquireItem(troutData, 1);
            }
            else if (randomValue <= (squidProbability + troutProbability + pufferfishProbability))
            {
               // Debug.Log("��� ���Ҵ�!");
                ShowToastMessage("��� ���Ҵ�!");
                Pufferfish pufferfishData = ScriptableObject.CreateInstance<Pufferfish>();
                pufferfishData.Initialize();

                tinventory.AcquireItem(pufferfishData, 1);
            }
            else if (randomValue <= (squidProbability + troutProbability + pufferfishProbability + clamProbability))
            {
               // Debug.Log("������ ���Ҵ�!");
                ShowToastMessage("������ ���Ҵ�!");
                Clam clamData = ScriptableObject.CreateInstance<Clam>();
                clamData.Initialize();

                tinventory.AcquireItem(clamData, 1);
            }
            else if (randomValue <= (squidProbability + troutProbability + pufferfishProbability + clamProbability + shrimpProbability))
            {
               // Debug.Log("���츦 ���Ҵ�!");
                ShowToastMessage("���츦 ���Ҵ�!");
                Shirimp shrimpData = ScriptableObject.CreateInstance<Shirimp>();
                shrimpData.Initialize();

                tinventory.AcquireItem(shrimpData, 1);
            }
            else
            {
                // �ƹ��͵� ���� ������ ���� ó��
                Debug.Log("�ƹ��͵� ���� ���ߴ�!");
                ShowToastMessage("�ƹ��͵� ���� ���ߴ�!");
            }
        }
        player.Speed = 4;
    }

    // �佺Ʈ �޽��� ǥ��
    private void ShowToastMessage(string message)
    {
        toastMessageUI.SetActive(true);
        toastMessage.text = message;
        StartCoroutine(FadeOutToastMessage());
    }

    private IEnumerator FadeOutToastMessage()
    {
        yield return new WaitForSeconds(3.0f); // 3�� ���

        // 3�� �Ŀ� �佺Ʈ �޽��� ����� ��Ȱ��ȭ
        toastMessageUI.SetActive(false);
    }
}
