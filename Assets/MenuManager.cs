using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public FoodScriptable[] Mdishes;

    public TMP_Text[] P1Menu;
    public TMP_Text[] P2Menu;
    public Canvas p1Canvas;
    public Canvas p2Canvas;
    public FoodScriptable[] p1SelectedM;
    public FoodScriptable[] p2SelectedM;

    public bool p1Selected = false;
    public bool p2Selected = false;

    private void Start()
    {
        GameManager.Instance.gameObject.name = "GameManager";
    }

    public void p1AddItem(FoodScriptable mdish)
    {
        if (GameManager.Instance.P1Menu.Count < 4)
        {
            GameManager.Instance.P1Menu.Add(mdish);

        }
    }

    public void p2AddItem(FoodScriptable mdish)
    {
       
            if (GameManager.Instance.P1Menu.Count < 4)
            {
                GameManager.Instance.P1Menu.Add(mdish);
            }
            else
            {
                GameManager.Instance.P2Menu.Add(mdish);
        
            }
    }

    private void Update()
    {
        if (GameManager.Instance.P1Menu.Count == 4 && !p1Selected)
        {
            p1Selected = true;
            Debug.Log(GameManager.Instance.P1Menu.Count);
            ticker = 0;
            p1Canvas.gameObject.SetActive(false);
            p2Canvas.gameObject.SetActive(true);
        }
        else if(GameManager.Instance.P2Menu.Count == 4 && p1Selected && !p2Selected)
        {
            p2Selected = true;
            p2Canvas.gameObject.SetActive(false);
            GameManager.Instance.startDay();

        }
    }

    private int ticker = 0;

    public void updateUI(String dishName)
    {
        if (!p1Selected)
        {
            P1Menu[ticker].text = dishName;

        }
        else
        {
            P2Menu[ticker].text = dishName;
        }
        ticker++;
    }



    public void addmushroom_burgers()
    {
        if (!p1Selected)
        {
            p1AddItem(Mdishes[7]);
            updateUI("V burgers");
            GameManager.Instance.P1Stats.Vegan++;
        }
        else
        {
            p2AddItem(Mdishes[7]);
            updateUI("V burgers");
            GameManager.Instance.P2Stats.Vegan++;

        }
    }

    public void addsalad()
    {
        if (!p1Selected)
        {
            p1AddItem(Mdishes[8]);
            updateUI("salad");
            GameManager.Instance.P1Stats.Vegan++;

        }
        else
        {
            p2AddItem(Mdishes[8]);
            updateUI("salad");
            GameManager.Instance.P2Stats.Vegan++;

        }
    }

    public void addmapo_tofu()
    {
        if (!p1Selected)
        {
            p1AddItem(Mdishes[6]);
            updateUI("mapo tofu");
            GameManager.Instance.P1Stats.Vegan++;

        }
        else
        {
            p2AddItem(Mdishes[6]);
            updateUI("mapo tofu");
            GameManager.Instance.P2Stats.Vegan++;

        }
    }

    public void addburger()
    {
        if (!p1Selected)
        {
            p1AddItem(Mdishes[0]);
            updateUI("burger");
            GameManager.Instance.P1Stats.Meat++;

        }
        else
        {
            p2AddItem(Mdishes[0]);
            updateUI("burger");
            GameManager.Instance.P2Stats.Meat++;

        }
    }

    public void addpasta()
    {
        if (!p1Selected)
        {
            p1AddItem(Mdishes[1]);
            updateUI("pasta");
            GameManager.Instance.P1Stats.Meat++;

        }
        else
        {
            p2AddItem(Mdishes[1]);
            updateUI("pasta");
            GameManager.Instance.P2Stats.Meat++;

        }
    }

    public void addtacos()
    {
        if (!p1Selected)
        {
            p1AddItem(Mdishes[2]);
            updateUI("tacos");
            GameManager.Instance.P1Stats.Meat++;

        }
        else
        {
            p2AddItem(Mdishes[2]);
            updateUI("tacos");
            GameManager.Instance.P2Stats.Meat++;

        }
    }

    public void addmeat_cube()
    {
        if (!p1Selected)
        {
            p1AddItem(Mdishes[4]);
            updateUI("meat cube");
            GameManager.Instance.P1Stats.Lab++;

        }
        else
        {
            p2AddItem(Mdishes[4]);
            updateUI("meat cube");
            GameManager.Instance.P2Stats.Lab++;

        }
    }

    public void addLAB_tacos()
    {
        if (!p1Selected)
        {
            p1AddItem(Mdishes[5]);
            updateUI("LAB tacos");
            GameManager.Instance.P1Stats.Lab++;

        }
        else
        {
            p2AddItem(Mdishes[5]);
            updateUI("LAB tacos");
            GameManager.Instance.P2Stats.Lab++;

        }
    }

    public void addLAB_Burger()
    {
        if (!p1Selected)
        {
            p1AddItem(Mdishes[3]);
            updateUI("LAB Burger");
            GameManager.Instance.P1Stats.Lab++;

        }
        else
        {
            p2AddItem(Mdishes[3]);
            updateUI("LAB Burger");
            GameManager.Instance.P2Stats.Lab++;

        }
    }
}

