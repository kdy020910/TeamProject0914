using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class FishingTrigger : SystemProPerty
{
    public GameObject CanFishUI; // ���� ��ȣ�ۿ�

    Player player;
    GuideUI guide;
    private void Start()
    {
        guide = FindObjectOfType<GuideUI>().GetComponent<GuideUI>();
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
            guide.GuideMessage("���˴밡 �η�����!");
            return;
        }

        if (pondField.isPlayerInPond && mountingSlot.currentEquippedWeapon != null)
        {
            float randomValue = Random.value;
            float squidProbability = 1f;       // ��¡�� Ȯ�� 20%
            float troutProbability = 1f;      // �׾� Ȯ�� 25%
            float pufferfishProbability = 2f; // ���� Ȯ�� 5%
            float clamProbability = 0.2f;      // ���� Ȯ�� 30%
            float shrimpProbability = 0.20f;    // ���� Ȯ�� 20%

            if (randomValue <= squidProbability)
            {
                //Debug.Log("��¡� ���Ҵ�!");

                guide.GuideMessage("��¡� ���Ҵ�!");
                Squid squidData = ScriptableObject.CreateInstance<Squid>();
                squidData.Initialize();

                tinventory.AcquireItem(squidData, 1);
            }
            else if (randomValue <= (squidProbability + troutProbability))
            {
                // Debug.Log("�׾ ���Ҵ�!");

                guide.GuideMessage("�׾ ���Ҵ�!");
                Trout troutData = ScriptableObject.CreateInstance<Trout>();
                troutData.Initialize();

                tinventory.AcquireItem(troutData, 1);
            }
            else if (randomValue <= (squidProbability + troutProbability + pufferfishProbability))
            {
                // Debug.Log("��� ���Ҵ�!");
                guide.GuideMessage("��� ���Ҵ�!");
                Pufferfish pufferfishData = ScriptableObject.CreateInstance<Pufferfish>();
                pufferfishData.Initialize();

                tinventory.AcquireItem(pufferfishData, 1);
            }
            else if (randomValue <= (squidProbability + troutProbability + pufferfishProbability + clamProbability))
            {
                // Debug.Log("������ ���Ҵ�!");
                guide.GuideMessage("������ ���Ҵ�!");
                Clam clamData = ScriptableObject.CreateInstance<Clam>();
                clamData.Initialize();

                tinventory.AcquireItem(clamData, 1);
            }
            else if (randomValue <= (squidProbability + troutProbability + pufferfishProbability + clamProbability + shrimpProbability))
            {
                // Debug.Log("���츦 ���Ҵ�!");
                guide.GuideMessage("���츦 ���Ҵ�!");
                Shirimp shrimpData = ScriptableObject.CreateInstance<Shirimp>();
                shrimpData.Initialize();

                tinventory.AcquireItem(shrimpData, 1);
            }
            else
            {
                Debug.Log("�ƹ��͵� ���� ���ߴ�!");
                guide.GuideMessage("�ƹ��͵� ���� ���ߴ�!");
            }
        }
        player.Speed = 4;
    }
}
