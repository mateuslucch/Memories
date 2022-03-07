using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiTextHandler : MonoBehaviour
{
    [Header("Score text boxes")]
    [SerializeField] TextMeshProUGUI scoreBox;
    [SerializeField] TextMeshProUGUI scoreBoxEnd;
    [SerializeField] TextMeshProUGUI topScoreBox;

    [SerializeField] TextMeshProUGUI timeCounter;

    public void ChangeScore(int score)
    {
        scoreBox.text = $"Score:\n {score.ToString()}";
    }

    public void EndGameScore(int topScore, int finalScore)
    {
        scoreBoxEnd.text = $"Final Score:\n {finalScore.ToString()}";
        topScoreBox.text = $"Top Score:\n {topScore.ToString()}";
    }
}
