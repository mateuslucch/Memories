using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] GameObject inGameMenu;
    [SerializeField] GameObject endGameMenu;
    [SerializeField] GameObject levelChoiceMenu;    
    [SerializeField] GameObject adMessageCanvas;

    private void Awake()
    {
        inGameMenu.SetActive(false);
        endGameMenu.SetActive(false);
        adMessageCanvas.SetActive(false);
        levelChoiceMenu.SetActive(true);
        // screenBlocker.SetActive(false);
    }

    // for menu button
    public void InGameMenu()
    {
        if (inGameMenu.activeSelf)
        {
            if (EndGame.gameFinished)
            {
                levelChoiceMenu.SetActive(true);
            }
            inGameMenu.SetActive(false);
            // screenBlocker.SetActive(false);
            return;
        }
        if (EndGame.gameFinished)
        {
            levelChoiceMenu.SetActive(false);
        }
        inGameMenu.SetActive(true);
        // screenBlocker.SetActive(true);
    }

    // for gamesession things, no buttons
    public void InGameMenu(bool onOff) { inGameMenu.SetActive(onOff); }

    public void EndGameMenu(bool onOff) { endGameMenu.SetActive(onOff); }

    public void LevelChoiceMenu(bool onOff) { levelChoiceMenu.SetActive(onOff); }

    public void AdMessage(bool onOff) { adMessageCanvas.SetActive(onOff); }

}
