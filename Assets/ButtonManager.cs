using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public TMP_Text timer;
    public TMP_Text p1Stats, p2Stats;
    public TMP_Text scorep1, scorep2;
    public Canvas canvas;

    private void Start()
    {
        if (canvas != null)
        {
            GameManager.Instance.timerText = timer;
            GameManager.Instance.p1statT = p1Stats;
            GameManager.Instance.p2statT = p2Stats;
            GameManager.Instance.endRoundCanvas = canvas;
            GameManager.Instance.p1Score = scorep1;
            GameManager.Instance.p2Score = scorep2;
            
        }
    }

    public void StartGame()
    {
        GameManager.Instance.startGame();
    }
    public void Mainmenu()
    {
        GameManager.Instance.MainMenu();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}