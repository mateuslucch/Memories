using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memory
{
    public class TableSize : MonoBehaviour
    {
        [SerializeField] int numberLines = 4;
        [SerializeField] int numberRows = 4;
        // for saving records
        [SerializeField] string gridId;

        // this is here the game start
        public void TableSizeGame()
        {
            EndGame.gameFinished = false;
            gridId = $"{numberLines},{numberRows}";
            FindObjectOfType<DistributingPieces>().RestartGame(numberLines, numberRows);
            FindObjectOfType<GameSession>().HideLevelMenu();
            FindObjectOfType<SoundControl>().ClickSound();
            FindObjectOfType<ScoreCounter>().ScoreId(gridId);
        }
    }
}