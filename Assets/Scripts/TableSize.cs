using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memory
{
    public class TableSize : MonoBehaviour
    {
        [SerializeField] int numberLines = 4;
        [SerializeField] int numberRows = 4;

        public void TableSizeGame()
        {
            FindObjectOfType<DistributingPieces>().RestartGame(numberLines, numberRows);
            FindObjectOfType<GameSession>().HideLevelMenu();
            FindObjectOfType<SoundControl>().ClickSound();
        }
    }
}