using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//샵 유아이 나와야하는 것들에 전부 넣기
public class Shop : MonoBehaviour
{
    public GameObject ShopPanel;
    //public GameObject InvenPanel;
    //public GameObject[] ShopPanell;
    public RectTransform InvenUI;

    public GameObject[] itemObj;
    public int[] itemPrice;
    public Transform itemPos;
    public TextMeshProUGUI talkText;
    public string[] talkData;

    public Player enterPlayer;

    void Start()
    {
        ShopPanel.SetActive(false);
        //ShopPanel[0].SetActive(false);
        //coin.SetActive(true);
        //InvenPanel.transform.position = new Vector3(0, 0, 0);
    }
    public void Enter(Player player)
    {
        enterPlayer = player;
        InvenUI.anchoredPosition = new Vector3(300, 0, 0);

        ShopPanel.SetActive(true);
    }
    public void Exit()
    {
        ShopPanel.SetActive(false);
        InvenUI.anchoredPosition = new Vector3(0, 0, 0);

        /*InvenPanel.SetActive(false);

        InvenPanel.transform.position = new Vector3(0, 0, 0);*/
    }

    public void Buy(int index)
    {
        int price = itemPrice[index];
        if(price > enterPlayer.coin)
        {
            StopCoroutine(Talk());

            StartCoroutine(Talk());
            return;
        }

        enterPlayer.coin -= price;
        Vector3 ranVec = Vector3.right * Random.Range(-2, 2) + Vector3.forward * Random.Range(-2, 2);
        Instantiate(itemObj[index], itemPos.position + ranVec, itemPos.rotation);
    }
    
    IEnumerator Talk()
    {
        talkText.text = talkData[1];
        Debug.Log("구매");
        yield return new WaitForSecondsRealtime(1.0f);
        Debug.Log("1초 후");
        talkText.text = talkData[0];

    }
}
