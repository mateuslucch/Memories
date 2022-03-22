using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] GameObject inGameMenu;
    [SerializeField] GameObject endGameMenu;
    [SerializeField] GameObject levelChoiceMenu;
    [SerializeField] GameObject adMessageCanvas;

    // block game menu open when winning processes is running (when true)
    bool winningPath = false;

    private void Awake()
    {
        inGameMenu.SetActive(false);
        endGameMenu.SetActive(false);
        adMessageCanvas.SetActive(false);
        levelChoiceMenu.SetActive(true);
    }

    public void WinningPath(bool winningPath)
    {
        this.winningPath = winningPath;
    }

    // for menu button
    public void InGameMenu()
    {
        if (!winningPath)
        {
            if (inGameMenu.activeSelf)
            {
                if (EndGame.gameFinished)
                {
                    levelChoiceMenu.SetActive(true);
                }
                inGameMenu.SetActive(false);
                MenuOpenStatic.menuOpen = false;
                return;
            }
            if (EndGame.gameFinished)
            {
                levelChoiceMenu.SetActive(false);
            }
            inGameMenu.SetActive(true);
            MenuOpenStatic.menuOpen = true;
        }
    }

    // for gamesession things, called on other classes
    public void InGameMenu(bool onOff) { inGameMenu.SetActive(onOff); }

    public void EndGameMenu(bool onOff) { endGameMenu.SetActive(onOff); }

    public void LevelChoiceMenu(bool onOff) { levelChoiceMenu.SetActive(onOff); }

    public void AdMessage(bool onOff) { adMessageCanvas.SetActive(onOff); }

}
