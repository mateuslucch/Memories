using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Memory
{
    public class GameSession : MonoBehaviour
    {
        GameObject[] selectedPieces;
        [SerializeField] int scoreUp = 2;
        [SerializeField] int scoreDown = -1;
        [SerializeField] float delayBeforeHide = 1.5f;
        [SerializeField] TextMeshProUGUI messageBox;
        [SerializeField] GameObject inGameMenu;
        [SerializeField] GameObject chooseSizeMenu;

        [Header("Audio Configs")]
        [SerializeField] AudioClip revealPiece;
        [SerializeField] AudioClip matchSound;
        [SerializeField] AudioClip notMatchSound;

        int index = 0;
        bool mouseClick = true;
        int numberOfPiecesSolved = 0;

        private void Start()
        {
            inGameMenu.SetActive(false);
            selectedPieces = new GameObject[2];
            messageBox.text = "";
        }

        public void AddObjectToArray(GameObject piece)
        {
            if (mouseClick)
            {
                AudioSource.PlayClipAtPoint(revealPiece, Camera.main.transform.position);
                //print(piece.transform.Find("Animal").GetComponent<SpriteRenderer>().sprite.name);
                piece.GetComponent<Pieces>().RevealImage();
                selectedPieces[index] = piece;
                index += 1;
                if (index > 1) { mouseClick = false; }
            }
        }

        public void HideLevelMenu()
        {
            chooseSizeMenu.SetActive(false);
        }

        public void CompareObjects()
        {
            if (index > 1)
            {
                mouseClick = false;
                if (selectedPieces[0].transform.Find("Animal").GetComponent<SpriteRenderer>().sprite.name !=
                selectedPieces[1].transform.Find("Animal").GetComponent<SpriteRenderer>().sprite.name)
                {
                    AudioSource.PlayClipAtPoint(notMatchSound, Camera.main.transform.position);
                    HideBackImages();
                    FindObjectOfType<ScoreCounter>().ScoreCount(scoreDown);
                }
                else
                {
                    AudioSource.PlayClipAtPoint(matchSound, Camera.main.transform.position);
                    mouseClick = true;
                    index = 0;
                    FindObjectOfType<ScoreCounter>().ScoreCount(scoreUp);
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
            inGameMenu.SetActive(true);
            FindObjectOfType<ScoreCounter>().TopScore();
            messageBox.text = "You did it!!\nTotal Score:\n" + FindObjectOfType<ScoreCounter>().ReturnScore().ToString();
        }

        public void RestartGame()
        {
            Pieces[] pieces = FindObjectsOfType<Pieces>();
            foreach (Pieces piece in pieces)
            {
                Destroy(piece.gameObject);
            }
            numberOfPiecesSolved = 0;
            FindObjectOfType<ScoreCounter>().ResetScore();
            //FindObjectOfType<DistributingPieces>().RestartGame(numberLines, numberRows);
            index = 0;
            inGameMenu.SetActive(false);
            chooseSizeMenu.SetActive(true);
        }

    }

}
