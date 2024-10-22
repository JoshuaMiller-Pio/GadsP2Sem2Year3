using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<FoodScriptable> P1Menu = new List<FoodScriptable>() ;
    public List<FoodScriptable> P2Menu = new List<FoodScriptable>() ;
    public Stats P1Stats, P2Stats;
    public float p1Cash, p2Cash;
    public Sector currentSector;
    public float totaldays = 15 ,currentDay;
    public enum Sector
    {
        healthy,
        unhealthy,
        balanced
    }
    // Start is called before the first frame update
    void Start()
    {
        currentDay = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextDay()
    {
        if (currentDay < totaldays)
        {
            currentDay++;
            checkSector();
            startDay();
        }
        else
        {
            endScreen();
        }
        
    }

    public void decideBonuses()
    {
        //TODO look at food items and apply bouses
    }

    public void startDay()
    {
        //TODO add start up UI, menu selection gold reset, stats etc
    }
    public void endScreen()
    {
    }
    public void checkSector()
    {
        switch (currentDay)
        {
            case 6:
                currentSector = Sector.balanced;
                break;
            case 11: 
                currentSector = Sector.healthy;

                break;
            default:
                currentSector = Sector.unhealthy;
                break;
        }
    }
    
    
    
    
}


public class Menu
{
    public List<FoodScriptable> dishes = new List<FoodScriptable>();
}

public class Stats
{
    public int healthy, unhealthy, balanced;
}