using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Memory
{
    [RequireComponent(typeof(SaveSystem))]
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] UiTextHandler uiTextHandler;

        [Header("Score points")]
        [SerializeField] int scoreUp = 2;
        [SerializeField] int scoreDown = -1;
        string gridId;

        SaveSystem loadSaveHandler;
        List<RecordData> listRecordData;
        RecordData tempRecordData;

        int score = 0;

        private void Start()
        {
            loadSaveHandler = GetComponent<SaveSystem>();
            tempRecordData = new RecordData();
            listRecordData = new List<RecordData>();

            // create new save file score if there is no save file
            if (loadSaveHandler.LoadScoreList() == null)
            {
                CreateNewSaveFile();
                return;
            }

            UpdateScoreText();
            // topScoreBox.text = "Top Score: \n" + topScore.ToString();
        }

        // load when click grid size button
        public void ScoreId(string gridId)
        {
            this.gridId = gridId;
            LoadScore();
        }

        public void ScoreCount(bool scoredUp)
        {
            if (scoredUp)
            {
                score += scoreUp;
            }
            else { score -= scoreDown; }

            UpdateScoreText();
        }

        public void ResetScore()
        {
            score = 0;
            UpdateScoreText();
            LoadScore();
        }

        private void UpdateScoreText()
        {
            uiTextHandler.ChangeScore(score);
        }

        // load when all pieces are revealed
        public void FinalScore()
        {
            LoadScore();
            if (score > tempRecordData.topScoreData)
            {
                tempRecordData.topScoreData = score;
                SaveScore();
                uiTextHandler.EndGameText("It´s a record!");
            }
            else { uiTextHandler.EndGameText("You did it!"); }
            uiTextHandler.EndGameScore(tempRecordData.topScoreData, score);
        }

        public int ReturnScore()
        {
            return score;
        }

        private void SaveScore()
        {
            // load list
            listRecordData = loadSaveHandler.LoadScoreList();

            // check if gridId exist and update score
            for (int i = 0; i < listRecordData.Count; i++)
            {
                // store topScore from selected grid if found
                if (listRecordData[i].gridId == gridId)
                {
                    listRecordData[i].topScoreData = tempRecordData.topScoreData;
                    loadSaveHandler.SaveScoreList(listRecordData);
                    return;
                }
            }

            // add new data to list if there is no record from selected grid
            listRecordData.Add(tempRecordData);
            loadSaveHandler.SaveScoreList(listRecordData);
        }

        private void LoadScore()
        {
            tempRecordData.gridId = gridId;

            listRecordData = loadSaveHandler.LoadScoreList();

            // load the top score of gridId
            foreach (RecordData recordData in listRecordData)
            {
                if (recordData.gridId == tempRecordData.gridId)
                {
                    tempRecordData.topScoreData = recordData.topScoreData;
                    return;
                }
                tempRecordData.topScoreData = 0;
            }
        }

        private void CreateNewSaveFile()
        {
            tempRecordData.gridId = gridId;
            tempRecordData.topScoreData = 0;
            listRecordData.Add(tempRecordData);
            loadSaveHandler.SaveScoreList(listRecordData);
            return;
        }
    }
}