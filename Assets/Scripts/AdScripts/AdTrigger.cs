using System;
using System.Collections;
using System.Collections.Generic;
using Memory;
using UnityEngine;

public class AdTrigger : MonoBehaviour
{
    [SerializeField] UiTextHandler adTextMessage;
    [SerializeField] MenuHandler adBoxMessage;
    [SerializeField] int clicksToTrigger = 70;
    [SerializeField] float waitTime = 5f;

    public void StartAd()
    {
        // turn on ad wehn true
        if (ClicksCount.clickCounter > clicksToTrigger)
        {
            // future: show the countdown
            adBoxMessage.AdMessage(true);
            StartCoroutine(CountDownBeforeAd());
            ClicksCount.clickCounter = 0;
        }
        // keep going if not
        else
        {
            ContinueGame();
        }
    }

    IEnumerator CountDownBeforeAd()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        adBoxMessage.AdMessage(false);
        FindObjectOfType<AdManager>().ShowAd(this);
    }

    public void ContinueGame()
    {
        FindObjectOfType<GameSession>().VictoryPath();
    }

    public void CountClicks()
    {
        ClicksCount.clickCounter++;
    }
}
