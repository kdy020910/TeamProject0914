using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ������â ������ ���� Ŭ���ϸ� ������ �̸�, ������ �����ϰ� � ��ᰡ �ʿ����� ������
// ������ �������� ���� �� ������ ���� �����ϰ� Ŭ���Ҷ� �߰��ϱ�

public class DIYUIController : SystemProPerty
{
    private Image currentClickImage; // ���� Ŭ���� ������ clickImage
    public Sprite clickImage;
    public Transform[] slotParents; // ���Ե��� �ִ� �θ� Transform

    public Vector2 imageSize = new Vector2(84f, 84f); // ���ϴ� �̹��� ũ��

    public Image recipeImage; // ������ �̹��� ǥ���� UI Image
    public TextMeshProUGUI recipeNameText; // ������ �̸��� ǥ���� Text
    public TextMeshProUGUI recipeDescriptionText; // ������ ������ ǥ���� Text

    public Image[] ingredientImages; // ��� �̹��� ui
    public TextMeshProUGUI[] ingredientNameTexts; // ��� �̸� ui
    public TextMeshProUGUI[] ingredientAmountTexts; // ��� ���� ui 

    private bool canCraft = false; // ������ ���� ���� ����
    public Button diyCraftButton; // DIY ���� ��ư
    public TextMeshProUGUI craftButtonText; // DIY ���� ��ư �ؽ�Ʈ

    public GameObject toastMessageUI;
    public Text toastMessage;

    public void Start()
    {
        toastMessageUI.SetActive(false);
    }
    public void OnRecipeSlotClicked(GameObject clickedSlot)
    {
        if (currentClickImage != null)
        {
            // ������ Ŭ���� ������ clickImage�� ����
            Destroy(currentClickImage.gameObject);
        }

        // Ŭ���� ������ clickImage ����
        GameObject clickImageObject = new GameObject("ClickImage");
        currentClickImage = clickImageObject.AddComponent<Image>();
        currentClickImage.sprite = clickImage;

        // Ŭ�� �̹��� ũ�� ����
        RectTransform rectTransform = currentClickImage.rectTransform;
        rectTransform.sizeDelta = imageSize;

        currentClickImage.transform.SetParent(clickedSlot.transform, false);

        // Ŭ�� �̹����� Ŭ���� ������ ���� ���߿� �׷������� ����
        currentClickImage.transform.SetAsFirstSibling();

        // Ŭ���� ������ ������ �����͸� �����ͼ� UI�� ������Ʈ
        RecipeSlot recipeSlot = clickedSlot.GetComponentInChildren<RecipeSlot>();
        if (recipeSlot != null && recipeSlot.recipeData != null)
        {
            ShowRecipeInfo(recipeSlot.recipeData);
        }
    }

    public void ShowRecipeInfo(RecipeData recipeData)
    {
        // ������ �̹��� ������Ʈ
        recipeImage.sprite = recipeData.recipeImage;

        // ������ �̸� ������Ʈ
        recipeNameText.text = recipeData.recipeName;

        // ������ ���� ������Ʈ
        recipeDescriptionText.text = recipeData.recipeDescription;

        // ������ ��� ���� ������Ʈ
        for (int i = 0; i < 3; i++)
        {
            // i�� �ش��ϴ� UI ��� �������� (��: ��� �̹���, �̸�, �ʿ� ����)
            Image ingredientImage = ingredientImages[i];
            TextMeshProUGUI ingredientNameText = ingredientNameTexts[i];
            TextMeshProUGUI ingredientAmountText = ingredientAmountTexts[i];

            if (i < recipeData.ingredients.Length)
            {
                // ������ �����ͷκ��� �ش� ��� ���� ����
                RecipeIngredient ingredient = recipeData.ingredients[i];

                // ��� �̹��� ������Ʈ
                ingredientImage.sprite = ingredient.ingredientIcon;

                // ��� �̸� ������Ʈ
                ingredientNameText.text = ingredient.ingredientName;

                // �ʿ��� ��� ���� ������Ʈ
                int requiredAmount = ingredient.requiredAmount;
               int inventoryAmount = GetInventoryItemCount(ingredient.ingredientName); // �κ��丮���� �ش� ��� ���� ��������
                ingredientAmountText.text = "(" + inventoryAmount + "/0" + requiredAmount + ")";

                // �ش� UI ��� Ȱ��ȭ
                ingredientImage.gameObject.SetActive(true);
                ingredientNameText.gameObject.SetActive(true);
                ingredientAmountText.gameObject.SetActive(true);

                // �ʿ��� ��ᰡ ������ ��Ẹ�� ���� ��� �ؽ�Ʈ ���� ����
                if (inventoryAmount < requiredAmount)
                {
                    ingredientAmountText.color = Color.red;
                }
                else
                {
                    ingredientAmountText.color = Color.green;
                }
            }
            else
            {
                // �ʿ����� ���� ���
                ingredientImage.gameObject.SetActive(false);
                ingredientNameText.gameObject.SetActive(false);
                ingredientAmountText.gameObject.SetActive(false);
            }
        }
    }

