using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTypes : MonoBehaviour, IDataPersistence
{
    [SerializeField] Sprite[] headsRight;
    [SerializeField] Sprite[] headsLeft;
    [SerializeField] Sprite[] headsUp;
    [SerializeField] Sprite[] headsDown;

    [SerializeField] Sprite[] bodysBottomLeft;
    [SerializeField] Sprite[] bodysBottomRight;
    [SerializeField] Sprite[] bodysTopLeft;
    [SerializeField] Sprite[] bodysTopRight;

    [SerializeField] Sprite[] bodysHorizontal;
    [SerializeField] Sprite[] bodysVertical;

    [SerializeField] Sprite[] tailsRight;
    [SerializeField] Sprite[] tailsLeft;
    [SerializeField] Sprite[] tailsUp;
    [SerializeField] Sprite[] tailsDown;

    public enum  SnakeType {Blue, Red,Yellow, Green, Pink};

    SnakeType snakeType;

    GameManager gameManager;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();

        if(!gameManager)
            Debug.LogError("NO GAME MANAGER FOUND");

        snakeType = gameManager.GetSnakeType();
    }

    public void ChangeSnake(SnakeType newSnakeType)
    {
        snakeType = newSnakeType;
    }

    public Sprite GetHeadRight()
    {
        return headsRight[(int)snakeType];
    }

    public Sprite GetHeadLeft()
    {
        return headsLeft[(int)snakeType];
    }
    public Sprite GetHeadUp()
    {
        return headsUp[(int)snakeType];
    }
    public Sprite GetHeadDown()
    {
        return headsDown[(int)snakeType];
    }

    public Sprite GetBodyBottomLeft()
    {
        return bodysBottomLeft[(int)snakeType];
    }
    public Sprite GetBodyBottomRight()
    {
        return bodysBottomRight[(int)snakeType];
    }
    public Sprite GetBodyTopLeft()
    {
        return bodysTopLeft[(int)snakeType];
    }
    public Sprite GetBodyTopRight()
    {
        return bodysTopRight[(int)snakeType];
    }

    public Sprite GetBodyHorizontal()
    {
        return bodysHorizontal[(int)snakeType];
    }
    public Sprite GetBodyVertical()
    {
        return bodysVertical[(int)snakeType];
    }

    public Sprite GetTailRight()
    {
        return tailsRight[(int)snakeType];
    }
    public Sprite GetTailLeft()
    {
        return tailsLeft[(int)snakeType];
    }
    public Sprite GetTailUp()
    {
        return tailsUp[(int)snakeType];
    }
    public Sprite GetTailDown()
    {
        return tailsDown[(int)snakeType];
    }

    public void SaveData(GameData data)
    {
        
    }

    public void LoadData(GameData gameData)
    {
        snakeType = (SnakeType)gameData.snakeType;
    }
}
