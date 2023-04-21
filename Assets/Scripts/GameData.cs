using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    [Range(0, 4)]
    public int snakeType;

    public int points;


    // true means maze[i] is locked.
    public bool[] maze;

    public int[] mazeCost;
    
    // true means snakes[i] is locked.
    public bool[] snakes;

    public int[] snakesCost;
    public Vector2 leftPadPosition;
    public Vector2 rightPadPosition;
    public Vector2 topPadPosition;
    public Vector2 bottomPadPosition;

    public bool isSoundOn;

    public bool isVibrationOn;

    public GameData()
    {   // Default values
        snakeType = 0;
        points = 0;
        maze = new bool[14] {true, true, true, true, true, true, true, true, true, true, true, true, true, true };
        mazeCost = new int[14] {250, 350, 450, 550, 650, 750, 850, 950, 1050, 1150, 1250, 1350, 1450, 1550};
        snakes = new bool[4] {true, true, true, true};
        snakesCost = new int[4] {300, 500, 700, 1100};
        leftPadPosition = new Vector2(-4, -15);
        rightPadPosition = new Vector2(4, -15);
        topPadPosition = new Vector2(0, -11);
        bottomPadPosition = new Vector2(0, -19);
        isSoundOn = true;
        isVibrationOn = true;
    }


}
