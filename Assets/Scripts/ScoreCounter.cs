using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Memory
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] UiTextHandler uiTextHandler;

        [Header("Score points")]
        [SerializeField] int scoreUp = 2;
        [SerializeField] int scoreDown = -1;

        int topScore = 0;
        int score = 0;

        private void Start()
        {
            UpdateScore();
            // topScoreBox.text = "Top Score: \n" + topScore.ToString();
        }

        public void ScoreCount(bool condition)
        {
            if (condition)
            {
                score += scoreUp;
            }
            else { score -= scoreDown; }

            UpdateScore();
        }

        public void ResetScore()
        {
            score = 0;
            UpdateScore();
        }

        private void UpdateScore()
        {
            uiTextHandler.ChangeScore(score);
        }

        public void FinalScore()
        {
            if (score > topScore) { topScore = score; }
            uiTextHandler.EndGameScore(topScore, score);
        }

        public int ReturnScore()
        {
            return score;
        }
    }
}