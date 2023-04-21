using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSnake : MonoBehaviour
{
    public void UnlockRedSnake()
    {
        SetupSnakeScene setupSnakeScene = FindObjectOfType<SetupSnakeScene>();
        setupSnakeScene.SelectSnake(0);
    }

    public void UnlockYellowSnake()
    {
        SetupSnakeScene setupSnakeScene = FindObjectOfType<SetupSnakeScene>();
        setupSnakeScene.SelectSnake(1);
    }

    public void UnlockGreenSnake()
    {
        SetupSnakeScene setupSnakeScene = FindObjectOfType<SetupSnakeScene>();
        setupSnakeScene.SelectSnake(2);
    }

    public void UnlockPinkSnake()
    {
        SetupSnakeScene setupSnakeScene = FindObjectOfType<SetupSnakeScene>();
        setupSnakeScene.SelectSnake(3);
    }

}
