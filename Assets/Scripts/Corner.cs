using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : MonoBehaviour
{

    Vector2 direction;

    /// <summary>
    /// Set the direction the snake is going in at this corner
    /// </summary>
    /// <param name="dir"> Vector2: The direction</param>
    public void setDirection(Vector2 dir)
    {
        direction = dir;
    }

    /// <summary>
    /// Get the direction the snake was going in at this corner
    /// </summary>
    /// <returns>Vector2: The direction</returns>
    public Vector2 GetDirection()
    {
        return direction;
    }
}
