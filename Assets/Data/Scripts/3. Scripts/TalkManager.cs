using System.Collections.Generic;
using UnityEngine;
public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        //������ ������ �� �� ������ ��Ʈ���� �迭��
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "�ȳ�?", "�̰��� ó�� �Ա���!" });
        talkData.Add(2000, new string[] { "�ȳ�?", "�� ������?" });
        talkData.Add(3000, new string[] { "����� ����̴�." });
        talkData.Add(4000, new string[] { "TŰ�� ������ ������ �� �� �־�!" });
        talkData.Add(5000, new string[] { "�����̴�." });

        //����Ʈ ��ȭ - ����Ʈ ��ȣ + npd Id
        talkData.Add(10 + 1000, new string[] { "� ��. ", "�ͳ󵵱��� ���ϰ� ���ٲ� ", "�׷��� ���� �� ����.. ��ũ���� ���� ������ �˷��ٲ���!" });
        talkData.Add(11 + 1000, new string[] { "��ũ���� ����~~!! "});
        talkData.Add(11 + 2000, new string[] { "�ȳ�? ", "�ͳ󵵱��� ���ϰ� ���ٲ� ", "�켱 ������ �ʿ��ϴ� ������ ���� ��ȣ�ۿ�(�����̽���)�� �غ�!" });

        talkData.Add(20 + 1000, new string[] { "����? ", "�ٷ� ���� �־�~ " });
        talkData.Add(20 + 2000, new string[] { "��? ", "���� ������ ������ ���߱���.." });
        talkData.Add(20 + 5000, new string[] { "�����ؿ��ִ� ������ EŰ�� �̿��Ͽ� �ֿ���! ", "�κ��丮�� �ִ� ������ �����Կ� ���� �� 1�� Ű�� ������!", 
                                                "�� ������ �̿��ؼ� ������ ���� �� �־�"});
        talkData.Add(21 + 1000, new string[] { "������ �ֿ� ������ ������ �ʾұ��� ", "�ٽ� ������ ���ư��� ������ �ֿ��" });
        talkData.Add(21 + 2000, new string[] { "������ �ֿ� ������ ������ �ʾұ��� ", "�ٽ� ������ ���ư��� ������ �ֿ��" });
        talkData.Add(21 + 5000, new string[] { "������ �����!!" });
        talkData.Add(22 + 1000, new string[] { "��!! ", "������ ã�ҳ�!! ", " �� ��ũ���� ����!" });
        talkData.Add(22 + 2000, new string[] { "��!! ", "������ ���ؿԱ���!! ", " ���� ������ �ٲ�!" });
        talkData.Add(22 + 5000, new string[] { "������ ȹ���߾�!!" });

        talkData.Add(30 + 2000, new string[] { "�����߾� ��� ����Ʈ�� ������!" });

        //����Ʈ ��� �ϳ��� ó���ϴ� ��� vs ����ó��
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
            }
        }
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

}
