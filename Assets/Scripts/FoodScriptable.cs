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
        LAB_Burger
    }
    
    public Dishes dishType;
    
    public Sprite DishSprite;  



    
}
