using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    [SerializeField] float swipeDistance = 0.1f;
    bool canSwipe = false;
    Vector2 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        Snake snake = FindObjectOfType<Snake>();
        if(snake)
        {
            if(canSwipe == false && Input.GetMouseButton(0))
            {
                startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                canSwipe = true;
            }

            if(canSwipe == true && Input.GetMouseButtonUp(0))
            {
                canSwipe = false;
            }

            if(snake.GetCurrentDirection() == Vector2.right || snake.GetCurrentDirection() == Vector2.left)
            {
                if(canSwipe)
                {
                    Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if(currentPosition.y >= startPosition.y + swipeDistance)
                    {
                        Debug.Log("Swipe up");
                        canSwipe = false;
                        snake.ChangeDirection(Vector2.down, Vector2.up);
                    }
                    else if(currentPosition.y <= startPosition.y - swipeDistance)
                    {
                        Debug.Log("Swipe down");
                        canSwipe = false;
                        snake.ChangeDirection(Vector2.up, Vector2.down);
                    }
                }
            }

            if (snake.GetCurrentDirection() == Vector2.down || snake.GetCurrentDirection() == Vector2.up)
            {
                if (canSwipe)
                {
                    Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (currentPosition.x >= startPosition.x + swipeDistance)
                    {
                        Debug.Log("Swipe right");
                        canSwipe = false;
                        snake.ChangeDirection(Vector2.left, Vector2.right);
                    }
                    else if (currentPosition.x <= startPosition.x - swipeDistance)
                    {
                        Debug.Log("Swipe left");
                        canSwipe = false;
                        snake.ChangeDirection(Vector2.right, Vector2.left);
                    }
                }
            }
        }
        else
        {
            Debug.LogError("No snake found");
        } 
    }
}
