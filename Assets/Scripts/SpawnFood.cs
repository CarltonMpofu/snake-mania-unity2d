using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    [SerializeField] Transform foodParent;
    [SerializeField] GameObject food;

    //[SerializeField] float spawnRate = 3f;

    [Header("Borders")]

    [SerializeField] Transform topBorder;
    [SerializeField] Transform bottomBorder;
    [SerializeField] Transform leftBorder;
    [SerializeField] Transform rightBorder;

    Snake snake;

    Wall[] walls;

    SnakeAbilities snakeAbilities;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        snakeAbilities = FindObjectOfType<SnakeAbilities>();
        if(!snakeAbilities)
        {
            Debug.Log("NO SNAKE ABILITY FOUND");
        }

        gameManager = FindObjectOfType<GameManager>();
        if(!gameManager)
        {
            Debug.Log("NO GAME MANAGER FOUND");
        }

        //Debug.Log("snake is");
        SnakeTypes.SnakeType snakeTypes = gameManager.GetSnakeType();
        //Debug.Log(snakeTypes);

        if(snakeTypes == SnakeTypes.SnakeType.Blue)
            snakeAbilities.SpawnBlueSnakeFood();
        else if(snakeTypes == SnakeTypes.SnakeType.Red)
            snakeAbilities.SpawnRedSnakeFood();
        else if(snakeTypes == SnakeTypes.SnakeType.Yellow)
            snakeAbilities.SpawnYellowSnakeFood();
        else if(snakeTypes == SnakeTypes.SnakeType.Green)
            snakeAbilities.SpawnGreenSnakeFood();
        else // (snakeTypes == SnakeTypes.SnakeType.Pink)
            snakeAbilities.SpawnPinkSnakeFood();

    }

}
