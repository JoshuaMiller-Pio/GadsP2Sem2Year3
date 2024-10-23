using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public bool p1Shop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("test");
        if (other.gameObject.CompareTag("Dish"))
        {
            if (p1Shop)
            {
                GameManager.Instance.p1Cash += other.gameObject.GetComponent<dishes>().foodScript.price;

            }
            else
            {
                GameManager.Instance.p2Cash += other.gameObject.GetComponent<dishes>().foodScript.price;

            }
            Destroy(other.gameObject);
        }
        
    }

    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
