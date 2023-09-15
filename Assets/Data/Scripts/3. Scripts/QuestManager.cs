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
        GenerateData();
    }


    void GenerateData()
    {
        questList.Add(10, new QuestData("�ð� ���", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("���� ���", new int[] { 5000, 2000 }));
        questList.Add(30, new QuestData("����Ʈ ��!", new int[] { 0 }));
    }
    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    //�̸��� ������ �ٸ��Լ� �Ű����� ������ �����Լ� ȣ�� ������ �ؿ� �Լ� ȣ��, �����ε��̴�
    public string CheckQuest(int id)
    {

        if (id == questList[questId].npcId[questActionIndex])
        { 
            questActionIndex++;
        }
        
        //����Ʈ ������Ʈ ����
        ControlObject();

        if (questActionIndex == questList[questId].npcId.Length)
        {
            NextQuest();
        }
        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        //����Ʈ �̸�
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
                    player.coin = player.coin + 10;
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