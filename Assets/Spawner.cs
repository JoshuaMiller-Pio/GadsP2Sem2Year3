using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject itemPrefab;  // Prefab to spawn
    private GameObject currentItem;  // Reference to the currently spawned item
    private bool free = false;
    public int numSpawned = 0;
// Start is called before the first frame update
    void Start()
    {
        numSpawned = 0;
        SpawnItem();  // Spawn the first item when the game starts
    }

    // Method to spawn the item at the spawner's position
    void SpawnItem()
    {
        if (numSpawned < 5 )
        {
            if (itemPrefab != null)
            {
                Vector3 spawnPoint = new Vector3(transform.position.x,transform.position.y,0);  
                // Instantiate the item at the spawner's position
                currentItem = Instantiate(itemPrefab, spawnPoint, transform.rotation);
                numSpawned++;
                if (currentItem.GetComponent<PlateP1>() != null)
                {
                    currentItem.GetComponent<PlateP1>().spawner = this;
                }
                else if (currentItem.GetComponent<PlateP2>() != null)
                {
                    currentItem.GetComponent<PlateP1>().spawner = this;

                }
                else
                {
                    currentItem.GetComponent<IngredientScript>().spawner = this;
                }

                free = false;
            }
        }
        
    }

    // This method should be called when the item is picked up
    public void OnItemPickedUp()
    {
        // Spawn a new item after the current one is picked up
        SpawnItem();
    }

    public void removeFromlist()
    {
        numSpawned--;
        if (free)
        {
            SpawnItem();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject == currentItem)
        {
            OnItemPickedUp();
            free = true;

        }    }
}