using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//샵 유아이 나와야하는 것들에 전부 넣기
public class Shop : MonoBehaviour
{
    public GameObject ShopPanel;
    //public RectTransform shopUI;

    public GameObject[] itemObj;
    public int[] itemPrice;
    public Transform itemPos;
    public TextMeshProUGUI talkText;
    public string[] talkData;

    Player enterPlayer;
    public void Enter(Player player)
    {
        enterPlayer = player;
        //shopUI.anchoredPosition = Vector3.zero;

        ShopPanel.SetActive(true);
    }
    public void Exit()
    {
        //shopUI.anchoredPosition = Vector3.down * 1000;
        ShopPanel.SetActive(false);
    }

    public void Buy(int index)
    {
        int price = itemPrice[index];
        if(price > enterPlayer.coin)
        {
            StopAllCoroutines();
            StartCoroutine(Talk());
            return;
        }

        enterPlayer.coin -= price;
        Vector3 ranVec = Vector3.right * Random.Range(-3, 3) + Vector3.forward * Random.Range(-3, 3);
        Instantiate(itemObj[index], itemPos.position + ranVec, itemPos.rotation);
    }
    
    IEnumerator Talk()
    {
        talkText.text = talkData[1];
        yield return new WaitForSeconds[2];
        talkText.text = talkData[0];
    }
}
