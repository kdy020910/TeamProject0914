/*using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public RecipeCategoryData[] recipeCategories;

    // 다른 스크립트에서 특정 레시피를 가져오는 메서드
    public RecipeData GetRecipe(string categoryName, string recipeName)
    {
        foreach (var category in recipeCategories)
        {
            if (category.categoryName == categoryName)
            {
                foreach (var recipe in category.recipes)
                {
                    if (recipe.recipeName == recipeName)
                    {
                        return recipe;
                    }
                }
            }
        }
        return null; // 해당 레시피를 찾지 못한 경우
    }
}*/
