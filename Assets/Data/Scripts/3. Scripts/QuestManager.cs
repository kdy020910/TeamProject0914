using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questobject;
    public Player player;

    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        for (int i = 0; i < questobject.Length; i++)
        {
            questobject[i].SetActive(false);
        }
        GenerateData();
    }


    void GenerateData()
    {
        questList.Add(10, new QuestData("시골 상경", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("도구 얻기", new int[] { 5000, 5000, 2000 }));
        questList.Add(30, new QuestData("돈 얻기!", new int[] { 2000 }));
        questList.Add(40, new QuestData("퀘스트 끝!", new int[] { 0 }));
    }
    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    //이름이 같지만 다른함수 매개변수 있으면 위에함수 호출 없으면 밑에 함수 호출, 오버로딩이댜
    public string CheckQuest(int id)
    {

        if (id == questList[questId].npcId[questActionIndex])
        { 
            questActionIndex++;
        }
        
        //퀘스트 오브젝트 관리
        ControlObject();

        if (questActionIndex == questList[questId].npcId.Length)
        {
            NextQuest();
        }
        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        //퀘스트 이름
        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject()
    {
        switch(questId)
        {
            case 10:
                if (questActionIndex == 2)
                {
                    questobject[0].SetActive(true);
                    questobject[1].SetActive(true);
                    player.coin = player.coin + 10;
                }
                break;
            case 20:
                if (questActionIndex == 3)
                {
                    //questobject[0].SetActive(false);
                    questobject[2].SetActive(true);
                    questobject[3].SetActive(true);
                    player.coin = player.coin + 2000;
                }
                break;
            case 30:
                if (questActionIndex == 0)
                {
                    //player.coin = player.coin + 1;
                }
                break;
        }
    }
}
