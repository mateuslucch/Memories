using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    GameObject[] selectedPieces;

    [SerializeField] float hideCountdown = 1.5f;
    [SerializeField] MenuHandler menuHandler;
    [SerializeField] ScoreCounter scoreHandler;
    [SerializeField] SoundControl soundControl;

    [Header("Audio Files")]
    [SerializeField] AudioClip revealPiece;
    [SerializeField] AudioClip matchSound;
    [SerializeField] AudioClip notMatchSound;
    [SerializeField] AudioClip winSound;

    MusicPlayer musicPlayer;
    AdTrigger adTrigger;

    int index = 0;
    bool mouseClick = true;
    int numberOfMatchs = 0;

    private void Start()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
        EndGame.gameFinished = true;
        selectedPieces = new GameObject[2];
        adTrigger = FindObjectOfType<AdTrigger>();
    }   

    public void AddObjectToArray(GameObject piece)
    {
        adTrigger.CountClicks();
        if (mouseClick)
        {
            
            soundControl.PlayPieceSound(revealPiece);
            //AudioSource.PlayClipAtPoint(revealPiece, Camera.main.transform.position);
            piece.GetComponent<Pieces>().RevealImage();
            piece.GetComponent<Pieces>().RemoveQuestionMark();
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
                soundControl.PlayPieceSound(notMatchSound);
                HideBackImages();
                scoreHandler.ScoreCount(false);                
            }

            // pieces match
            else
            {
                mouseClick = true;
                soundControl.PlayPieceSound(matchSound);
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
            StartCoroutine(pieces.GetComponent<Pieces>().HideImagesCountdown(hideCountdown));
        }
    }

    private void CheckEndGame()
    {
        numberOfMatchs++;
        if (numberOfMatchs == FindObjectOfType<DistributingPieces>().NumberPieces() / 2)
        {
            //VictoryPath();            
            musicPlayer.EndGameMute(true);
            menuHandler.WinningPath(true);
            StartCoroutine(PauseBeforeWin());
            soundControl.PlayPieceSound(winSound);
        }
    }

    IEnumerator PauseBeforeWin()
    {
        yield return new WaitForSecondsRealtime(winSound.length);
        musicPlayer.EndGameMute(false);
        StartAd();
    }

    private void StartAd()
    {
        adTrigger.StartAd();
    }

    // VictoryPath is now called from AdTrigger
    public void VictoryPath()
    {
        EndGame.gameFinished = true;
        menuHandler.EndGameMenu(true);
        scoreHandler.FinalScore();
        menuHandler.WinningPath(false);
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
        numberOfMatchs = 0;
        scoreHandler.ResetScore();
        index = 0;

        menuHandler.EndGameMenu(false);
        menuHandler.InGameMenu(false);
        menuHandler.LevelChoiceMenu(true);
    }
}