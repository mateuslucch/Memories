using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Memory
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreBox;
        [SerializeField] TextMeshProUGUI topScoreBox;

        int topScore = 0;
        int score = 0;

        private void Start()
        {
            PrintScore();
            topScoreBox.text = "Top Score: \n" + topScore.ToString();
        }

        public void ScoreCount(int score)
        {
            this.score += score;
            PrintScore();
        }

        public void ResetScore()
        {
            score = 0;
            PrintScore();
        }

        private void PrintScore()
        {
            scoreBox.text = "Score: \n" + score.ToString();
        }

        public void TopScore()
        {
            if (score > 0)
            {
                topScore = score;
                topScoreBox.text = "Top Score: \n" + topScore.ToString();
            }
        }

        public int ReturnScore()
        {
            return score;
        }
    }
}