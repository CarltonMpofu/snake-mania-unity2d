using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDpadPosition : MonoBehaviour
{
    [SerializeField] float borderOffset = 1f;

    GameManager gameManager;
    DpadBorder[] dpadBorders;

    Vector2 dpadTopBorder;
    Vector2 dpadBottomBorder;
    Vector2 dpadLeftBorder;
    Vector2 dpadRightBorder;


    bool moveUp = false;
    bool moveDown = false;
    bool moveLeft = false;
    bool moveRight = false;

    private void Start() 
    {
        gameManager = FindObjectOfType<GameManager>();
        if(!gameManager){
            Debug.LogError("NO GAME MANAGER COMPONENT FOUND");
        }

        if(gameManager.GetChangeDpadPosition())
        {
            // Find borders
            dpadBorders = FindObjectsOfType<DpadBorder>();

            if(dpadBorders.Length==0)
                Debug.LogError("NO JOYPAD BORDERS FOUND");
            else // set-up border positions
            SetBorderPositions();
        }

    }
    private void Update() 
    {
        if(moveUp || moveDown || moveLeft || moveRight)
        { // Move the selected D-Pad Button position to the current mousePosition 

            // Get position of mouse where user clicked
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            // Move the current selected D-Pad button to mouse position
            // Stops when reaches borders
            float yPosition = Mathf.Clamp(mousePosition.y, 
            dpadBottomBorder.y + borderOffset, 
            dpadTopBorder.y - borderOffset);
            float xPosition = Mathf.Clamp(mousePosition.x, 
            dpadLeftBorder.x + borderOffset, 
            dpadRightBorder.x - borderOffset);

            // Set new positon of the D-pad button
            Vector2 dPadButtonPosition = new Vector2(xPosition, yPosition);
            transform.position = dPadButtonPosition;
        }
    }

    private void OnMouseDown() 
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        if(gameManager.GetChangeDpadPosition())
        {   // Change the position of the D-Pad Button
   
            if(CompareTag("Up"))
            { // Set as current selected button
                //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //transform.position = mousePosition;
                // Set as current selected button
                moveUp = true;
            }
            else if(CompareTag("Down"))
            { // Set as current selected button
                moveDown = true;
            }
            else if(CompareTag("Left"))
            { // Set as current selected button
                moveLeft = true;
            }

            else if(CompareTag("Right"))
            { // Set as current selected button
                moveRight = true;
            }
        }
    }


    private void OnMouseUp() {
        // No button selected
        moveUp = false;
        moveDown = false;
        moveLeft = false;
        moveRight = false;
    }

    private void SetBorderPositions()
    { // Get D-Pad border positions for easy access
        
        foreach(DpadBorder dPadBorder in dpadBorders)
        {
            if(dPadBorder.gameObject.CompareTag("PadBorderTop"))
                dpadTopBorder = dPadBorder.transform.position;
            else if(dPadBorder.gameObject.CompareTag("PadBorderBottom"))
                dpadBottomBorder = dPadBorder.transform.position;
            else if(dPadBorder.gameObject.CompareTag("PadBorderLeft"))
                dpadLeftBorder = dPadBorder.transform.position;
            else
                dpadRightBorder = dPadBorder.transform.position;
        }
    }
}
