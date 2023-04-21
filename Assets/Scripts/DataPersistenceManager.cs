using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] string fileName = "";

    FileDataHandler dataHandler;
    GameData gameData;

    List<IDataPersistence> dataPersistenceObjects;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake() 
    {

        #region 
        // int dataPersistenceManagerCount = FindObjectsOfType<DataPersistenceManager>().Length;

        // if(dataPersistenceManagerCount > 1)
        // {
        //     gameObject.SetActive(false);
        //     Destroy(gameObject);
        // }
        // else
        // {
        //     instance = this;
        //     DontDestroyOnLoad(this);
        // }
        #endregion

        if(instance != null)
        {
            Debug.LogError("ALREADY HAS DATA PERSISTENCE MANAGER");
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    /// <summary>
    /// Finds all scripts that implement the IDataPersistence interface. 
    /// In other words it finds all scripts that need to save and load game data from
    /// game file.
    /// </summary>
    /// <returns></returns>
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistences = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistences);
    }


    public void NewGame()
    {
        this.gameData = new GameData();
    }

    /// <summary>
    /// Load game data from the game file. If no game file then create new game data object
    /// with defaults
    /// </summary>
    public void LoadGame()
    {
        this.gameData = dataHandler.LoadData();

        if(this.gameData == null)
        {
            Debug.Log("No game data found. Intializing to defaults");
            NewGame();
        }

        foreach(IDataPersistence dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.LoadData(gameData);
        }
    }

    /// <summary>
    /// Save game data to file
    /// </summary>
    public void SaveGame()
    {
        foreach(IDataPersistence dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(gameData);
        }

        dataHandler.SaveData(gameData);

        //Debug.Log($"Saved snake type is {this.gameData.snakeType}");
    }

    private void OnApplicationQuit() 
    {
        SaveGame();
    }

    private void OnDestroy() 
    {
        SaveGame();
    }
}
