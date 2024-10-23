using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientScript : MonoBehaviour
{
    // Start is called before the first frame update
    public IngredientsScriptable ingredientScript;
    public bool isheld ,destroyed = false;
    public Spawner spawner;

    void Start()
    {
       // GetComponent<SpriteRenderer>().sprite = ingredientScript.foodSprite;
    }

    private void FixedUpdate()
    {
if (destroyed == true) GameObject.Destroy(this.gameObject);   }

    private void OnDestroy()
    {
        spawner.removeFromlist();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
