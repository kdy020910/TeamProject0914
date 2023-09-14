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
        //talkText.text = "이것의 이름은" + scanObject.name + "이라고 한다.";
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);
        //스페이스바 누르고 액션할때
       
        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        //대화 시작
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        //대화 끝
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;         // 보이드함수에서 리턴은 강제종료 역할
        }

        //대화 계속
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
