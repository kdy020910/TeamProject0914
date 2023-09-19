using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class FishingTrigger : SystemProPerty
{
    public GameObject CanFishUI; // 낚시 상호작용

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
            guide.GuideMessage("낚싯대가 부러졌다!");
            return;
        }

        if (pondField.isPlayerInPond && mountingSlot.currentEquippedWeapon != null)
        {
            float randomValue = Random.value;
            float squidProbability = 1f;       // 오징어 확률 20%
            float troutProbability = 1f;      // 잉어 확률 25%
            float pufferfishProbability = 2f; // 복어 확률 5%
            float clamProbability = 0.2f;      // 조개 확률 30%
            float shrimpProbability = 0.20f;    // 새우 확률 20%

            if (randomValue <= squidProbability)
            {
                //Debug.Log("오징어를 낚았다!");

                guide.GuideMessage("오징어를 낚았다!");
                Squid squidData = ScriptableObject.CreateInstance<Squid>();
                squidData.Initialize();

                tinventory.AcquireItem(squidData, 1);
            }
            else if (randomValue <= (squidProbability + troutProbability))
            {
                // Debug.Log("잉어를 낚았다!");

                guide.GuideMessage("잉어를 낚았다!");
                Trout troutData = ScriptableObject.CreateInstance<Trout>();
                troutData.Initialize();

                tinventory.AcquireItem(troutData, 1);
            }
            else if (randomValue <= (squidProbability + troutProbability + pufferfishProbability))
            {
                // Debug.Log("복어를 낚았다!");
                guide.GuideMessage("복어를 낚았다!");
                Pufferfish pufferfishData = ScriptableObject.CreateInstance<Pufferfish>();
                pufferfishData.Initialize();

                tinventory.AcquireItem(pufferfishData, 1);
            }
            else if (randomValue <= (squidProbability + troutProbability + pufferfishProbability + clamProbability))
            {
                // Debug.Log("조개를 낚았다!");
                guide.GuideMessage("조개를 낚았다!");
                Clam clamData = ScriptableObject.CreateInstance<Clam>();
                clamData.Initialize();

                tinventory.AcquireItem(clamData, 1);
            }
            else if (randomValue <= (squidProbability + troutProbability + pufferfishProbability + clamProbability + shrimpProbability))
            {
                // Debug.Log("새우를 낚았다!");
                guide.GuideMessage("새우를 낚았다!");
                Shirimp shrimpData = ScriptableObject.CreateInstance<Shirimp>();
                shrimpData.Initialize();

                tinventory.AcquireItem(shrimpData, 1);
            }
            else
            {
                Debug.Log("아무것도 낚지 못했다!");
                guide.GuideMessage("아무것도 낚지 못했다!");
            }
        }
        player.Speed = 4;
    }
}
