using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 이 스크립트는 각종 도움말 또는 안내를 위해 만들었습니다.
/// </summary>
public class GuideUI : MonoBehaviour
{
    private UIManager Manager;

    [Header("바인딩")]
    public Text         GuideMsg;    // 안내메시지 text 바인딩
    public GameObject   GuidePanel;  // 안내메시지 (팝업)윈도우

    // 두 참조자는 UI Canvas에 있어야합니다.
    // GuideMsg는 Text(Legacy)로써, 출력될 메시지를 나타내는 필드값입니다.
    // GuidePanel은 Msg의 부모 위치에 존재하며 팝업창 역할을 하기에 Image가 필요합니다.
    // (Msg와 같이 OnOff로 동작시키기 위함입니다.)

    private void Start()
    {
        // 테스트용으로 실행되는 문구입니다.
        // 거슬린다면 GuideUI.cs의 Start문까지 통째로 지워주세요.

        Manager = FindObjectOfType<UIManager>().GetComponent<UIManager>();

        GuideMessage
            ("도움이 필요하신가요?\n" +
            "이것은 시스템 안내 메시지 입니다.\n" +
            "닫으려면 아무 키나 누르세요.");

    }

    private void Update()
    {
        //이 메시지는 아무 키나 누르면 닫힙니다.
        if (Input.anyKeyDown)
            Manager.HideGuide();
    }

    // 안내용 함수
    public void GuideMessage(string msg)
    {
        Manager.ShowGuide();
        GuideMsg.text = msg;
    }

    // GuideMessage 기능 사용시 아래 함수를 추가하여
    // Awake 또는 Start문에서 CallRefGuide()를 먼저 호출해주세요.

    /*public void CallRefGuide()
    {
        if (guide == null)
        {
            guide = FindObjectOfType<GuideUI>().GetComponent<GuideUI>();

            if (guide == null) // 참조값이 null인가에 대한 이중 탐색
            {
                print
                      ("guide의 참조값을 찾을 수 없어 null이므로 리턴합니다." +
                      "GuideUI Script Component가 부착된 GameObject가 있는지 확인해주세요.");
                return;
            }
        }
    }*/
}
