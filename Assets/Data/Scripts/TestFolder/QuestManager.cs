using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questobject;

    Dictionary<int, QuestData> questList;

    // Start is called before the first frame update
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        questList.Add(10, new QuestData("시골 상경", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("도구 얻기", new int[] { 5000, 2000 }));
        questList.Add(30, new QuestData("퀘스트 끝!", new int[] { 0 }));/*
        questList.Add(40, new QuestData("퀘스트 끝!", new int[] { 0 }));*/
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
                if(questActionIndex == 2)
                {
                    questobject[0].SetActive(true);
                }
                break;
            case 20:
                if (questActionIndex == 1)
                {
                    questobject[0].SetActive(false);
                    questobject[1].SetActive(true);
                }
                break;
        }
    }
}
