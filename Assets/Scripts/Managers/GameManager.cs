using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // Import TextMeshPro namespace

public class GameManager : Singleton<GameManager>
{
    public List<FoodScriptable> P1Menu = new List<FoodScriptable>();
    public List<FoodScriptable> P2Menu = new List<FoodScriptable>();
    public Stats P1Stats = new Stats(), P2Stats = new Stats();
    public float p1Cash = 0, p2Cash = 0;
    public float p1TCash = 0, p2TCash = 0;
    public Sector currentSector;
    public float totaldays = 15, currentDay;
    public Canvas endRoundCanvas;  // Reference to the end-round canvas
    public TMP_Text timerText, p1statT, p2statT,p2Score,p1Score;  // References to TMP text for player stats
    private float countdownTimer = 300f;  // 5 minutes = 300 seconds
    private bool timerRunning = false;

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
        // If the timer is running, update it
        if (timerRunning)
        {
            UpdateTimer();
        }
       
    }

    private void FixedUpdate()
    {
        p1Score.text = "P1 SCORE: " + p1Cash;  
        p2Score.text = "P2 SCORE: " + p2Cash;  
    }

    public void StartTimer()
    {
        countdownTimer = 300;  // Reset to 5 minutes
        timerRunning = true;
    }

    public void UpdateTimer()
    {
        if (countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(countdownTimer / 60);  // Calculate minutes
            int seconds = Mathf.FloorToInt(countdownTimer % 60);  // Calculate seconds

            // Update the TMP text to show the remaining time
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timerRunning = false;
            endScreen();  // Call end screen when time is up
        }
    }

    public void nextDay()
    {
        if (currentDay < totaldays)
        {
            currentDay++;
            SceneManager.LoadScene(1);
            populateEnviroment();
            checkSector();
        }
        else
        {
            endScreen();
        }
    }

    public void populateEnviroment()
    {
        //TODO: update environment
    }
    
    public void decideBonusesP1(float cash, bool deserved)
    {
        if (deserved)
        {
            p1Cash += 1.2f * cash;
        }
    }

    public void decideBonusesP2(float cash, bool deserved)
    {
        if (deserved)
        {
            p2Cash += 1.2f * cash;
        }
    }

    public void startDay()
    {
        p2TCash += p2Cash;  // Update total cash for P2
        p1TCash += p1Cash;  // Update total cash for P1
        p2Cash = 0;
        p1Cash = 0;
        StartTimer();
    }

    public void RestartDay()
    {
        p2TCash = 0;
        p1TCash = 0;
        p2Cash = 0;
        p1Cash = 0;
        startGame();
    }

    public void endScreen()
    {
        // Show the end-round stats when the day ends
        EndRoundStats();
    }

    // Method to show end-round stats and update the canvas
    public void EndRoundStats()
    {
        // Activate the end-round canvas
        endRoundCanvas.gameObject.SetActive(true);

        // Update Player 1's stats
        p1statT.text = "Gold made this round: " + p1Cash.ToString("F2") + "\n" +
                       "Total Gold: " + p1TCash.ToString("F2") + "\n" +
                       "Meat Dishes: " + P1Stats.Meat + "\n" +
                       "Lab Meat Dishes: " + P1Stats.Lab + "\n" +
                       "Vegan Dishes: " + P1Stats.Vegan;

        // Update Player 2's stats
        p2statT.text = "Gold made this round: " + p2Cash.ToString("F2") + "\n" +
                       "Total Gold: " + p2TCash.ToString("F2") + "\n" +
                       "Meat Dishes: " + P2Stats.Meat + "\n" +
                       "Lab Meat Dishes: " + P2Stats.Lab + "\n" +
                       "Vegan Dishes: " + P2Stats.Vegan;
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

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

public class Menu
{
    public List<FoodScriptable> dishes = new List<FoodScriptable>();
}

public class Stats
{
    public int Vegan, Lab, Meat;
}
