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
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;

    

    void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        //talkText.text = "�̰��� �̸���" + scanObject.name + "�̶�� �Ѵ�.";
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);
        //�����̽��� ������ �׼��Ҷ�
       
        talkPanel.SetActive(isAction);
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

}
