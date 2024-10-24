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
    public GameObject p1G, p2G, p1S, p2S;
    public Sprite goodg, badg, okayg;
    public Sprite goods, bads, okays;
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
            GameManager.Instance.p1G = p1G;
            GameManager.Instance.p2G = p2G;
            GameManager.Instance.p1S = p1S;
            GameManager.Instance.p2S = p2S;
            GameManager.Instance.goodg = goodg;
            GameManager.Instance.badg = badg;
            GameManager.Instance.okayg = okayg;
            
            GameManager.Instance.goods = goods;
            GameManager.Instance.bads = bads;
            GameManager.Instance.okays = okays;
            GameManager.Instance.populateEnviroment();

        }
    }

    public void StartGame()
    {
        if (GameManager.Instance.currentDay < 15)
        {
            GameManager.Instance.startGame();
            
        }
        else
        {
            GameManager.Instance.MainMenu();
        }
    }
    
    public void StartNgame()
    {
        GameManager.Instance.startNewGame();
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