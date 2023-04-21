using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IDataPersistence
{
    bool soundOn = true;
    bool vibrationOn;

    Vector2 dPadLeftPositionOriginal = new Vector2(-4, -15);
    Vector2 dPadRightPositionOriginal = new Vector2(4, -15);
    Vector2 dPadTopPositionOriginal = new Vector2(0, -11);
    Vector2 dPadBottomPositionOriginal = new Vector2(0, -19);

    Vector2 dPadLeftPosition;
    Vector2 dPadRightPosition;
    Vector2 dPadUpPosition;
    Vector2 dPadDownPosition;

    bool changeDpadPosition = false;

    int totalPoints;

    int currentScore = -1;
    int previousSceneIndex = -1;

    SnakeTypes.SnakeType snakeType;

    // [SerializeField] SnakeType snakeType;

    private void Awake() 
    {

        // Stay in potrait mode
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortraitUpsideDown = false;

        
        int gameManagerCount = FindObjectsOfType<GameManager>().Length;

        if(gameManagerCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    private void Start() {
        // Debug.Log("Hello C");
        // snakeType = SnakeType.Blue;
    }

    /// <summary>
    /// Set the index of the maze scene the player was playing
    /// </summary>
    /// <param name="index">The index of a maze</param>
    public void SetPreviousSceneIndex(int index)
    {
        previousSceneIndex = index;
    }

    /// <summary>
    /// Get the scene index of the maze the player was playing
    /// </summary>
    /// <returns></returns>
     public int GetPreviousSceneIndex()
    {
        return previousSceneIndex;
    }


    /// <summary>
    /// Set the score the player got before dying
    /// </summary>
    /// <param name="score">The players score</param>
    public void SetCurrentScore(int score)
    {
        currentScore = score;
    }

    /// <summary>
    /// Get the score the player got before dying
    /// </summary>
    /// <returns></returns>
    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void SetSnakeType(SnakeTypes.SnakeType snakeType)
    {
        this.snakeType = snakeType;
    }

    public SnakeTypes.SnakeType GetSnakeType()
    {
        return this.snakeType;
    }

    public void AddPoints(int pointsToAdd)
    {
        totalPoints += pointsToAdd;
    }

    public int GetPoints()
    {
        return totalPoints;
    }

    public bool IsSoundOn()
    {
        return soundOn;
    }

    public void SetIsSoundOn(bool isSoundOn)
    {
        soundOn = isSoundOn;
    }

    public bool IsVibrationOn()
    {
        return vibrationOn;
    }

    public void SetIsVibrationOn(bool isVibrationOn)
    {
        vibrationOn = isVibrationOn;
    }

    public Vector2 GetLeftPadPositon()
    {
        return dPadLeftPosition;
    }

    public Vector2 GetRightPadPositon()
    {
        return dPadRightPosition;
    }

    public Vector2 GetTopPadPositon()
    {
        return dPadUpPosition;
    }

    public Vector2 GetBottomPadPositon()
    {
        return dPadDownPosition;
    }

    public bool GetChangeDpadPosition() 
    {
        return changeDpadPosition;   
    }

    public void SetChangeDpadPosition(bool changePosition) 
    {
        changeDpadPosition = changePosition;
    }

    public void SetLeftPadPositon(Vector2 pos)
    {
        Vector3 position = new Vector3(pos.x, pos.y, 0);
        dPadLeftPosition = position;
    }

    public void SetRightPadPositon(Vector2 pos)
    {
        Vector3 position = new Vector3(pos.x, pos.y, 0);
        dPadRightPosition = position;
    }

    public void SetTopPadPositon(Vector2 pos)
    {
        Vector3 position = new Vector3(pos.x, pos.y, 0);
        dPadUpPosition = position;
    }

    public void SetBottomPadPositon(Vector2 pos)
    {
        Vector3 position = new Vector3(pos.x, pos.y, 0);
        dPadDownPosition = position;
    }

    public void ResetPadPositions()
    {
        dPadUpPosition = dPadTopPositionOriginal;
        dPadDownPosition = dPadBottomPositionOriginal;
        dPadLeftPosition = dPadLeftPositionOriginal;
        dPadRightPosition = dPadRightPositionOriginal;
        
    }

    public void SaveData(GameData gameData)
    {
        
        gameData.snakeType = (int)snakeType;
        gameData.points = totalPoints;
        gameData.leftPadPosition = dPadLeftPosition;
        gameData.rightPadPosition = dPadRightPosition;
        gameData.topPadPosition = dPadUpPosition;
        gameData.bottomPadPosition = dPadDownPosition;
        gameData.isSoundOn = soundOn;
        gameData.isVibrationOn = vibrationOn;
    }

    public void LoadData(GameData gameData)
    {
        soundOn = gameData.isSoundOn;
        snakeType = (SnakeTypes.SnakeType)gameData.snakeType;
        totalPoints = gameData.points;
        dPadLeftPosition = gameData.leftPadPosition;
        dPadRightPosition = gameData.rightPadPosition;
        dPadUpPosition = gameData.topPadPosition;
        dPadDownPosition = gameData.bottomPadPosition;
        vibrationOn = gameData.isVibrationOn;

    }
}
