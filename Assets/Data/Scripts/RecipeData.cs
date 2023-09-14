using UnityEngine;

[CreateAssetMenu(fileName = "RecipeData", menuName = "Custom/RecipeData", order = 1)]
public class RecipeData : ScriptableObject
{
    public string recipeName; // 레시피 이름
    [TextArea(3, 6)]
    public string recipeDescription; // 레시피 설명
    public Sprite recipeImage; // 레시피 이미지

    public RecipeIngredient[] ingredients; //재료 목록
}

[System.Serializable]
public class RecipeIngredient
{
    public string ingredientName; // 재료 이름
    public int requiredAmount; // 재료 개수
    public Sprite ingredientIcon; // 재료 이미지
}
