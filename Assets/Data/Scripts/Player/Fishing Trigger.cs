using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class FishingTrigger : SystemProPerty
{
    public GameObject toastMessageUI;
    public Text toastMessage;
    public GameObject CanFishUI; // 낚시 상호작용

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
        return true; // 성공적으로 설정되었을 때 true를 반환
    }
    public void OnFishingAnimationStart(Player _player)
    {
        player = _player;
        player = FindObjectOfType<Player>();
        player.Speed = 0;
    }
    // 낚시 성공 또는 실패 체크 및 데이터 추가
    public void OnFishingAnimationEnd()
    {
        Weapon equippedWeapon = mountingSlot.currentEquippedWeapon;
        equippedWeapon.durability--;

        if (equippedWeapon.durability == 0)
        {
            // 내구도 수정해야함, 도구 자체가 사라져야함
            Debug.Log("도구가 부러졌다");
            ShowToastMessage("낚시대가 부러졌다!");
            return;
        }

        if (pondField.isPlayerInPond && mountingSlot.currentEquippedWeapon != null)
        {
            float randomValue = Random.value;
            float squidProbability = 0.20f;       // 오징어 확률 20%
            float troutProbability = 0.25f;      // 잉어 확률 25%
            float pufferfishProbability = 0.05f; // 복어 확률 5%
            float clamProbability = 0.30f;      // 조개 확률 30%
            float shrimpProbability = 0.20f;    // 새우 확률 20%

            if (randomValue <= squidProbability)
            {
              //  Debug.Log("오징어를 낚았다!");
                ShowToastMessage("오징어를 낚았다!");
                Squid squidData = ScriptableObject.CreateInstance<Squid>();
                squidData.Initialize();

                tinventory.AcquireItem(squidData, 1);
            }
            else if (randomValue <= (squidProbability + troutProbability))
            {
              // Debug.Log("잉어를 낚았다!");
                ShowToastMessage("잉어를 낚았다!");
                Trout troutData = ScriptableObject.CreateInstance<Trout>();
                troutData.Initialize();

                tinventory.AcquireItem(troutData, 1);
            }
            else if (randomValue <= (squidProbability + troutProbability + pufferfishProbability))
            {
               // Debug.Log("복어를 낚았다!");
                ShowToastMessage("복어를 낚았다!");
                Pufferfish pufferfishData = ScriptableObject.CreateInstance<Pufferfish>();
                pufferfishData.Initialize();

                tinventory.AcquireItem(pufferfishData, 1);
            }
            else if (randomValue <= (squidProbability + troutProbability + pufferfishProbability + clamProbability))
            {
               // Debug.Log("조개를 낚았다!");
                ShowToastMessage("조개를 낚았다!");
                Clam clamData = ScriptableObject.CreateInstance<Clam>();
                clamData.Initialize();

                tinventory.AcquireItem(clamData, 1);
            }
            else if (randomValue <= (squidProbability + troutProbability + pufferfishProbability + clamProbability + shrimpProbability))
            {
               // Debug.Log("새우를 낚았다!");
                ShowToastMessage("새우를 낚았다!");
                Shirimp shrimpData = ScriptableObject.CreateInstance<Shirimp>();
                shrimpData.Initialize();

                tinventory.AcquireItem(shrimpData, 1);
            }
            else
            {
                // 아무것도 낚지 못했을 때의 처리
                Debug.Log("아무것도 낚지 못했다!");
                ShowToastMessage("아무것도 낚지 못했다!");
            }
        }
        player.Speed = 4;
    }

    // 토스트 메시지 표시
    private void ShowToastMessage(string message)
    {
        toastMessageUI.SetActive(true);
        toastMessage.text = message;
        StartCoroutine(FadeOutToastMessage());
    }

    private IEnumerator FadeOutToastMessage()
    {
        yield return new WaitForSeconds(3.0f); // 3초 대기

        // 3초 후에 토스트 메시지 숨기고 비활성화
        toastMessageUI.SetActive(false);
    }
}
