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

    // called from pieces
    public void AddObjectToArray(GameObject piece)
    {
        if (mouseClick)
        {
            adTrigger.CountClicks();
            selectedPieces[index] = piece;
            index += 1;
            if (index > 1) { mouseClick = false; }
            soundControl.PlayPieceSound(revealPiece);
            piece.GetComponent<Pieces>().RevealImage();
            piece.GetComponent<Pieces>().RemoveQuestionMark();
        }
    }

    public void HideLevelMenu()
    {
        menuHandler.LevelChoiceMenu(false);
    }

    // called from pieces, when they end to reveal
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
            // PIECES MATCH
            else
            {
                mouseClick = true;
                soundControl.PlayPieceSound(matchSound);
                index = 0;
                scoreHandler.ScoreCount(true);
                CheckEndGame();
                if (EndGame.gameFinished == false)
                {
                    foreach (GameObject piece in selectedPieces)
                    {
                        piece.GetComponent<Pieces>().StartAnimation();
                        piece.GetComponent<GlowControl>().ChangeColorTrigger();
                    }
                }
            }
        }
    }

    private void HideBackImages()
    {
        foreach (GameObject pieces in selectedPieces)
        {
            StartCoroutine(pieces.GetComponent<Pieces>().HideImagesCountdown(hideCountdown));
        }
        index = 0;
        mouseClick = true;
    }

    private void CheckEndGame()
    {
        numberOfMatchs++;
        if (numberOfMatchs == FindObjectOfType<PieceDistributor>().NumberPieces() / 2)
        {
            Pieces[] pieces = FindObjectsOfType<Pieces>();
            foreach (Pieces piece in pieces)
            {
                piece.StartAnimation();
            }

            musicPlayer.EndGameMute(true);
            menuHandler.WinningPath(true);
            StartCoroutine(PauseBeforeWinPath());
            soundControl.PlayPieceSound(winSound);
        }
    }

    IEnumerator PauseBeforeWinPath()
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

        // manage menus
        menuHandler.EndGameMenu(false);
        menuHandler.InGameMenu(false);
        menuHandler.LevelChoiceMenu(true);        
        EndGame.gameFinished = true;
    }
}