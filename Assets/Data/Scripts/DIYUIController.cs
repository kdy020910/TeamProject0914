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
                ingredientAmountText.text = "(00/0" + ingredient.requiredAmount + ")";

                // �ش� UI ��� Ȱ��ȭ
                ingredientImage.gameObject.SetActive(true);
                ingredientNameText.gameObject.SetActive(true);
                ingredientAmountText.gameObject.SetActive(true);
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



    public void OnExitButtonClicked()
    {
        // DIY UI�� ��Ȱ��ȭ
        playerTrigger.DiyUI.SetActive(false);
        diyField.CanDiyUi.SetActive(true);

        // ���� Ŭ���� ������ clickImage ����
        if (currentClickImage != null)
        {
            Destroy(currentClickImage.gameObject);
        }
    }
}
