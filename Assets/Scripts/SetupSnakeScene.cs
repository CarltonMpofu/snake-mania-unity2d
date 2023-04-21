using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetupSnakeScene : MonoBehaviour, IDataPersistence
{
    [Header("UI Objects")]
    [SerializeField] GameObject snakeCanvas;

    [SerializeField] GameObject unlockSnakelUI;
    [SerializeField] GameObject morePointsUI;

    [Header("Snake Objects")]
    [SerializeField] GameObject[] snakeObjects;

    [SerializeField] GameObject[] snakeToggleObjects;


    int[] snakesCost;
    
    bool[] snakeLocks;

    int selectedSnakeIndex;

    int totalPoints;
    int currentSnakeIndex;

    // When scene is loading 
    bool loading = true;

    private void Awake() {
        for(int i = 1; i < snakeToggleObjects.Length; i++)
        {
            snakeToggleObjects[i].GetComponent<Toggle>().isOn = false;
            snakeToggleObjects[i].GetComponent<Toggle>().interactable = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        unlockSnakelUI.SetActive(false);
        morePointsUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Changes the snake used to play game  
    /// </summary>
    /// <param name="newSnakeIndex"> The index of the selecteed snake</param>
    public void ChangeSnake(int newSnakeIndex)
    {
        // If scene is not loading for the first time
        if(!loading)
        {
            FindObjectOfType<SoundManager>().PlayClickSound();

            snakeToggleObjects[currentSnakeIndex].GetComponent<Toggle>().isOn = false;
            snakeToggleObjects[currentSnakeIndex].GetComponent<Toggle>().interactable = true;

            //snakeToggleObjects[newSnakeIndex].GetComponent<Toggle>().isOn = true;
            snakeToggleObjects[newSnakeIndex].GetComponent<Toggle>().interactable = false;

            currentSnakeIndex = newSnakeIndex;


        }
    }

    /// <summary>
    /// Unlocks the snake selected by player
    /// </summary>
    public void UnLockIt()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();

        snakeObjects[selectedSnakeIndex].GetComponent<Button>().enabled = false;
        snakeObjects[selectedSnakeIndex].GetComponentInChildren<BackgroudImage>().gameObject.SetActive(false);

        snakeToggleObjects[selectedSnakeIndex+1].GetComponent<Toggle>().interactable = true;

        snakeLocks[selectedSnakeIndex] = false;

        HideUnlockSnakelUI();

        // reduce points
        totalPoints -= GetSnakeCost();

        FindObjectOfType<GameManager>().AddPoints(-GetSnakeCost());
    }

    /// <summary>
    /// Get the cost of the snake selected by the player
    /// </summary>
    /// <returns>int: The cost of the snake</returns>
    int GetSnakeCost()
    {
        return snakesCost[selectedSnakeIndex];
    }


    /// <summary>
    /// Show the UI for unlocking the snake
    /// </summary>
    void ShowUnlockSnakelUI()
    {
        unlockSnakelUI.SetActive(true);

        Cost snakeCostScript = unlockSnakelUI.GetComponentInChildren<Cost>();
        snakeCostScript.gameObject.GetComponent<TextMeshProUGUI>().text = GetSnakeCost().ToString();
        // Make sure player cannot click on canvas
        snakeCanvas.GetComponent<GraphicRaycaster>().enabled = false;
    }

    /// <summary>
    /// Hide the UI for unlocking the snake
    /// </summary>
    public void HideUnlockSnakelUI()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();

        unlockSnakelUI.SetActive(false);
        // Make sure player can click on canvas
        snakeCanvas.GetComponent<GraphicRaycaster>().enabled = true;
    }


    /// <summary>
    /// Show the UI that tells player how many points needed to unlock the selected snake
    /// </summary>
     void ShowMorePointsUI()
    {
        morePointsUI.SetActive(true);
        Cost snakeCostScript = morePointsUI.GetComponentInChildren<Cost>();

        int pointsNeeded = GetSnakeCost() - totalPoints;
        snakeCostScript.gameObject.GetComponent<TextMeshProUGUI>().text = pointsNeeded.ToString();
        // Make sure player cannot click on canvas
        snakeCanvas.GetComponent<GraphicRaycaster>().enabled = false;
    }

    /// <summary>
    /// Hide the UI that tells player how many points needed to unlock the selected snake
    /// </summary>
    public void HideMorePointsUIUI()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();

        morePointsUI.SetActive(false);
        // Make sure player can click on canvas
        snakeCanvas.GetComponent<GraphicRaycaster>().enabled = true;
    }

    /// <summary>
    /// Unlocks the selected snake if the player has enough points.
    /// Else tell the player how many points are needed to unlock the snake.
    /// </summary>
    /// <param name="snakeIndex">The index of the snake selected by the player</param>
    public void SelectSnake(int snakeIndex)
    {
        FindObjectOfType<SoundManager>().PlayClickSound();

        selectedSnakeIndex = snakeIndex;

        if(totalPoints < GetSnakeCost())
        {
            ShowMorePointsUI();
        }
        else
        {
            ShowUnlockSnakelUI();
        }
        
    }


    public void LoadData(GameData gameData)
    {
        snakeLocks = gameData.snakes;
        snakesCost = gameData.snakesCost;
        totalPoints = gameData.points;

        currentSnakeIndex = gameData.snakeType;
        // First time loading scene
        loading = true;
        // Set up snake background if snake is locked
        // And show the cost of the snake
        for(int i = 0; i < snakeObjects.Length; i++)
        {
            snakeObjects[i].GetComponentInChildren<BackgroudImage>().gameObject.SetActive(snakeLocks[i]);
            snakeObjects[i].GetComponent<Button>().enabled = snakeLocks[i];


            snakeToggleObjects[i+1].GetComponent<Toggle>().interactable = !snakeLocks[i];

            if(currentSnakeIndex == i + 1)
            {
                snakeToggleObjects[i+1].GetComponent<Toggle>().isOn = true;
                snakeToggleObjects[i+1].GetComponent<Toggle>().interactable = false;
            }
            else
            {
                snakeToggleObjects[i+1].GetComponent<Toggle>().isOn = false;
            }

            Cost cost = snakeObjects[i].GetComponentInChildren<Cost>();

            if(cost)
                cost.gameObject.GetComponent<TextMeshProUGUI>().text = snakesCost[i].ToString();
        }

        if(currentSnakeIndex == 0)
        {
            snakeToggleObjects[0].GetComponent<Toggle>().isOn = true;
            snakeToggleObjects[0].GetComponent<Toggle>().interactable = false;
        }
        else
        {
            snakeToggleObjects[0].GetComponent<Toggle>().isOn = false;
        }
        // Done loading
        loading = false;


    }

    public void SaveData(GameData gameData)
    {
        gameData.snakes = snakeLocks;
        gameData.snakeType = currentSnakeIndex;
    }

    
}
