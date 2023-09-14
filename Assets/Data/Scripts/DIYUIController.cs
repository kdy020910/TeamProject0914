using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 레시피창 아이템 슬롯 클릭하면 아이템 이름, 설명이 떠야하고 어떤 재료가 필요한지 떠야함
// 레시피 종류별로 값을 다 나눠서 상태 저장하고 클릭할때 뜨게하기

public class DIYUIController : SystemProPerty
{
    private Image currentClickImage; // 현재 클릭한 슬롯의 clickImage
    public Sprite clickImage;
    public Transform[] slotParents; // 슬롯들이 있는 부모 Transform

    public Vector2 imageSize = new Vector2(84f, 84f); // 원하는 이미지 크기

    public Image recipeImage; // 레시피 이미지 표시할 UI Image
    public TextMeshProUGUI recipeNameText; // 레시피 이름을 표시할 Text
    public TextMeshProUGUI recipeDescriptionText; // 레시피 설명을 표시할 Text

    public Image[] ingredientImages; // 재료 이미지 ui
    public TextMeshProUGUI[] ingredientNameTexts; // 재료 이름 ui
    public TextMeshProUGUI[] ingredientAmountTexts; // 재료 갯수 ui 

    public void OnRecipeSlotClicked(GameObject clickedSlot)
    {
        if (currentClickImage != null)
        {
            // 이전에 클릭한 슬롯의 clickImage를 제거
            Destroy(currentClickImage.gameObject);
        }

        // 클릭한 슬롯의 clickImage 생성
        GameObject clickImageObject = new GameObject("ClickImage");
        currentClickImage = clickImageObject.AddComponent<Image>();
        currentClickImage.sprite = clickImage;

        // 클릭 이미지 크기 설정
        RectTransform rectTransform = currentClickImage.rectTransform;
        rectTransform.sizeDelta = imageSize;

        currentClickImage.transform.SetParent(clickedSlot.transform, false);

        // 클릭 이미지를 클릭한 슬롯의 가장 나중에 그려지도록 조절
        currentClickImage.transform.SetAsFirstSibling();

        // 클릭한 슬롯의 레시피 데이터를 가져와서 UI를 업데이트
        RecipeSlot recipeSlot = clickedSlot.GetComponentInChildren<RecipeSlot>();
        if (recipeSlot != null && recipeSlot.recipeData != null)
        {
            ShowRecipeInfo(recipeSlot.recipeData);
        }
    }

    public void ShowRecipeInfo(RecipeData recipeData)
    {
        // 레시피 이미지 업데이트
        recipeImage.sprite = recipeData.recipeImage;

        // 레시피 이름 업데이트
        recipeNameText.text = recipeData.recipeName;

        // 레시피 설명 업데이트
        recipeDescriptionText.text = recipeData.recipeDescription;

        // 레시피 재료 정보 업데이트
        for (int i = 0; i < 3; i++)
        {
            // i에 해당하는 UI 요소 가져오기 (예: 재료 이미지, 이름, 필요 갯수)
            Image ingredientImage = ingredientImages[i];
            TextMeshProUGUI ingredientNameText = ingredientNameTexts[i];
            TextMeshProUGUI ingredientAmountText = ingredientAmountTexts[i];

            if (i < recipeData.ingredients.Length)
            {
                // 레시피 데이터로부터 해당 재료 정보 추출
                RecipeIngredient ingredient = recipeData.ingredients[i];

                // 재료 이미지 업데이트
                ingredientImage.sprite = ingredient.ingredientIcon;

                // 재료 이름 업데이트
                ingredientNameText.text = ingredient.ingredientName;

                // 필요한 재료 갯수 업데이트
                ingredientAmountText.text = "(00/0" + ingredient.requiredAmount + ")";

                // 해당 UI 요소 활성화
                ingredientImage.gameObject.SetActive(true);
                ingredientNameText.gameObject.SetActive(true);
                ingredientAmountText.gameObject.SetActive(true);
            }
            else
            {
                // 필요하지 않은 경우
                ingredientImage.gameObject.SetActive(false);
                ingredientNameText.gameObject.SetActive(false);
                ingredientAmountText.gameObject.SetActive(false); 
            }
        }
    }



    public void OnExitButtonClicked()
    {
        // DIY UI를 비활성화
        playerTrigger.DiyUI.SetActive(false);
        diyField.CanDiyUi.SetActive(true);

        // 현재 클릭한 슬롯의 clickImage 제거
        if (currentClickImage != null)
        {
            Destroy(currentClickImage.gameObject);
        }
    }
}
