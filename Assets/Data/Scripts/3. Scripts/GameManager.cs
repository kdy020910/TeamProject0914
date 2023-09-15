using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public GameObject talkPanel;
    public Image portraitImg;
    public TextMeshProUGUI talkText;
    public TextMeshProUGUI[] playerCoinText;
    public GameObject scanObject;
    
    public bool isAction;
    public int talkIndex;

    public Player player;

    void Start()
    {
        Debug.Log(questManager.CheckQuest());
        talkPanel.SetActive(false);
        /*InvenPanel.transform.position = Vector3.zero;
        InvenPanel.SetActive(true);*/
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        //talkText.text = "�̰��� �̸���" + scanObject.name + "�̶�� �Ѵ�.";
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);
        //�����̽��� ������ �׼��Ҷ�
       
        talkPanel.SetActive(isAction);

        /*scanObject = null;
        if (scanObject == null)
        {
            isAction = false;
        }*/
    }

    void Talk(int id, bool isNpc)
    {
        //��ȭ ����
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        //��ȭ ��
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            scanObject = null;
            return;         // ���̵��Լ����� ������ �������� ����
        }

        //��ȭ ���
        if(isNpc)
        {
            talkText.text = talkData.Split(':')[0];

            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;

            portraitImg.color = new(1, 1, 1, 0);

        }
        isAction = true;
        talkIndex++;
    }

    void Update()
    {
        playerCoinText[0].text = string.Format("{0:N0}", player.coin);
        playerCoinText[1].text = string.Format("{0:N0}", player.coin);
    }

}
