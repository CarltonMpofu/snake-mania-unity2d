using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joypad : MonoBehaviour
{
    Snake snake;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        snake = FindObjectOfType<Snake>();
        if(!snake)
        {
            // Debug.LogError("NO SNAKE COMPONENT FOUND");
        }

        gameManager = FindObjectOfType<GameManager>();
        
        if (CompareTag("Up"))
        {
            // Debug.Log("Get Up");
            Vector2 padPosition = gameManager.GetTopPadPositon();
            Vector3 position = new Vector3(padPosition.x, padPosition.y, 0);
            transform.position = position;
            Vector2 localPosition = transform.localPosition;
            transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0);
        }
        else if (CompareTag("Down"))
        {
            Vector2 padPosition = gameManager.GetBottomPadPositon();
            Vector3 position = new Vector3(padPosition.x, padPosition.y, 0);
            transform.position = position;
            Vector2 localPosition = transform.localPosition;
            transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0);
        }
        else if (CompareTag("Left"))
        {
            Vector2 padPosition = gameManager.GetLeftPadPositon();
            Vector3 position = new Vector3(padPosition.x, padPosition.y, 0);
            transform.position = position;
            Vector2 localPosition = transform.localPosition;
            transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0);

        }

        else if (CompareTag("Right"))
        {
            Vector2 padPosition = gameManager.GetRightPadPositon();
            Vector3 position = new Vector3(padPosition.x, padPosition.y, 0);
            transform.position = position;
            Vector2 localPosition = transform.localPosition;
            transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     // Debug.Log("Pressed");
        //     Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     Vector2 m = new Vector2(mousePosition.x, mousePosition.y);
        //     RaycastHit2D raycast = Physics2D.Raycast(m, Vector2.zero);
        //     if(raycast.collider)
        //     {
        //         Debug.Log(raycast.collider.gameObject.name);
        //     }

        // }
    }

    private void OnMouseDown() {
        if (gameManager.GetChangeDpadPosition() == false)
        {
            //Debug.Log(gameObject.name);
            if (gameObject.CompareTag("Left"))
            {
                snake.ChangeDirection(Vector2.right, Vector2.left);
                //Vibration.Vibrate(60);
            }
            else if (gameObject.CompareTag("Right"))
            {
                snake.ChangeDirection(Vector2.left, Vector2.right);
                //Vibration.Vibrate(60);
            }
            else if (gameObject.CompareTag("Up"))
            {
                snake.ChangeDirection(Vector2.down, Vector2.up);
                //Vibration.Vibrate(60);
            }
            else
            {
                snake.ChangeDirection(Vector2.up, Vector2.down);
                //Vibration.Vibrate(60);
            }
            //if(gameManager.IsVibrationOn())
                //Vibration.Vibrate(60);
        }
    }

    
}
