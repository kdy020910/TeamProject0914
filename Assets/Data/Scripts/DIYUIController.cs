using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// ������â ������ ���� Ŭ���ϸ� ������ �̸�, ������ �����ϰ� � ��ᰡ �ʿ����� ������
// ������ �������� ���� �� ������ ���� �����ϰ� Ŭ���Ҷ� �߰��ϱ�

public class DIYUIController : SystemProPerty
{
    public UnityEvent<RecipeData> onCraftButtonClick = new UnityEvent<RecipeData>();
    private RecipeData currentRecipeData;

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
    public Text craftButtonText; // DIY ���� ��ư �ؽ�Ʈ

    public GameObject toastMessageUI;
    public Text toastMessage;

    TSlot tslot;
    public void Start()
    {
        tslot = FindObjectOfType<TSlot>(); // TSlot Ŭ������ �ν��Ͻ��� ã�Ƽ� �ʱ�ȭ�մϴ�.

        toastMessageUI.SetActive(false);
    }
    public void OnRecipeSlotClicked(GameObject clickedSlot)
    {
        if (currentClickImage != null)
        {
            Destroy(currentClickImage.gameObject);
        }

        GameObject clickImageObject = new GameObject("ClickImage");
        currentClickImage = clickImageObject.AddComponent<Image>();
        currentClickImage.sprite = clickImage;
        RectTransform rectTransform = currentClickImage.rectTransform;
        rectTransform.sizeDelta = imageSize;
        currentClickImage.transform.SetParent(clickedSlot.transform, false);
        currentClickImage.transform.SetAsFirstSibling();

        RecipeSlot recipeSlot = clickedSlot.GetComponentInChildren<RecipeSlot>();
        if (recipeSlot != null && recipeSlot.recipeData != null)
        {
            // currentRecipeData ������ ������ ������ ����
            currentRecipeData = recipeSlot.recipeData;
            ShowRecipeInfo(currentRecipeData);
            CheckCraftability(currentRecipeData);
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
                string ingredientAmountTextValue = "(" + GetInventoryItemCount(ingredient.ingredientName) + "/" + ingredient.requiredAmount + ")";
                ingredientAmountText.text = ingredientAmountTextValue;

                // �ش� UI ��� Ȱ��ȭ
                ingredientImage.gameObject.SetActive(true);
                ingredientNameText.gameObject.SetActive(true);
                ingredientAmountText.gameObject.SetActive(true);

                // �ʿ��� ��ᰡ ������ ��Ẹ�� ���� ��� �ؽ�Ʈ ���� ����
                if (GetInventoryItemCount(ingredient.ingredientName) < ingredient.requiredAmount)
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
        int itemCount = 0;

        // �κ��丮 ������ ��ȸ�ϸ� ������ �̸��� ��ġ�ϴ� ������ ������ ã���ϴ�.
        foreach (TSlot slot in tinventory.Slots)
        {
            if (slot.item != null && slot.item.Name == itemName)
            {
                itemCount += slot.ItemCount;
            }
        }

        return itemCount;
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
        if (tslot != null)
        {
            for (int i = 0; i < recipeData.ingredients.Length; i++)
            {
                RecipeIngredient ingredient = recipeData.ingredients[i];
                int requiredAmount = ingredient.requiredAmount;

                // �ʿ��� ��� �� ������ �����ۺ��� ������ ���� ������ ������ ���� �Ұ�
                if (tslot.ItemCount < requiredAmount) //t ���� ������ �ȵ�;;
                {
                    canCraft = false;
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning("TSlot is not initialized.");
        }
    }


    public void OnCraftButtonClick(TSlot tslot)
    {
        if (canCraft && currentRecipeData != null)
        {
            // �ʿ��� ��Ḧ �Ҹ��ϰ� ������ ����
            for (int i = 0; i < currentRecipeData.ingredients.Length; i++)
            {
                RecipeIngredient ingredient = currentRecipeData.ingredients[i];
                int requiredAmount = ingredient.requiredAmount;

                // �ش� ����� �̸�
                string itemName = ingredient.ingredientName;

                
                // �ʿ��� ����� ������ ���� �κ��丮���� ����
                int inventoryItemCount = GetInventoryItemCount(itemName);
                if (inventoryItemCount >= requiredAmount)
                {
                    item.quantity--;
                }
                else
                {
                    Debug.Log("�ʿ��� ��ᰡ ������� �ʽ��ϴ�.");
                    return; // ���� �������� ������ �� �����Ƿ� ����
                }
            }

            // �������� �����ϰ� �κ��丮�� �߰�
            Item craftedItem = CraftItem(currentRecipeData);
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
