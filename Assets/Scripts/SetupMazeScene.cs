using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetupMazeScene : MonoBehaviour, IDataPersistence
{
    [SerializeField] GameObject mazeCanvas;

    [SerializeField] GameObject unlockLevelUI;
    [SerializeField] GameObject morePointsUI;


    [SerializeField] GameObject[] mazeObjects;

    int[] mazeCost;

    bool[] mazeLocks;

    int selectedMazeIndex;

    int totalPoints;

    // Start is called before the first frame update
    void Start()
    {
        unlockLevelUI.SetActive(false);
        morePointsUI.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Unlocks the selected maze
    /// </summary>
    public void UnLockIt()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();

        mazeObjects[selectedMazeIndex].GetComponentInChildren<BackgroudImage>().gameObject.SetActive(false);

        mazeLocks[selectedMazeIndex] = false;

        HideUnlockLevelUI();

        totalPoints -= GetMazeCost();

        FindObjectOfType<GameManager>().AddPoints(-GetMazeCost());
    }

    /// <summary>
    /// Checks if the current selected maze is locked
    /// </summary>
    /// <returns>True: If maze is locked, else False</returns>
    public bool IsSelectedMazeLocked()
    {
        return mazeLocks[selectedMazeIndex];
    }

    /// <summary>
    /// Sets the current maze selected by the user
    /// </summary>
    /// <param name="mazeIndex">The index of the maze selected by the user</param>
    public void SetSelectedMazeIndex(int mazeIndex)
    {
        selectedMazeIndex = mazeIndex;
    }

    /// <summary>
    /// Gets the cost of the current maze selected by the user
    /// </summary>
    /// <returns>int: The cost of the maze</returns>
    int GetMazeCost()
    {
        return mazeCost[selectedMazeIndex];
    }

    /// <summary>
    /// Unlocks the current maze selected by the player if the player has enough points.
    /// Else tells the player how many points are needed to unlock the selected maze.
    /// </summary>
    public void UnlockSelectedMaze()
    {
        if(totalPoints < GetMazeCost())
        {
            ShowMorePointsUI();
        }
        else
        {
            ShowUnlockLevelUI();
        }
    }

    /// <summary>
    /// Hide the UI for unlocking the maze
    /// </summary>
    public void HideUnlockLevelUI()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();

        unlockLevelUI.SetActive(false);
        // Make sure player can click on canvas
        mazeCanvas.GetComponent<GraphicRaycaster>().enabled = true;
    }

    /// <summary>
    /// Show the UI for unlocking the maze
    /// </summary>
    void ShowUnlockLevelUI()
    {
        unlockLevelUI.SetActive(true);
        Cost mazeCostScript = unlockLevelUI.GetComponentInChildren<Cost>();
        mazeCostScript.gameObject.GetComponent<TextMeshProUGUI>().text = GetMazeCost().ToString();
        // Make sure player cannot click on canvas
        mazeCanvas.GetComponent<GraphicRaycaster>().enabled = false;
    }

    /// <summary>
    /// Hide the UI that shows player how many points are needed to unlock the maze
    /// </summary>
    public void HideMorePointsUIUI()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();

        morePointsUI.SetActive(false);
        // Make sure player can click on canvas
        mazeCanvas.GetComponent<GraphicRaycaster>().enabled = true;
    }

    /// <summary>
    /// Show the UI that shows player how many points are needed to unlock the maze
    /// </summary>
    void ShowMorePointsUI()
    {
        morePointsUI.SetActive(true);
        Cost mazeCostScript = morePointsUI.GetComponentInChildren<Cost>();

        int pointsNeeded = GetMazeCost() - totalPoints;
        mazeCostScript.gameObject.GetComponent<TextMeshProUGUI>().text = pointsNeeded.ToString();
        // Make sure player cannot click on canvas
        mazeCanvas.GetComponent<GraphicRaycaster>().enabled = false;
    }

    /// <summary>
    /// Sets maze background to black if the maze is locked
    /// </summary>
    private void SetUpMazeBackground()
    {
        for (int i = 0; i < mazeObjects.Length; i++)
        {
            mazeObjects[i].GetComponentInChildren<BackgroudImage>().gameObject.SetActive(mazeLocks[i]);

            Cost mazeCostScript = mazeObjects[i].GetComponentInChildren<Cost>();
            if(mazeCostScript) // if the maze is still locked
                mazeCostScript.gameObject.GetComponent<TextMeshProUGUI>().text = mazeCost[i].ToString();
        }
    }

    public void LoadData(GameData gameData)
    {
        mazeLocks = gameData.maze;
        mazeCost = gameData.mazeCost;
        totalPoints = gameData.points;

        SetUpMazeBackground();

    }

    public void SaveData(GameData gameData)
    {
        gameData.maze = mazeLocks;
        gameData.points = totalPoints;
    }

}
