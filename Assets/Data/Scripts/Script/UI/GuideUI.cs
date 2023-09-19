using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �� ��ũ��Ʈ�� ���� ���� �Ǵ� �ȳ��� ���� ��������ϴ�.
/// </summary>
public class GuideUI : MonoBehaviour
{
    private UIManager Manager;

    [Header("���ε�")]
    public Text         GuideMsg;    // �ȳ��޽��� text ���ε�
    public GameObject   GuidePanel;  // �ȳ��޽��� (�˾�)������

    // �� �����ڴ� UI Canvas�� �־���մϴ�.
    // GuideMsg�� Text(Legacy)�ν�, ��µ� �޽����� ��Ÿ���� �ʵ尪�Դϴ�.
    // GuidePanel�� Msg�� �θ� ��ġ�� �����ϸ� �˾�â ������ �ϱ⿡ Image�� �ʿ��մϴ�.
    // (Msg�� ���� OnOff�� ���۽�Ű�� �����Դϴ�.)

    private void Start()
    {
        // �׽�Ʈ������ ����Ǵ� �����Դϴ�.
        // �Ž����ٸ� GuideUI.cs�� Start������ ��°�� �����ּ���.

        Manager = FindObjectOfType<UIManager>().GetComponent<UIManager>();

        GuideMessage
            ("������ �ʿ��ϽŰ���?\n" +
            "�̰��� �ý��� �ȳ� �޽��� �Դϴ�.\n" +
            "�������� �ƹ� Ű�� ��������.");

    }

    private void Update()
    {
        //�� �޽����� �ƹ� Ű�� ������ �����ϴ�.
        if (Input.anyKeyDown)
            Manager.HideGuide();
    }

    // �ȳ��� �Լ�
    public void GuideMessage(string msg)
    {
        Manager.ShowGuide();
        GuideMsg.text = msg;
    }

    // GuideMessage ��� ���� �Ʒ� �Լ��� �߰��Ͽ�
    // Awake �Ǵ� Start������ CallRefGuide()�� ���� ȣ�����ּ���.

    /*public void CallRefGuide()
    {
        if (guide == null)
        {
            guide = FindObjectOfType<GuideUI>().GetComponent<GuideUI>();

            if (guide == null) // �������� null�ΰ��� ���� ���� Ž��
            {
                print
                      ("guide�� �������� ã�� �� ���� null�̹Ƿ� �����մϴ�." +
                      "GuideUI Script Component�� ������ GameObject�� �ִ��� Ȯ�����ּ���.");
                return;
            }
        }
    }*/
}
