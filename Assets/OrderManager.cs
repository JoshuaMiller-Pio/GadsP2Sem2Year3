using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderManager : Singleton<OrderManager>
{
    public TMP_Text[] orderTextP1;  // Text fields for Player 1's orders
    public TMP_Text[] orderTextP2;  // Text fields for Player 2's orders

    private List<FoodScriptable.Dishes> ordersP1 = new List<FoodScriptable.Dishes>();  // Player 1's order list
    private List<FoodScriptable.Dishes> ordersP2 = new List<FoodScriptable.Dishes>();  // Player 2's order list

    private int maxOrders = 4;  // Maximum number of active orders per player
    private float orderInterval = 10f;  // Time interval between adding new orders (in seconds)

    // Event Action for when a dish is submitted to the shop
    public event Action<FoodScriptable.Dishes, bool> OnDishSubmitted;

    void OnEnable()
    {
        // Subscribe to the event
        OnDishSubmitted += HandleDishSubmission;
        GameManager.OnGameStart += GameStart;
    }

    void OnDisable()
    {
        // Unsubscribe from the event
        OnDishSubmitted -= HandleDishSubmission;
        GameManager.OnGameStart -= GameStart;
    }

    // This method will be called when the game starts
    public void GameStart()
    {
        StartCoroutine(AddOrdersOverTime());
    }

    IEnumerator AddOrdersOverTime()
    {
        while (true)
        {
            if (ordersP1.Count < maxOrders)
            {
                AddRandomOrderP1();
            }

            if (ordersP2.Count < maxOrders)
            {
                AddRandomOrderP2();
            }

            yield return new WaitForSeconds(orderInterval);
        }
    }

    public void AddRandomOrderP1()
    {
        if (ordersP1.Count < maxOrders)
        {
            var randomIndex = UnityEngine.Random.Range(0, GameManager.Instance.P1Menu.Count);
            FoodScriptable.Dishes newOrder = GameManager.Instance.P1Menu[randomIndex].dishType;
            ordersP1.Add(newOrder);
            UpdateOrderText();
        }
    }

    public void AddRandomOrderP2()
    {
        if (ordersP2.Count < maxOrders)
        {
            var randomIndex = UnityEngine.Random.Range(0, GameManager.Instance.P2Menu.Count);
            FoodScriptable.Dishes newOrder = GameManager.Instance.P2Menu[randomIndex].dishType;
            ordersP2.Add(newOrder);
            UpdateOrderText();
        }
    }

    void UpdateOrderText()
    {
        for (int i = 0; i < orderTextP1.Length; i++)
        {
            if (i < ordersP1.Count)
            {
                orderTextP1[i].text = ordersP1[i].ToString();
            }
            else
            {
                orderTextP1[i].text = "";
            }
        }

        for (int i = 0; i < orderTextP2.Length; i++)
        {
            if (i < ordersP2.Count)
            {
                orderTextP2[i].text = ordersP2[i].ToString();
            }
            else
            {
                orderTextP2[i].text = "";
            }
        }
    }

    // Handle dish submission event
    public void HandleDishSubmission(FoodScriptable.Dishes dishType, bool isP1)
    {
        if (isP1)
        {
            CompleteOrderP1(dishType);
        }
        else
        {
            CompleteOrderP2(dishType);
        }
    }

    // Complete an order for Player 1
    public void CompleteOrderP1(FoodScriptable.Dishes completedOrder)
    {
        if (ordersP1.Contains(completedOrder))
        {
            Debug.Log("Order for Player 1 Completed: " + completedOrder);
            ordersP1.Remove(completedOrder);  // Remove the first matching order
            UpdateOrderText();
        }
        else
        {
            Debug.LogWarning("Order not found for Player 1: " + completedOrder);
        }
    }

    // Complete an order for Player 2
    public void CompleteOrderP2(FoodScriptable.Dishes completedOrder)
    {
        if (ordersP2.Contains(completedOrder))
        {
            Debug.Log("Order for Player 2 Completed: " + completedOrder);
            ordersP2.Remove(completedOrder);  // Remove the first matching order
            UpdateOrderText();
        }
        else
        {
            Debug.LogWarning("Order not found for Player 2: " + completedOrder);
        }
    }
}
