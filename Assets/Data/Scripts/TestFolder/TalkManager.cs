using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;
    

    void Awake()
    {
        //문장이 여러개 일 수 있으니 스트링은 배열로
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }
    void GenerateData()
    {
        talkData.Add(1000, new string[] { "안녕?:0", "이곳에 처음 왔구나!:1" });
        talkData.Add(2000, new string[] { "안녕?:0", "넌 누구니?:1" });
        talkData.Add(3000, new string[] { "평범한 기둥이다." });
        talkData.Add(4000, new string[] { "e키를 누르면 상점을 열 수 있어!" });

        //퀘스트 대화 - 퀘스트 번호 + npd Id
        talkData.Add(10 + 1000, new string[] { "어서 와. :0", "귀농도구를 구하게 해줄께 :1", "그런데 나는 잘 몰라.. 핑크에게 가면 정보를 알려줄꺼야!:2" });
        talkData.Add(11 + 1000, new string[] { "핑크에게 가봐~~!! :1"});
        talkData.Add(11 + 2000, new string[] { "안녕? :0", "귀농도구를 구하게 해줄께 :1", "우선 나무가 필요하니 나무에 가서 상호작용(스페이스바)를 해봐!:2" });

        talkData.Add(20 + 1000, new string[] { "나무? :0", "바로 옆에 있어~ :1" });
        talkData.Add(20 + 2000, new string[] { "음? :0", "아직 나무를 구하지 못했구나..:2" });
        talkData.Add(20 + 5000, new string[] { "나무를 찾았다." });
        talkData.Add(21 + 1000, new string[] { "오!! :0", "나무를 찾았네!! :1", " 얼른 핑크에게 가봐!:2" });
        talkData.Add(21 + 2000, new string[] { "오!! :0", "나무를 구해왔구나!! :1", " 여기 도구를 줄께!:2" });

        //퀘스트 대사 하나씩 처리하는 방법 vs 예외처리


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
                //퀘스트와 상관없는 물품 이름 넣는거, 원래 대사 등장
                return GetTalk(id - id % 100, talkIndex);
            }
            else
            {
                //해당 퀘스트 진행 중 대사 없을 때 진행 순서, 이 퀘스트 맨 처음 대사 가져옴
                GetTalk(id - id % 10, talkIndex);
                
                //얘를 GetTalk로 줄여서 코드줄이기~~
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
