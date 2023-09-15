using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;
    Dictionary<int, Sprite> NowData;

    public Sprite[] portraitArr;
    public Sprite NowQuest;

    void Awake()
    {
        //������ ������ �� �� ������ ��Ʈ���� �迭��
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "�ȳ�?:0", "�̰��� ó�� �Ա���!:1" });
        talkData.Add(2000, new string[] { "�ȳ�?:0", "�� ������?:1" });
        talkData.Add(3000, new string[] { "����� ����̴�." });
        talkData.Add(4000, new string[] { "eŰ�� ������ ������ �� �� �־�!" });
        talkData.Add(5000, new string[] { "�����̴�." });

        //����Ʈ ��ȭ - ����Ʈ ��ȣ + npd Id
        talkData.Add(10 + 1000, new string[] { "� ��. :0", "�ͳ󵵱��� ���ϰ� ���ٲ� :1", "�׷��� ���� �� ����.. ��ũ���� ���� ������ �˷��ٲ���!:2" });
        talkData.Add(11 + 1000, new string[] { "��ũ���� ����~~!! :1"});
        talkData.Add(11 + 2000, new string[] { "�ȳ�? :0", "�ͳ󵵱��� ���ϰ� ���ٲ� :1", "�켱 ������ �ʿ��ϴ� ������ ���� ��ȣ�ۿ�(�����̽���)�� �غ�!:2" });

        talkData.Add(20 + 1000, new string[] { "����? :0", "�ٷ� ���� �־�~ :1" });
        talkData.Add(20 + 2000, new string[] { "��? :0", "���� ������ ������ ���߱���..:2" });
        talkData.Add(20 + 5000, new string[] { "������ ã�Ҵ�.", "��ũ���� ���ư���!" });
        talkData.Add(21 + 1000, new string[] { "��!! :0", "������ ã�ҳ�!! :1", " �� ��ũ���� ����!:2" });
        talkData.Add(21 + 2000, new string[] { "��!! :0", "������ ���ؿԱ���!! :1", " ���� ������ �ٲ�!:2" });

        //����Ʈ ��� �ϳ��� ó���ϴ� ��� vs ����ó��

        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);

    }

    public string GetTalk(int id, int talkIndex)
    {
        if(!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                //����Ʈ�� ������� ��ǰ �̸� �ִ°�, ���� ��� ����
                return GetTalk(id - id % 100, talkIndex);
            }
            else
            {
                //�ش� ����Ʈ ���� �� ��� ���� �� ���� ����, �� ����Ʈ �� ó�� ��� ������
                GetTalk(id - id % 10, talkIndex);
                
                //�긦 GetTalk�� �ٿ��� �ڵ����̱�~~
                /*if (talkData.ContainsKey(id - id % 10))
                {
                    return null;
                }
                else
                    return talkData[id - id % 10][talkIndex];*/
            }
        }
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
