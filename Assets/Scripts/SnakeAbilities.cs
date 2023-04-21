using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAbilities : MonoBehaviour
{
    [SerializeField] Transform foodParent;

    [Header("Snake Food")]
    [SerializeField] GameObject appleFoodGameObject;
    [SerializeField] GameObject grapeFoodGameObject;
    [SerializeField] GameObject largeFoodGameObject;

    [SerializeField] GameObject pinkFoodGameOject;

    [SerializeField] GameObject chainFoodGameObject;

    [Header("Borders")]
    [SerializeField] Transform topBorder;
    [SerializeField] Transform bottomBorder;
    [SerializeField] Transform leftBorder;
    [SerializeField] Transform rightBorder;

    [Header("Large food timers")]
    [SerializeField] float chainTimerConstant = 6f;

    [SerializeField] float largeFoodTimerConstant = 7f;


    Snake snake;

    Wall[] walls;

    SoundManager soundManager;

    GamePlay gamePlay;

    GameObject food;

    GameObject grapeFood1;
    GameObject grapeFood2;
    GameObject grapeFood3;

    GameObject largeFood;

    GameObject ChainFood;

    bool isBlueSnakeFood = false;
    bool isYellowSnakeFood = false;

    bool isRedSnakeFood = false;

    bool isGreenSnakeFood = false;

    bool isPinkSnakeFood = false;
    bool randomSpawn = false;


    //Spawns immediately when it starts
    int largeFoodCounter = -1;

    float largeFoodTimer;
    float largeFoodPoints = 3f;

    float chainFoodTimer;

    bool startChainFoodTimer = false;
    bool startLargeFoodTimer = false;


    // Start is called before the first frame update
    void Start()
    {
        snake = FindObjectOfType<Snake>();

        walls = FindObjectsOfType<Wall>();

        soundManager = FindObjectOfType<SoundManager>();
        if(!soundManager)
            Debug.LogError("NO SOUND MANAGER FOUND!");

        gamePlay = FindObjectOfType<GamePlay>();
        if(!gamePlay)
            Debug.LogError("NO GAME PLAY FOUND!");
        
        largeFoodTimer = largeFoodTimerConstant;

        chainFoodTimer = chainTimerConstant;
    }

    // Update is called once per frame
    void Update()
    {
        if(isBlueSnakeFood)
        {
            if(food == null)
                SpawnOneFood();
        }
        else if(isYellowSnakeFood)
        {
            SpawnThreeFood();
        }
        else if(isRedSnakeFood)
        {
            SpawnOneLargeFood();

            if(startLargeFoodTimer && largeFood != null)
                LargeFoodTimer();
            else
            {
                largeFoodTimer = largeFoodTimerConstant;
            }
        }
        else if(isGreenSnakeFood)
        {
            SpawnDoubleLargeFood();

            if(startLargeFoodTimer && largeFood != null)
                LargeFoodTimer();
            else
            {
                largeFoodTimer = largeFoodTimerConstant;
            }

            if(startChainFoodTimer && ChainFood != null)
                ChainFoodTimer();
            else
            {
                chainFoodTimer = chainTimerConstant;
            }

        }
        else if(isPinkSnakeFood)
        {
            SpawnRandomFood();

            if(startLargeFoodTimer && largeFood != null)
                LargeFoodTimer();
            else
            {
                largeFoodTimer = largeFoodTimerConstant;
            }

            if(startChainFoodTimer && ChainFood != null)
                ChainFoodTimer();
            else
            {
                chainFoodTimer = chainTimerConstant;
            }
        }

        

    }

    public void SpawnBlueSnakeFood()
    {
        isBlueSnakeFood = true;
    }

    public void SpawnYellowSnakeFood()
    {
        isYellowSnakeFood = true;
    }

    public void SpawnRedSnakeFood()
    {
        isRedSnakeFood = true;
    }

    public void SpawnGreenSnakeFood()
    {
        isGreenSnakeFood = true;
    }

    public void SpawnPinkSnakeFood()
    {
        isPinkSnakeFood = true;
    }

    /// <summary>
    /// Check if the newFoodPosition is not already taken by another food
    /// </summary>
    /// <param name="newFoodPosition"></param>
    /// <returns>True if position is empty. Else false.</returns>
    public bool IsFoodPositionAvailable(Vector2 newFoodPosition)
    {
        if(grapeFood1 != null)
        {
            Vector2 foodPositon = new Vector2(
                Mathf.RoundToInt(grapeFood1.transform.position.x), 
                Mathf.RoundToInt(grapeFood1.transform.position.y));

            if(newFoodPosition.Equals(foodPositon))
            {
                return false;
            }
        }

        if(grapeFood2 != null)
        {
            Vector2 foodPositon = new Vector2(
                Mathf.RoundToInt(grapeFood2.transform.position.x), 
                Mathf.RoundToInt(grapeFood2.transform.position.y));

            if(newFoodPosition.Equals(foodPositon))
            {
                return false;
            }
        }

        if(grapeFood3 != null)
        {
            Vector2 foodPositon = new Vector2(
                Mathf.RoundToInt(grapeFood3.transform.position.x), 
                Mathf.RoundToInt(grapeFood3.transform.position.y));

            if(newFoodPosition.Equals(foodPositon))
            {
                return false;
            }
        }

        if(largeFood != null)
        {
            int xPos = Mathf.RoundToInt(largeFood.transform.position.x);
            int yPos = Mathf.RoundToInt(largeFood.transform.position.y);
            
            Vector2 foodPositon0 = new Vector2(xPos, yPos);
            Vector2 foodPositon1 = new Vector2(xPos+1, yPos);
            Vector2 foodPositon2 = new Vector2(xPos-1, yPos);
            Vector2 foodPositon3 = new Vector2(xPos, yPos+1);
            Vector2 foodPositon4 = new Vector2(xPos, yPos-1);
            Vector2 foodPositon5 = new Vector2(xPos-1, yPos-1);
            Vector2 foodPositon6 = new Vector2(xPos-1, yPos+1);
            Vector2 foodPositon7 = new Vector2(xPos+1, yPos+1);
            Vector2 foodPositon8 = new Vector2(xPos+1, yPos-1);

            if(newFoodPosition.Equals(foodPositon0) || newFoodPosition.Equals(foodPositon1)
                || newFoodPosition.Equals(foodPositon2) || newFoodPosition.Equals(foodPositon3)
                || newFoodPosition.Equals(foodPositon4) || newFoodPosition.Equals(foodPositon4)
                || newFoodPosition.Equals(foodPositon5) || newFoodPosition.Equals(foodPositon6)
                || newFoodPosition.Equals(foodPositon7) || newFoodPosition.Equals(foodPositon8))
            {
                return false;
            }  
        }

        if(ChainFood != null)
        {
            int xPos = Mathf.RoundToInt(ChainFood.transform.position.x);
            int yPos = Mathf.RoundToInt(ChainFood.transform.position.y);
            
            Vector2 foodPositon0 = new Vector2(xPos, yPos);
            Vector2 foodPositon1 = new Vector2(xPos+1, yPos);
            Vector2 foodPositon2 = new Vector2(xPos-1, yPos);
            Vector2 foodPositon3 = new Vector2(xPos, yPos+1);
            Vector2 foodPositon4 = new Vector2(xPos, yPos-1);
            Vector2 foodPositon5 = new Vector2(xPos-1, yPos-1);
            Vector2 foodPositon6 = new Vector2(xPos-1, yPos+1);
            Vector2 foodPositon7 = new Vector2(xPos+1, yPos+1);
            Vector2 foodPositon8 = new Vector2(xPos+1, yPos-1);

            if(newFoodPosition.Equals(foodPositon0) || newFoodPosition.Equals(foodPositon1)
                || newFoodPosition.Equals(foodPositon2) || newFoodPosition.Equals(foodPositon3)
                || newFoodPosition.Equals(foodPositon4) || newFoodPosition.Equals(foodPositon4)
                || newFoodPosition.Equals(foodPositon5) || newFoodPosition.Equals(foodPositon6)
                || newFoodPosition.Equals(foodPositon7) || newFoodPosition.Equals(foodPositon8))
            {
                return false;
            }  
        }

        return true;
    }

    /// <summary>
    /// Spawns food gameObject for the snake to eat
    /// </summary>
    void SpawnOneFood()
    {
        while (true)
        { // Keep on looping until position of food is free 
            // Generate random position within borders
            int x = (int)Random.Range(leftBorder.position.x, rightBorder.position.x);
            int y = (int)Random.Range(bottomBorder.position.y, topBorder.position.y);
            Vector2 pos = new Vector2(x, y);

            bool isFree = CanSpawnFood(pos);

            if (isFree == true)
            {
                if(isYellowSnakeFood || randomSpawn)
                    food = Instantiate(grapeFoodGameObject, new Vector2(x, y), Quaternion.identity);
                else if(isPinkSnakeFood)
                    food = Instantiate(pinkFoodGameOject, new Vector2(x, y), Quaternion.identity);
                else
                    food = Instantiate(appleFoodGameObject, new Vector2(x, y), Quaternion.identity);

                food.transform.parent = foodParent;
                break;
            }
        }

    }

    /// <summary>
    /// Check if the specified position is free to spawn food 
    /// </summary>
    /// <param name="pos">Vector2: The specified position</param>
    /// <returns>True: If position is free, else False</returns>
    private bool CanSpawnFood(Vector2 pos)
    {   
        bool isFree;

        // Check if position already has food or not
        // Or is not position of the wall
        // And if position of the snake
        isFree = snake.IsPositionFree(pos) && IsFoodPositionAvailable(pos);
        if (walls.Length != 0)
        {
            foreach (Wall wall in walls)
            {
                Vector2 wallPositon = new Vector2(
                    Mathf.RoundToInt(wall.transform.position.x),
                    Mathf.RoundToInt(wall.transform.position.y));
                if (wallPositon.Equals(pos))
                {
                    isFree = false;
                    break;
                }
            }
        }

        return isFree;
    }

    /// <summary>
    /// Spawns large food for the snake to eat
    /// </summary>
    void SpawnLargeFood()
    {
        while (true)
        { // Keep on looping until positio of food is free

            // Generate random position within borders
            int x = (int)Random.Range(leftBorder.position.x, rightBorder.position.x);
            int y = (int)Random.Range(bottomBorder.position.y, topBorder.position.y);
            // Large food takes 3 * 3 space 
            Vector2 pos1 = new Vector2(x, y);
            Vector2 pos2 = new Vector2(x+1, y);
            Vector2 pos3 = new Vector2(x-1, y);
            Vector2 pos4 = new Vector2(x, y+1);
            Vector2 pos5 = new Vector2(x, y-1);
            Vector2 pos6 = new Vector2(x-1, y+1);
            Vector2 pos7 = new Vector2(x-1, y-1);
            Vector2 pos8 = new Vector2(x+1, y+1);
            Vector2 pos9 = new Vector2(x+1, y-1);

            // check if all positions are free
            bool isFree = CanSpawnFood(pos1) && CanSpawnFood(pos2) && CanSpawnFood(pos3) &&
                            CanSpawnFood(pos4) && CanSpawnFood(pos5) && CanSpawnFood(pos6) &&
                            CanSpawnFood(pos7) && CanSpawnFood(pos8) && CanSpawnFood(pos9);

            if (isFree == true)
            {
                largeFood = Instantiate(largeFoodGameObject, new Vector2(x, y), Quaternion.identity);
                largeFood.transform.parent = foodParent;
                break;
            }
        }
        soundManager.PlayLargeSound();
        
        // reset timer and start
        largeFoodTimer = largeFoodTimerConstant;
        startLargeFoodTimer = true;

    }

    /// <summary>
    /// Spawns chain food for the snake to eat
    /// </summary>
    void SpawnChainFood()
    {
        while (true)
        { // Keep on looping until position of food is free

            // Generate random position within borders
            int x = (int)Random.Range(leftBorder.position.x, rightBorder.position.x);
            int y = (int)Random.Range(bottomBorder.position.y, topBorder.position.y);
            // Chain food takes 3 * 3 space
            Vector2 pos1 = new Vector2(x, y);
            Vector2 pos2 = new Vector2(x+1, y);
            Vector2 pos3 = new Vector2(x-1, y);
            Vector2 pos4 = new Vector2(x, y+1);
            Vector2 pos5 = new Vector2(x, y-1);
            Vector2 pos6 = new Vector2(x-1, y+1);
            Vector2 pos7 = new Vector2(x-1, y-1);
            Vector2 pos8 = new Vector2(x+1, y+1);
            Vector2 pos9 = new Vector2(x+1, y-1);

            bool isFree = CanSpawnFood(pos1) && CanSpawnFood(pos2) && CanSpawnFood(pos3) &&
                            CanSpawnFood(pos4) && CanSpawnFood(pos5) && CanSpawnFood(pos6) &&
                            CanSpawnFood(pos7) && CanSpawnFood(pos8) && CanSpawnFood(pos9);

            if (isFree == true)
            {
                ChainFood = Instantiate(chainFoodGameObject, new Vector2(x, y), Quaternion.identity);
                ChainFood.transform.parent = foodParent;
                break;
            }
        }
        soundManager.PlayChainSound();

        // reset timer and start
        chainFoodTimer = chainTimerConstant;
        startChainFoodTimer = true;
    }


    public void ReSpawnChainFood()
    {
        SpawnChainFood();
    }

    /// <summary>
    /// Spawns three food for the snake to eat
    /// </summary>
    void SpawnThreeFood()
    {
        if(grapeFood1 == null)
        {
            SpawnOneFood();
            grapeFood1 = food;
        }
        if(grapeFood2 == null)
        {
            SpawnOneFood();
            grapeFood2 = food;
        }
        if(grapeFood3 == null)
        {
            SpawnOneFood();
            grapeFood3 = food;
        } 
    }

    /// <summary>
    /// Spawns large food when the snake eats five small food
    /// </summary>
    void SpawnOneLargeFood()
    {
        if(food == null)
        {
            SpawnOneFood();
            if(largeFood == null)
            {
                largeFoodCounter++;
                //Debug.Log(largeFoodCounter);
            }
            
            if(largeFoodCounter > 4)
            {
                //Debug.Log("BIG!!");
                SpawnLargeFood();
                
                largeFoodCounter = 0;
            }
        }
    }

    /// <summary>
    /// Spawns one large food and one chain food when the snake eats five small food
    /// </summary>
    void SpawnDoubleLargeFood()
    {
        if(food == null)
        {
            SpawnOneFood();
            if(largeFood == null)
            {
                largeFoodCounter++;
                //Debug.Log(largeFoodCounter);
            }
            
            if(largeFoodCounter > 4)
            {
                //Debug.Log("BIG TWO!!");
                SpawnLargeFood();

                if(ChainFood == null)
                {
                    SpawnChainFood();
                    
                }
                
                startLargeFoodTimer = true;
                largeFoodCounter = 0;
            }
        }
    }

    /// <summary>
    /// Chooses random number between 0 and 2 (both inclusive).
    /// And either spawns three food, or one large food, or one large +  one chain food.
    /// Depending on what the random number is 
    /// </summary>
    void SpawnRandomFood()
    {
        if(food == null)
        {
            SpawnOneFood();
            if(largeFood == null)
            {
                largeFoodCounter++;
                Debug.Log(largeFoodCounter);
            }
            
            if(largeFoodCounter > 4)
            {
                int randomInt = Random.Range(0,3);
                //int randomInt = 0;

                if(randomInt == 0)
                { // Spawn three
                    randomSpawn = true;
                    GameObject oldaFood = food;
                    SpawnThreeFood();
                    food = oldaFood;
                    randomSpawn = false;
                }
                else if(randomInt == 1)
                { // spawn one large
                    SpawnLargeFood();
                    
                }
                else if(randomInt == 2)
                { // spawn large + chain
                    SpawnLargeFood();

                    if(ChainFood == null)
                    {
                        SpawnChainFood();
                        
                    }
                
                    startLargeFoodTimer = true;
                }

                largeFoodCounter = 0;
            }
        }
    }

    /// <summary>
    /// Countdown timer for how long large food is shown on screen
    /// </summary>
    void LargeFoodTimer()
    {
        if(gamePlay.IsGamePlaying())
        {
            largeFoodTimer -= 1 * Time.deltaTime;
            int timeLeft = (int)Mathf.Floor(largeFoodTimer);
            if(timeLeft < 0)
            {
                Destroy(largeFood);
                startLargeFoodTimer = false;
            }
        }
    }

    /// <summary>
    /// Countdown timer for how long chain food is shown on screen
    /// </summary>
    void ChainFoodTimer()
    {
        if(gamePlay.IsGamePlaying())
        {
            chainFoodTimer -= 1 * Time.deltaTime;
            int timeLeft = (int)Mathf.Floor(chainFoodTimer);
            if(timeLeft < 0)
            {
                Destroy(ChainFood);
                startChainFoodTimer = false;
            }
        }
    }

    public int GetAppleScore()
    {
        return 1;
    }

    public int GetGrapeScore()
    {
        return 2;
    }

    public int GetPinkyScore()
    {
        return 3;
    }

    public int GetLargeScore()
    {
        return (int)Mathf.Floor(largeFoodPoints * largeFoodTimer);
    }

    public int GetChainFoodScore()
    {
        return (int)Mathf.Floor(largeFoodPoints * chainFoodTimer);
    }

}