    private int GetInventoryItemCount(string itemName)
    {

        // ���Կ��ִ� ������ ���� �ҷ�����
        return 0;
        
    }

    public void OnExitButtonClicked()
    {
        // DIY UI�� ��Ȱ��ȭ
        playerTrigger.DiyUI.SetActive(false);
        diyField.CanDiyUi.SetActive(true);
        playerTrigger.ItemSlot.SetActive(true);
        // ���� Ŭ���� ������ clickImage ����
        if (currentClickImage != null)
        {
            Destroy(currentClickImage.gameObject);
        }
    }


    public void CheckCraftability(RecipeData recipeData)
    {
        // �ʿ��� ���� ������ �������� ���Ͽ� ������ ���� ���� ���� Ȯ��
        canCraft = true;
        for (int i = 0; i < recipeData.ingredients.Length; i++)
        {
            RecipeIngredient ingredient = recipeData.ingredients[i];
            int requiredAmount = ingredient.requiredAmount;

            // �ʿ��� ��� �� ������ �����ۺ��� ������ ���� ������ ������ ���� �Ұ�
            if (tslot.ItemCount < requiredAmount)
            {
                canCraft = false;
                break;
            }
        }

        // DIY ���� ��ư ���� ������Ʈ
        UpdateCraftButtonState();
    }


    public void OnCraftButtonClick(RecipeData recipeData)
    {
        if (canCraft)
        {
            // �ʿ��� ��Ḧ �Ҹ��ϰ� ������ ����
            for (int i = 0; i < recipeData.ingredients.Length; i++)
            {
                RecipeIngredient ingredient = recipeData.ingredients[i];
                int requiredAmount = ingredient.requiredAmount;

                // �ʿ��� ����� ������ ����
                tslot.SetSlotCount(-1);
            }

            // �������� �����ϰ� �κ��丮�� �߰�
            Item craftedItem = CraftItem(recipeData);
            tinventory.AcquireItem(craftedItem, 1);

            // �佺Ʈ �޽��� ���
            ShowToastMessage("������ " + craftedItem.Name + "��(��) �����ߴ�!");

            // DIY UI �ݱ�
            OnExitButtonClicked();
        }
    }
    private void ShowToastMessage(string message)
    {
        toastMessageUI.SetActive(true);
        toastMessage.text = message;
    }

    private void UpdateCraftButtonState()
    {
        if (canCraft)
        {
            // ������ ���� ������ ���
            diyCraftButton.interactable = true;
            craftButtonText.text = "����";
        }
        else
        {
            // ������ ���� �Ұ����� ���
            diyCraftButton.interactable = false;
            craftButtonText.text = "��� ����";
        }
    }

    private Item CraftItem(RecipeData recipeData)
    {
        // ������ ���ۿ� ���� ������ �����Ͽ� ���ο� �������� ����
        // ��: ������ �̸�, ������, �Ӽ� ���� ��

        // �Ʒ��� ���÷� �� �������� �����ϴ� �ڵ��Դϴ�.
        Item craftedItem = new Item();
        craftedItem._name = recipeData.recipeName;
        craftedItem.Icon = recipeData.recipeImage;

        return craftedItem;
    }
}
