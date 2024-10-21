using UnityEngine;

[CreateAssetMenu(fileName = "newIngredientItem", menuName = "ScriptableObjects/Ingredients")]
public class IngredientsScriptable : ScriptableObject
{
    // Enum to classify the type of food
    public enum FoodType
    {
        LabGrown,
        Vegan,
        Meat
    }
    public enum Ingredient
    {
        Carrot,
        beans,
        patato,
        flour,
        eggPlant,
        mushroom,
        eggs,
        tofu,
        pasta,
        tortilla,
        meat,
        lab_meat
    }
    
  
    public FoodType foodCategory;  
    public Ingredient ingredient;
    public Sprite foodSprite;  



    
}
//mushroom burgers 
//salad
//mapo tofu
    
    
//burger
//pasta
//tacos
    
//meat cube
//LAB_tacos
//LAB_Burger