using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// 레시피창 아이템 슬롯 클릭하면 아이템 이름, 설명이 떠야하고 어떤 재료가 필요한지 떠야함
// 레시피 종류별로 값을 다 나눠서 상태 저장하고 클릭할때 뜨게하기

public class DIYUIController : SystemProPerty
{
    public RecipeData currentRecipeData;

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

    private bool canCraft = false; // 아이템 제작 가능 여부
    public Button diyCraftButton; // DIY 제작 버튼
    public Text craftButtonText; // DIY 제작 버튼 텍스트

    public GameObject craftMessageUI;
    public Text craftMessage;

    private void Awake()
    {
        craftMessageUI.SetActive(false);
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
            // currentRecipeData 변수에 레시피 데이터 저장
            currentRecipeData = recipeSlot.recipeData;
            ShowRecipeInfo(currentRecipeData);

            // 클릭한 레시피에 대한 제작 가능 여부를 확인
            CheckCraftability(currentRecipeData);
        }
    }

    public void CheckCraftability(RecipeData recipeData)
    {
        // 필요한 재료와 보유한 아이템을 비교하여 아이템 제작 가능 여부 확인
        canCraft = true;
        if (tinventory != null)
        {
            for (int i = 0; i < recipeData.ingredients.Length; i++)
            {
                RecipeIngredient ingredient = recipeData.ingredients[i];
                int requiredAmount = ingredient.requiredAmount;

                // 필요한 재료 중 보유한 아이템보다 부족한 것이 있으면 아이템 제작 불가
                if (GetInventoryItemCount(ingredient.ingredientName) < requiredAmount)
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

    public void OnCraftButtonClick()
    {
        if (canCraft && currentRecipeData != null)
        {
            // 필요한 재료를 소모하고 아이템 제작
            for (int i = 0; i < currentRecipeData.ingredients.Length; i++)
            {
                RecipeIngredient ingredient = currentRecipeData.ingredients[i];
                int requiredAmount = ingredient.requiredAmount;

                // 해당 재료의 이름
                string itemName = ingredient.ingredientName;

                // 필요한 재료의 갯수를 현재 인벤토리에서 차감
                int inventoryItemCount = GetInventoryItemCount(itemName);
                if (inventoryItemCount >= requiredAmount)
                {
                    // 필요한 만큼 재료를 차감
                    UseInventoryItem(itemName, requiredAmount);
                }
                else
                {
                    Debug.Log("필요한 재료가 충분하지 않습니다.");
                    return; // 현재 아이템을 제작할 수 없으므로 종료
                }
            }

            // 아이템을 제작하고 인벤토리에 추가
            Item craftedItem = CraftItem(currentRecipeData);
            tinventory.AcquireItem(craftedItem, 1);

            OnExitButtonClicked();
            // 토스트 메시지 출력
            ShowToastMessage("아이템 " + craftedItem.Name + "을(를) 제작했다!");
            craftMessageUI.SetActive(false); // 이부분 수정해야함 안되는중
        }
        else
        {
            Debug.Log("아이템을 제작할 수 없습니다.");
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
                string ingredientAmountTextValue = "(" + GetInventoryItemCount(ingredient.ingredientName) + "/" + ingredient.requiredAmount + ")";
                ingredientAmountText.text = ingredientAmountTextValue;

                // 해당 UI 요소 활성화
                ingredientImage.gameObject.SetActive(true);
                ingredientNameText.gameObject.SetActive(true);
                ingredientAmountText.gameObject.SetActive(true);

                // 필요한 재료가 보유한 재료보다 많을 경우 텍스트 색상 변경
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
                // 필요하지 않은 경우
                ingredientImage.gameObject.SetActive(false);
                ingredientNameText.gameObject.SetActive(false);
                ingredientAmountText.gameObject.SetActive(false);
            }
        }
    }

    private int GetInventoryItemCount(string itemName)
    {
        int itemCount = 0;

        // 인벤토리 슬롯을 순회하며 아이템 이름과 일치하는 아이템 갯수를 찾습니다.
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
        // DIY UI를 비활성화
        playerTrigger.DiyUI.SetActive(false);
        diyField.CanDiyUi.SetActive(true);
        // 현재 클릭한 슬롯의 clickImage 제거
        if (currentClickImage != null)
        {
            Destroy(currentClickImage.gameObject);
        }
    }
    // 인벤토리에서 아이템을 감소시키는 함수
    private void UseInventoryItem(string itemName, int amount)
    {
        // 인벤토리 슬롯을 순회하며 아이템 이름과 일치하는 아이템을 찾아 감소시킴
        foreach (TSlot slot in tinventory.Slots)
        {
            if (slot.item != null && slot.item.Name == itemName)
            {
                if (slot.ItemCount >= amount)
                {
                    slot.SetSlotCount(-amount);
                    return;
                }
                else
                {
                    // 현재 슬롯의 아이템 갯수가 필요한 양보다 작으면
                    // 현재 슬롯을 비우고 남은 양을 다음 슬롯에서 감소시킴
                    int remainingAmount = amount - slot.ItemCount;
                    slot.ClearSlot();
                    amount = remainingAmount;
                }
            }
        }
    }

    private Item CraftItem(RecipeData recipeData)
    {
        // 아이템 제작에 대한 로직을 구현하여 새로운 아이템을 생성
        // 예: 아이템 이름, 아이콘, 속성 설정 등

        // ScriptableObject.CreateInstance 함수를 사용하여 Item의 인스턴스 생성
        Item craftedItem = ScriptableObject.CreateInstance<Item>();

        // 생성된 아이템의 속성 설정
        craftedItem._name = recipeData.recipeName;
        craftedItem.Icon = recipeData.recipeImage;

        return craftedItem;
    }

    private void ShowToastMessage(string message)
    {
         craftMessageUI.SetActive(true);
         craftMessage.text = message;
    }
}
