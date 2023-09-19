using System.Collections.Generic;
using UnityEngine;
public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        //문장이 여러개 일 수 있으니 스트링은 배열로
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "안녕?", "이곳에 처음 왔구나!" });
        talkData.Add(2000, new string[] { "안녕?", "넌 누구니?" });
        talkData.Add(3000, new string[] { "평범한 기둥이다." });
        talkData.Add(4000, new string[] { "T키를 누르면 상점을 열 수 있어!" });
        talkData.Add(5000, new string[] { "나무이다." });

        //퀘스트 대화 - 퀘스트 번호 + npd Id
        talkData.Add(10 + 1000, new string[] { "어서 와. ", "귀농도구를 구하게 해줄께 ", "그런데 나는 잘 몰라.. 핑크에게 가면 정보를 알려줄꺼야!" });
        talkData.Add(11 + 1000, new string[] { "핑크에게 가봐~~!! "});
        talkData.Add(11 + 2000, new string[] { "안녕? ", "귀농도구를 구하게 해줄께 ", "우선 나무가 필요하니 나무에 가서 상호작용(스페이스바)를 해봐!" });

        talkData.Add(20 + 1000, new string[] { "나무? ", "바로 옆에 있어~ " });
        talkData.Add(20 + 2000, new string[] { "음? ", "아직 나무를 구하지 못했구나.." });
        talkData.Add(20 + 5000, new string[] { "나무밑에있는 도끼를 E키를 이용하여 주워줘! ", "인벤토리에 있는 도끼를 퀵슬롯에 장착 후 1번 키를 눌러줘!", 
                                                "그 도끼를 이용해서 나무를 얻을 수 있어"});
        talkData.Add(21 + 1000, new string[] { "도끼를 주워 나무를 구하지 않았구나 ", "다시 나무로 돌아가서 도끼를 주우렴" });
        talkData.Add(21 + 2000, new string[] { "도끼를 주워 나무를 구하지 않았구나 ", "다시 나무로 돌아가서 도끼를 주우렴" });
        talkData.Add(21 + 5000, new string[] { "나무를 얻었다!!" });
        talkData.Add(22 + 1000, new string[] { "오!! ", "나무를 찾았네!! ", " 얼른 핑크에게 가봐!" });
        talkData.Add(22 + 2000, new string[] { "오!! ", "나무를 구해왔구나!! ", " 여기 도구를 줄께!" });
        talkData.Add(22 + 5000, new string[] { "나무를 획득했어!!" });

        talkData.Add(30 + 2000, new string[] { "수고했어 모든 퀘스트가 끝났어!" });

        //퀘스트 대사 하나씩 처리하는 방법 vs 예외처리
    }

    public string GetTalk(int id, int talkIndex)
    {
        if(!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                //퀘스트와 상관없는 물품 이름 넣는거, 원래 대사 등장
                return GetTalk(id - id % 100, talkIndex);
            }
            else
            {
                //해당 퀘스트 진행 중 대사 없을 때 진행 순서, 이 퀘스트 맨 처음 대사 가져옴
                GetTalk(id - id % 10, talkIndex);
            }
        }
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

}
