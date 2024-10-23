using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
   public TMP_Text timer;
   public TMP_Text p1Stats, p2Stats;
   public Canvas canvas;

   private void Start()
   {
      if (timer != null)
      {
         GameManager.Instance.timerText = timer;
      }

      if (canvas != null)
      {
         GameManager.Instance.p1statT = p1Stats;
         GameManager.Instance.p2statT = p2Stats;
         GameManager.Instance.endRoundCanvas = canvas;
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
