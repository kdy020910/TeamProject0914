using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI ����
public class UIManager : TSingleton<UIManager>
{
    private GuideUI guide;

    private readonly bool ShowUI = true;

    [Header("���ε�")]
    [SerializeField] GameObject InvenUI;        // Ref Inventory UI
    [SerializeField] GameObject CollectBookUI;  // Ref Collection UI

    private void Awake()
    {
        Init();
        SetAllUI();
        guide = FindObjectOfType<GuideUI>().GetComponent<GuideUI>();
    }

    private void Update()
    {
        TryTogleUI();
    }

    public void ShowInvenUI() => InvenUI.SetActive(true);
    public void HideInvenUI() => InvenUI.SetActive(false);

    public void ShowBookUI() => CollectBookUI.SetActive(true);
    public void HideBookUI() => CollectBookUI.SetActive(false);

    public void HideGuide() => guide.GuidePanel.SetActive(false);
    public void ShowGuide() => guide.GuidePanel.SetActive(true);

    private void SetAllUI()
    {
        HideInvenUI();
        HideBookUI();
    }

    #region UI On/Off
    public void TryTogleUI()
    {
        CheckingUI();
        if (Input.GetKeyDown(KeyCode.I))
        {
            CheckingUI();
            if (InvenUI.activeSelf == !ShowUI)
                ShowInvenUI();
            else
                HideInvenUI();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            CheckingUI();
            if (CollectBookUI.activeSelf == !ShowUI)
                ShowBookUI();
            else
                HideBookUI();
        }
    }
    #endregion

    #region UI���� ���� �����ִ��� Ȯ��
    public void CheckingUI()
    {
        if (InvenUI.activeSelf == ShowUI) HideBookUI();
        if (CollectBookUI.activeSelf == ShowUI) HideInvenUI();
    }
    #endregion


}
