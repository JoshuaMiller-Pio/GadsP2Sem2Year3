using UnityEngine;

public class PlateP2 : MonoBehaviour
{
    public bool isPlayer1 = true;  // Determine which player is interacting with the plate
    public bool isheld = false;

    // Ingredients added to the plate (in exact order)
    private IngredientsScriptable.Ingredient ingredient1;
    private IngredientsScriptable.Ingredient ingredient2;
    private IngredientsScriptable.Ingredient ingredient3;
    public FoodScriptable food;
    private int ingredientCount = 0; // Track how many ingredients have been added

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ingredient"))
        {
            // Get the ingredient from the GameObject
            IngredientScript script = other.gameObject.GetComponent<IngredientScript>();
            IngredientsScriptable ingredientScriptable = script.ingredientScript;

            if (ingredientCount < 3 && ingredientScriptable != null)
            {
                AddIngredient(ingredientScriptable.ingredient);
            }
            Destroy(other.gameObject);
        }
    }

    // Method to add ingredients to the plate
    private void AddIngredient(IngredientsScriptable.Ingredient ingredient)
    {
        ingredientCount++;

        // Add ingredients in order
        if (ingredientCount == 1)
        {
            ingredient1 = ingredient;
            Debug.Log("Added Ingredient 1: " + ingredient1);
        }
        else if (ingredientCount == 2)
        {
            ingredient2 = ingredient;
            Debug.Log("Added Ingredient 2: " + ingredient2);
        }
        else if (ingredientCount == 3)
        {
            ingredient3 = ingredient;
            Debug.Log("Added Ingredient 3: " + ingredient3);

            // Once 3 ingredients are added, check for a dish
            CreateDish();
        }
    }

    // Method to create a dish based on the ingredients and the player's menu
    private void CreateDish()
    {
        // Default to "shit" dish
        FoodScriptable.Dishes createdDish = FoodScriptable.Dishes.shit;

        // Get the correct menu based on the player
        var menu = GameManager.Instance.P2Menu;
        
        // Iterate through the available menu for the respective player and check if the ingredients match
        foreach (FoodScriptable dish in menu)
        {
            if (dish.ingredient1 == ingredient1 && dish.ingredient2 == ingredient2 && dish.ingredient3 == ingredient3)
            {
                createdDish = dish.dishType;
                Debug.Log("Dish Created: " + createdDish);
                break;
            }
        }

        if (createdDish == FoodScriptable.Dishes.shit)
        {
            Debug.Log("Dish Created: Shit");
        }

        // Reset the plate for new ingredients after creating the dish
        ResetPlate();
    }

    // Method to reset the plate
    private void ResetPlate()
    {
        ingredientCount = 0;
        ingredient1 = default;
        ingredient2 = default;
        ingredient3 = default;
    }
}
