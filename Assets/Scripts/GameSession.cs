using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Memory
{
    public class GameSession : MonoBehaviour
    {
        GameObject[] selectedPieces;

        [SerializeField] float delayBeforeHide = 1.5f;
        [SerializeField] MenuHandler menuHandler;
        [SerializeField] ScoreCounter scoreHandler;
        [SerializeField] SoundControl soundControl;

        [Header("Audio Files")]
        [SerializeField] AudioClip revealPiece;
        [SerializeField] AudioClip matchSound;
        [SerializeField] AudioClip notMatchSound;

        int index = 0;
        bool mouseClick = true;
        int numberOfPiecesSolved = 0;

        private void Start()
        {
            selectedPieces = new GameObject[2];
        }

        public void AddObjectToArray(GameObject piece)
        {
            if (mouseClick)
            {
                soundControl.PlaySound(revealPiece);
                //AudioSource.PlayClipAtPoint(revealPiece, Camera.main.transform.position);
                piece.GetComponent<Pieces>().RevealImage();
                selectedPieces[index] = piece;
                index += 1;
                if (index > 1) { mouseClick = false; }
            }
        }

        public void HideLevelMenu()
        {
            menuHandler.LevelChoiceMenu(false);
        }

        public void CompareObjects()
        {
            if (index > 1)
            {
                mouseClick = false;

                // pieces dont match
                if (selectedPieces[0].transform.Find("Animal").GetComponent<SpriteRenderer>().sprite.name !=
                selectedPieces[1].transform.Find("Animal").GetComponent<SpriteRenderer>().sprite.name)
                {
                    soundControl.PlaySound(notMatchSound);
                    //AudioSource.PlayClipAtPoint(notMatchSound, Camera.main.transform.position);
                    HideBackImages();
                    scoreHandler.ScoreCount(false);
                }
                // pieces match
                else
                {
                    soundControl.PlaySound(matchSound);
                    //AudioSource.PlayClipAtPoint(matchSound, Camera.main.transform.position);
                    mouseClick = true;
                    index = 0;

                    scoreHandler.ScoreCount(true);
                    CheckEndGame();
                }
            }
        }

        private void HideBackImages()
        {
            mouseClick = true;
            index = 0;
            foreach (GameObject pieces in selectedPieces)
            {
                StartCoroutine(pieces.GetComponent<Pieces>().HideImagesCountdown(0.5f));
            }
        }

        private void CheckEndGame()
        {
            numberOfPiecesSolved++;
            if (numberOfPiecesSolved == FindObjectOfType<DistributingPieces>().NumberOfPieces() / 2)
            {
                VictoryPath();
            }
        }

        private void VictoryPath()
        {
            menuHandler.EndGameMenu(true);
            scoreHandler.FinalScore();
        }

        public void RestartGame()
        {
            // destroy pieces from last game
            Pieces[] pieces = FindObjectsOfType<Pieces>();
            foreach (Pieces piece in pieces)
            {
                Destroy(piece.gameObject);
            }

            // reset things
            numberOfPiecesSolved = 0;
            scoreHandler.ResetScore();
            //FindObjectOfType<DistributingPieces>().RestartGame(numberLines, numberRows);
            index = 0;

            menuHandler.EndGameMenu(false);
            menuHandler.InGameMenu(false);
            menuHandler.LevelChoiceMenu(true);
        }
    }
}
