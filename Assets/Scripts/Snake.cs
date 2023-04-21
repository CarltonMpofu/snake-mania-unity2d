using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] Transform snakeParent;

    [Header("Snake Speed")]
    [SerializeField] float snakeSpeed = 0.3f;
    [SerializeField] float decreaseSnakeSpeedByValue = 0.0025f;

    [Header("Snake Parts")]
    [SerializeField] GameObject headRightPrefab;
    [SerializeField] GameObject headLeftPrefab;
    [SerializeField] GameObject headUpPrefab;
    [SerializeField] GameObject headDownPrefab;

    [SerializeField] GameObject bodyBottomLeftPrefab;
    [SerializeField] GameObject bodyBottomRightPrefab;
    [SerializeField] GameObject bodyTopLeftPrefab;
    [SerializeField] GameObject bodyTopRightPrefab;

    [SerializeField] GameObject bodyHorizontalPrefab;
    [SerializeField] GameObject bodyVerticalPrefab;

    [SerializeField] GameObject tailRightPrefab;
    [SerializeField] GameObject tailLeftPrefab;
    [SerializeField] GameObject tailUpPrefab;
    [SerializeField] GameObject tailDownPrefab;

    [Header("Borders")]
    [SerializeField] Transform borderTop;
    [SerializeField] Transform borderBottom;
    [SerializeField] Transform borderRight;
    [SerializeField] Transform borderLeft;


    List<GameObject> snake;

    Vector2 currentDir;
    Vector2 previousDir;

    bool ate = false;
    bool canPress = true;

    int foodCounter = 0;

    GamePlay gamePlay;
    GameManager gameManager;

    SnakeTypes snakeTypes;

    private void Start()
    {
        // For storing snake parts
        snake = new List<GameObject>();

        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager)
            Debug.LogError("NO GAME MANAGER FOUND");

        snakeTypes = FindObjectOfType<SnakeTypes>();
        if (!snakeTypes)
            Debug.LogError("NO SNAKE TYPES FOUND");
        else
        {
            SetSnakeSprites();
        }

        // Set the snakes initial direction
        currentDir = Vector2.right;
        previousDir = Vector2.right;

        CreateSnake();

        gamePlay = FindObjectOfType<GamePlay>();
        if (!gamePlay)
        {
            Debug.LogError("NO GAMEPLAY");
        }

        // Start moving the snake after 1 second
        InvokeRepeating("MoveSnake", 1f, snakeSpeed);

    }

    /// <summary>
    /// Instantiate the first four snake parts 
    /// </summary>
    private void CreateSnake()
    {
        GameObject newHead = Instantiate(headRightPrefab, transform.position, Quaternion.identity);
        snake.Add(newHead);
        snake.Add(Instantiate(
        bodyHorizontalPrefab, new Vector2(transform.position.x - 1, transform.position.y), Quaternion.identity));
        snake.Add(Instantiate(
        bodyHorizontalPrefab, new Vector2(transform.position.x - 2, transform.position.y), Quaternion.identity));
        snake.Add(Instantiate(
        tailLeftPrefab, new Vector2(transform.position.x - 3, transform.position.y), Quaternion.identity));
    }

    /// <summary>
    /// Decreases the number of seconds the MoveSnake method is called.
    /// Which actually increases the speed of the snake.
    /// </summary>
    public void IncreaseSnakeSpeed()
    {
        foodCounter += 1;
        if(foodCounter >= 5)
        {
            foodCounter = 0;
            snakeSpeed -= decreaseSnakeSpeedByValue;
            CancelInvoke();
            InvokeRepeating("MoveSnake", snakeSpeed, snakeSpeed);
            
        } 
        
    }

    /// <summary>
    /// Setup how the snake looks depending on chosen snake
    /// </summary>
    private void SetSnakeSprites()
    {
        headRightPrefab.GetComponent<SpriteRenderer>().sprite = snakeTypes.GetHeadRight();
        headLeftPrefab.GetComponent<SpriteRenderer>().sprite = snakeTypes.GetHeadLeft();
        headUpPrefab.GetComponent<SpriteRenderer>().sprite = snakeTypes.GetHeadUp();
        headDownPrefab.GetComponent<SpriteRenderer>().sprite = snakeTypes.GetHeadDown();
        bodyBottomLeftPrefab.GetComponent<SpriteRenderer>().sprite = snakeTypes.GetBodyBottomLeft();
        bodyBottomRightPrefab.GetComponent<SpriteRenderer>().sprite = snakeTypes.GetBodyBottomRight();
        bodyTopLeftPrefab.GetComponent<SpriteRenderer>().sprite = snakeTypes.GetBodyTopLeft();
        bodyTopRightPrefab.GetComponent<SpriteRenderer>().sprite = snakeTypes.GetBodyTopRight();

        bodyHorizontalPrefab.GetComponent<SpriteRenderer>().sprite = snakeTypes.GetBodyHorizontal();
        bodyVerticalPrefab.GetComponent<SpriteRenderer>().sprite = snakeTypes.GetBodyVertical();

        tailRightPrefab.GetComponent<SpriteRenderer>().sprite = snakeTypes.GetTailRight();
        tailLeftPrefab.GetComponent<SpriteRenderer>().sprite = snakeTypes.GetTailLeft();
        tailUpPrefab.GetComponent<SpriteRenderer>().sprite = snakeTypes.GetTailUp();
        tailDownPrefab.GetComponent<SpriteRenderer>().sprite = snakeTypes.GetTailDown();

    }

    private void Update()
    {
        if (gamePlay.IsGamePlaying() == true && canPress)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            { // Move snake UP
                ChangeDirection(Vector2.down, Vector2.up);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            { // Move snake DOWN
                ChangeDirection(Vector2.up, Vector2.down);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            { // Move snake RIGHT
                ChangeDirection(Vector2.left, Vector2.right);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            { // Move snake LEFT
                ChangeDirection(Vector2.right, Vector2.left);
            }
        }
        
    }

    /// <summary>
    /// Get the current snake direction
    /// </summary>
    /// <returns>Vector2: The current direction</returns>
    public Vector2 GetCurrentDirection(){
        return currentDir;
    }

    /// <summary>
    /// Change the snake direction only if the new direction is a 90 degree turn
    /// </summary>
    /// <param name="oppositeDirection">The direction opposite the newDirection</param>
    /// <param name="newDirection"> The new direction the snake has to go</param>
    public void ChangeDirection(Vector2 oppositeDirection, Vector2 newDirection)
    {
        if(canPress)
        { // Can move
            if(currentDir != oppositeDirection)
            { // Only change direction if it's a 90 degree turn
                canPress = false;
                previousDir = currentDir;
                currentDir = newDirection;
            }
        }
        
    }

    void MoveSnake()
    {

        if (gamePlay.IsGamePlaying() == true)
        {
            // Get the head
            GameObject headPrefab = snake[0];

            // Current snake head position (No gap)
            Vector2 currentPosition = headPrefab.transform.position;

            // Change the head when direction changed
            if (currentDir != previousDir)
            {
                // Remove the current head
                snake.RemoveAt(0);
                Destroy(headPrefab);

                // Change the head to face new direction
                if (currentDir == Vector2.up)
                {
                    // Set new snake head looking up
                    headPrefab = Instantiate(headUpPrefab, currentPosition, Quaternion.identity);
                }
                else if (currentDir == Vector2.down)
                {
                    // Set new snake head looking down
                    headPrefab = Instantiate(headDownPrefab, currentPosition, Quaternion.identity);
                }
                else if (currentDir == Vector2.right)
                {
                    // Set new snake head looking right
                    headPrefab = Instantiate(headRightPrefab, currentPosition, Quaternion.identity);
                }
                else
                {
                    // Set new snake head looking left
                    headPrefab = Instantiate(headLeftPrefab, currentPosition, Quaternion.identity);
                }
                headPrefab.transform.parent = snakeParent;
                // Replace head 
                snake.Insert(0, headPrefab);
            }

            Vector2 targetPos = (Vector2)headPrefab.transform.position + currentDir;
            targetPos = new Vector2(Mathf.RoundToInt(targetPos.x), Mathf.RoundToInt(targetPos.y));
            
            // Check if snake head is touching any of the borders
            // And then move the snake head to opposite border 
            if (currentDir == Vector2.up && targetPos.y >= borderTop.position.y)
            {
                headPrefab.transform.position = new Vector2(headPrefab.transform.position.x, borderBottom.position.y+1f);
            }
            else if (currentDir == Vector2.down && targetPos.y <= borderBottom.position.y)
            {
                headPrefab.transform.position = new Vector2(headPrefab.transform.position.x, borderTop.position.y-1f);
            }
            else if (currentDir == Vector2.right && targetPos.x >= borderRight.position.x)
            {
                headPrefab.transform.position = new Vector2(borderLeft.position.x+1, headPrefab.transform.position.y);
            }
            else if (currentDir == Vector2.left && targetPos.x <= borderLeft.position.x)
            {
                headPrefab.transform.position = new Vector2(borderRight.position.x-1f, headPrefab.transform.position.y);
            }
            else
            {
                // Snake moved (Has a gap )
                headPrefab.transform.Translate(currentDir);
            }

            Head head = headPrefab.GetComponent<Head>();
            if (!head)
                Debug.LogError("SNAKE HAS NO HEAD");  

            // Did the snake eat ?
            if (ate) // Yes. snake ate
            {
                GameObject body;

                // Did the direction change when snake immediately ate ?
                if (currentDir != previousDir) // Yes
                {
                    // Include the right/proper corner
                    if (currentDir == Vector2.up && previousDir == Vector2.right)
                    {
                        body = Instantiate(bodyTopLeftPrefab, currentPosition, Quaternion.identity);

                    }
                    else if (currentDir == Vector2.up && previousDir == Vector2.left)
                    {
                        body = Instantiate(bodyTopRightPrefab, currentPosition, Quaternion.identity);
                    }
                    else if (currentDir == Vector2.down && previousDir == Vector2.right)
                    {
                        body = Instantiate(bodyBottomLeftPrefab, currentPosition, Quaternion.identity);
                    }
                    else if (currentDir == Vector2.down && previousDir == Vector2.left)
                    {
                        body = Instantiate(bodyBottomRightPrefab, currentPosition, Quaternion.identity);
                    }
                    else if (currentDir == Vector2.right && previousDir == Vector2.up)
                    {
                        body = Instantiate(bodyBottomRightPrefab, currentPosition, Quaternion.identity);
                    }
                    else if (currentDir == Vector2.right && previousDir == Vector2.down)
                    {
                        body = Instantiate(bodyTopRightPrefab, currentPosition, Quaternion.identity);
                    }
                    else if (currentDir == Vector2.left && previousDir == Vector2.up)
                    {
                        body = Instantiate(bodyBottomLeftPrefab, currentPosition, Quaternion.identity);
                    }
                    else if (currentDir == Vector2.left && previousDir == Vector2.down)
                    {
                        body = Instantiate(bodyTopLeftPrefab, currentPosition, Quaternion.identity);
                    }
                    else 
                    {
                        body = Instantiate(bodyTopLeftPrefab, currentPosition, Quaternion.identity);
                    }

                    // Set the direction the snake is going in when it turned
                    body.GetComponent<Corner>().setDirection(currentDir);

                }
                else // No, direction did not change
                {
                    // Include the vertical part if snake going up or down
                    // Else include horizontal part 
                    if (currentDir == Vector2.up || currentDir == Vector2.down)
                    {
                        body = Instantiate(bodyVerticalPrefab, currentPosition, Quaternion.identity);
                    }
                    else
                    {
                        body = Instantiate(bodyHorizontalPrefab, currentPosition, Quaternion.identity);
                    }
                }

                body.transform.parent = snakeParent;

                // Mark position/part where snake ate
                body.GetComponent<SpriteRenderer>().color = new Color(0.05f, 0.61f, 0.64f);

                // Fill up gap
                snake.Insert(1, body);
                ate = false;
            }
            else // No. Snake did not eat.
            {
                GameObject secondLast = snake[snake.Count - 2];

                if (secondLast.GetComponent<SpriteRenderer>().color != Color.white)
                { // Unmark the positon where snake ate since it has reached the back
                    secondLast.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                }

                Vector2 newTailPos = secondLast.transform.position;
                // Remove from second last position
                snake.RemoveAt(snake.Count - 2);

                // Get corner
                Corner corner = secondLast.GetComponent<Corner>();

                // If the second last part is a where the snake turns then
                // change the tail prefab
                if (corner)
                {
                    GameObject tail = snake[snake.Count - 1];
                    snake.RemoveAt(snake.Count - 1);

                    GameObject newTail;

                    // Include the right tail depending on which directions snake was going in at the corner
                    if (corner.GetDirection() == Vector2.up)
                    {
                        newTail = Instantiate(tailDownPrefab, tail.transform.position, Quaternion.identity);

                    }
                    else if (corner.GetDirection() == Vector2.down)
                    {
                        newTail = Instantiate(tailUpPrefab, tail.transform.position, Quaternion.identity);

                    }
                    else if (corner.GetDirection() == Vector2.right)
                    {
                        newTail = Instantiate(tailLeftPrefab, tail.transform.position, Quaternion.identity);
                    }
                    else
                    {
                        newTail = Instantiate(tailRightPrefab, tail.transform.position, Quaternion.identity);

                    }

                    newTail.transform.parent = snakeParent;
                    // Add the tail
                    snake.Add(newTail);
                    // Destroy old tail
                    Destroy(tail);
                }


                // Changed direction ?
                if (currentDir != previousDir) // Yes
                {
                    Destroy(secondLast);
                    GameObject turnPart;

                    // Insert the right corner
                    if (currentDir == Vector2.up && previousDir == Vector2.right)
                    {
                        turnPart = Instantiate(bodyTopLeftPrefab, currentPosition, Quaternion.identity);

                    }
                    else if (currentDir == Vector2.up && previousDir == Vector2.left)
                    {
                        turnPart = Instantiate(bodyTopRightPrefab, currentPosition, Quaternion.identity);
                    }
                    else if (currentDir == Vector2.down && previousDir == Vector2.right)
                    {
                        turnPart = Instantiate(bodyBottomLeftPrefab, currentPosition, Quaternion.identity);
                    }
                    else if (currentDir == Vector2.down && previousDir == Vector2.left)
                    {
                        turnPart = Instantiate(bodyBottomRightPrefab, currentPosition, Quaternion.identity);
                    }
                    else if (currentDir == Vector2.right && previousDir == Vector2.up)
                    {
                        turnPart = Instantiate(bodyBottomRightPrefab, currentPosition, Quaternion.identity);
                    }
                    else if (currentDir == Vector2.right && previousDir == Vector2.down)
                    {
                        turnPart = Instantiate(bodyTopRightPrefab, currentPosition, Quaternion.identity);
                    }
                    else if (currentDir == Vector2.left && previousDir == Vector2.up)
                    {
                        turnPart = Instantiate(bodyBottomLeftPrefab, currentPosition, Quaternion.identity);
                    }
                    else // (currentDir == Vector2.left && previousDir == Vector2.down)
                    {
                        turnPart = Instantiate(bodyTopLeftPrefab, currentPosition, Quaternion.identity);
                    }

                    turnPart.transform.parent = snakeParent;

                    // Add the corner
                    snake.Insert(1, turnPart);
                    snake[snake.Count - 1].transform.position = newTailPos;
                    // Set the direction of snake at this corner
                    turnPart.GetComponent<Corner>().setDirection(currentDir);

                }
                else // No
                {
                    // Is snake going up or down but second last is horizontal body
                    if ((currentDir == Vector2.up || currentDir == Vector2.down) &&
                        (secondLast.CompareTag("Horizontal") || secondLast.CompareTag("Corner"))) // Yes
                    { // Make it vertical
                        Destroy(secondLast);
                        secondLast = Instantiate(bodyVerticalPrefab, currentPosition, Quaternion.identity);
                        // And insert it at gap position
                        snake.Insert(1, secondLast);
                        snake[snake.Count - 1].transform.position = newTailPos;
                    }
                    // Is snake going left or right but second last is vertical body
                    else if ((currentDir == Vector2.right || currentDir == Vector2.left) &&
                        (secondLast.CompareTag("Vertical") || secondLast.CompareTag("Corner"))) // Yes
                    { // Make it horizontal
                        Destroy(secondLast);
                        secondLast = Instantiate(bodyHorizontalPrefab, currentPosition, Quaternion.identity);
                        // And insert it at gap position
                        snake.Insert(1, secondLast);
                        snake[snake.Count - 1].transform.position = newTailPos;
                    }

                    else
                    {
                        secondLast.transform.position = currentPosition;

                        // And insert it at gap position
                        snake.Insert(1, secondLast);

                        snake[snake.Count - 1].transform.position = newTailPos;
                    }

                    secondLast.transform.parent = snakeParent;
                }

            }

            previousDir = currentDir;
            canPress = true;
        }
    }
    
    /// <summary>
    /// Changes variable 'ate' to true when the snake eats food
    /// </summary>
    public void SetAte()
    {
        ate = true;
    }

    /// <summary>
    /// Check if the position of the food is not at the same position as the snake
    /// </summary>
    /// <param name="foodPosition"></param>
    /// <returns>True if position of food is not on the snake. Else false.</returns>
    public bool IsPositionFree(Vector2 foodPosition)
    {
        foreach(GameObject part in snake)
        {
            Vector2 partPositon = new Vector2(
                Mathf.RoundToInt(part.transform.position.x), Mathf.RoundToInt(part.transform.position.y));
            if (partPositon.Equals(foodPosition))
            {
                return false;
            }
        }

        return true;
    }

}
