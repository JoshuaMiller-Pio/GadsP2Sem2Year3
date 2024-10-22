using UnityEngine;

[CreateAssetMenu(fileName = "newDishItem", menuName = "ScriptableObjects/dishes")]
public class FoodScriptable : ScriptableObject
{
    public enum Dishes
    {
        //vegan
        mushroom_burgers, 
        salad,
        mapo_tofu,
        //meat
        burger,
        pasta,
        tacos,
        //labgrown
        meat_cube,
        LAB_tacos,
        LAB_Burger,
        
        shit
    }

    public enum FoodType
    {
        Vegan,
        Meat,
        LabGrown,
        SHIT
    }
    
    public IngredientsScriptable.Ingredient ingredient1;
    public IngredientsScriptable.Ingredient ingredient2;
    public IngredientsScriptable.Ingredient ingredient3;

    public float price;
    public FoodType foodType;
    public Dishes dishType;
    
    public Sprite DishSprite;  



    
}
