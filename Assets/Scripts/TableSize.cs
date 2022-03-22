using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class TableSize : MonoBehaviour
{
    [SerializeField] int numberLines = 4;
    [SerializeField] int numberRows = 4;
    [SerializeField] TextMeshProUGUI buttonText = null;
    [SerializeField] TextMeshProUGUI buttonScore = null;
    [SerializeField] float timeBeforeHide = 3f;

    // for saving records
    string gridId;
    int score;

    private void Start()
    {

        if (buttonText == null)
        {
            buttonText = gameObject.transform.Find("Table Size Text").GetComponent<TextMeshProUGUI>();
            buttonScore = gameObject.transform.Find("Score Text").GetComponent<TextMeshProUGUI>();
        }

        gridId = $"{numberLines},{numberRows}";
        UpdateText();
    }

    private void OnEnable()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        LoadScore();
        buttonText.text = $"{numberLines}x{numberRows}";
        buttonScore.text = $"Record: {score}";
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Application.isPlaying && Application.isEditor)
        {
            if (buttonText == null)
            {
                buttonText = gameObject.transform.Find("Table Size Text").GetComponent<TextMeshProUGUI>();
                buttonScore = gameObject.transform.Find("Score Text").GetComponent<TextMeshProUGUI>();
            }
            gridId = $"{numberLines},{numberRows}";
            buttonText.text = $"{numberLines}x{numberRows}";
        }
    }
#endif

    // this is where the game start
    public void TableSizeGame()
    {
        EndGame.gameFinished = false;
        MenuOpenStatic.menuOpen = false;
        TimeToHide.timeBeforeHide = timeBeforeHide;

        FindObjectOfType<DistributingPieces>().StartMountBoard(numberLines, numberRows);
        FindObjectOfType<GameSession>().HideLevelMenu();
        if (FindObjectOfType<SoundControl>() != null)
        {
            FindObjectOfType<SoundControl>().ClickSound();
        }
        FindObjectOfType<ScoreCounter>().ScoreId(gridId);
    }

    private void LoadScore()
    {
        SaveSystem loadRecord;
        loadRecord = FindObjectOfType<SaveSystem>();
        List<RecordData> listRecordData;
        listRecordData = loadRecord.LoadScoreList();

        // load the top score of gridId
        foreach (RecordData recordData in listRecordData)
        {
            if (recordData.gridId == gridId)
            {
                score = recordData.topScoreData;
                return;
            }
            score = 0;
        }
    }
}