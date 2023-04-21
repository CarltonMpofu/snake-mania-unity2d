using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class FileDataHandler
{
    private string dataDirPath = "";

    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    /// <summary>
    /// Load game data from game file stored in Json 
    /// </summary>
    /// <returns>GameData</returns>
    public GameData LoadData()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedGameData = null;

        if(File.Exists(fullPath))
        {
            try
            {
                string dataToReturn = "";

                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToReturn = reader.ReadToEnd();
                    }
                }

                // deserialize data
                loadedGameData = JsonUtility.FromJson<GameData>(dataToReturn);
            }
            catch(Exception e)
            {
                Debug.LogError("Error when trying to load data from " + fullPath + "\n" + e);
            }
        }

        return loadedGameData;
    }

    /// <summary>
    /// Save the game data to the game file in Json format
    /// </summary>
    /// <param name="gameData"></param>
    public void SaveData(GameData gameData)
    {

        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // Serialize data
            string dataToStore = JsonUtility.ToJson(gameData, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Error when trying to save data to " + fullPath + "\n" + e);
        }
    }
}
