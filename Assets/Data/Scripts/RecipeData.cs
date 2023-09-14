using UnityEngine;

[CreateAssetMenu(fileName = "RecipeData", menuName = "Custom/RecipeData", order = 1)]
public class RecipeData : ScriptableObject
{
    [Header("������ ������")]
    public string recipeName; // ������ �̸�
    [TextArea(3, 6)]
    public string recipeDescription; // ������ ����
    public Sprite recipeImage; // ������ �̹���

    [Header("�ʿ��� ���")]
    public RecipeIngredient[] ingredients; //��� ���
}

[System.Serializable]
public class RecipeIngredient
{
    public string ingredientName; // ��� �̸�
    public int requiredAmount; // ��� ����
    public Sprite ingredientIcon; // ��� �̹���
}
