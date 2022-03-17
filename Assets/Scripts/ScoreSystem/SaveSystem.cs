using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    string filePath;

    private void Awake()
    {
        // not getting path here, for some reason
        filePath = Application.persistentDataPath + "/save.score"; // where file will be saved       
    }

    public void SaveScoreList(List<RecordData> scoreData)
    {
        filePath = Application.persistentDataPath + "/save.score"; // where file will be saved               
        FileStream dataStream = new FileStream(filePath, FileMode.Create);

        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(dataStream, scoreData);

        dataStream.Close();
    }

    public List<RecordData> LoadScoreList()
    {
        filePath = Application.persistentDataPath + "/save.score";
        if (File.Exists(filePath))
        {
            // File exists  
            FileStream dataStream = new FileStream(filePath, FileMode.Open);

            BinaryFormatter converter = new BinaryFormatter();
            List<RecordData> loadedData = converter.Deserialize(dataStream) as List<RecordData>;

            dataStream.Close();
            return loadedData;
        }
        else
        {
            // File does not exist
            Debug.Log($"<color=#00FF3C>Save file not found in {filePath}</color>");
            return null;
        }
    }
}
