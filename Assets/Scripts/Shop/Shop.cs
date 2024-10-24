using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public bool p1Shop;  // Determine if it's Player 1's shop

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Dish"))
        {
            // Get the dish type from the Plate's food (ScriptableObject)
            FoodScriptable.Dishes dishType = other.gameObject.GetComponent<Plate>().food.dishType;

            // Add the dish price to the correct player's cash
            if (p1Shop)
            {
                GameManager.Instance.decideBonusesP1(other.gameObject.GetComponent<Plate>().food.price, other.gameObject.GetComponent<Plate>().food.foodType);
                if (other.gameObject.GetComponent<Plate>().food.foodType == FoodScriptable.FoodType.Meat )
                {
                    GameManager.Instance.P1Stats.Meat++;
                }
                else if (other.gameObject.GetComponent<Plate>().food.foodType == FoodScriptable.FoodType.Vegan )
                {
                    
                    GameManager.Instance.P1Stats.Vegan++;
                }
                else
                {
                    GameManager.Instance.P1Stats.Lab++;
                    
                }
                // Directly call the OrderManager to complete Player 1's order
                OrderManager.Instance.CompleteOrderP1(dishType);
            }
            else
            {
                GameManager.Instance.decideBonusesP2(other.gameObject.GetComponent<Plate>().food.price, other.gameObject.GetComponent<Plate>().food.foodType);
                if (other.gameObject.GetComponent<Plate>().food.foodType == FoodScriptable.FoodType.Meat )
                {
                    GameManager.Instance.P2Stats.Meat++;
                }
                else if (other.gameObject.GetComponent<Plate>().food.foodType == FoodScriptable.FoodType.Vegan )
                {
                    
                    GameManager.Instance.P2Stats.Vegan++;
                }
                else
                {
                    GameManager.Instance.P2Stats.Lab++;
                    
                }
                // Directly call the OrderManager to complete Player 2's order
                OrderManager.Instance.CompleteOrderP2(dishType);
            }

            // Destroy the dish GameObject once it's been submitted
            Destroy(other.gameObject);
        }
    }
}