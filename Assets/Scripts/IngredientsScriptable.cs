using UnityEngine;

[CreateAssetMenu(fileName = "newIngredientItem", menuName = "ScriptableObjects/Ingredients")]
public class IngredientsScriptable : ScriptableObject
{
    // Enum to classify the type of food
  
    public enum Ingredient
    {
        Carrot,
        beans,
        patato,
        flour,
        eggPlant,
        wholeeggPlant,
        mushroom,
        wholemushroom,
        eggs,
        tofu,
        pasta,
        tortilla,
        meat,
        lab_meat,
        raw_lMeat,
        Buns,
        Cheese,
        RawMeat,
        
        uncookedPasta
    }
    
  
      
    public Ingredient ingredient;
    public Sprite foodSprite;  
    public Sprite ProcessedfoodSprite;  
    public Ingredient processedIngredient;


    
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