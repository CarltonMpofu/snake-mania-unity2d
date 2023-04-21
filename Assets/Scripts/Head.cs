using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Head : MonoBehaviour
{
    Score score;

    SnakeAbilities snakeAbilities;

    SoundManager soundManager;

    //Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        score = FindObjectOfType<Score>();
        if (!score)
        {
            Debug.LogError("NO SCORE FOUND");
        }

        snakeAbilities = FindObjectOfType<SnakeAbilities>();
        if(!snakeAbilities)
        {
            Debug.LogError("NO SNAKE ABILITIES FOUND");
        }

        soundManager = FindObjectOfType<SoundManager>();
        if(!soundManager)
            Debug.LogError("NO SOUND MANAGER FOUND");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Apple"))
        {
            soundManager.PlayEatSound();
            UpdatePoints(other, snakeAbilities.GetAppleScore());
        }
        else if (other.gameObject.CompareTag("Pink"))
        {
            soundManager.PlayEatSound();
            UpdatePoints(other, snakeAbilities.GetPinkyScore());
        }
        else if (other.gameObject.CompareTag("Grape"))
        {
            soundManager.PlayEatSound();
            UpdatePoints(other, snakeAbilities.GetGrapeScore());
        }
        else if(other.gameObject.CompareTag("LargeApple"))
        {
            soundManager.PlayEatSound();
            UpdatePoints(other, snakeAbilities.GetLargeScore());
        }
        else if(other.gameObject.CompareTag("ChainFood"))
        {
            soundManager.PlayEatSound();
            UpdatePoints(other, snakeAbilities.GetChainFoodScore());
            // Snake snake = FindObjectOfType<Snake>();
            // snake.SetAte();
            // float scoreVal = snakeAbilities.GetChainFoodScore();
            // //Debug.Log($"The score value is {scoreVal}");
            // score.UpdateScore((int)Mathf.Floor(scoreVal));
            // Destroy(other.gameObject);

            // Player ate food in time. Spawn it again 
            // Keeps on spawning if player eats chainFood before timer runs out.
            snakeAbilities.ReSpawnChainFood();
        }
        else // Head collided with snake body part or walls
        { // Game Over

            GameManager gameManager = FindObjectOfType<GameManager>();
            // update total points and save
            gameManager.AddPoints(score.getScore());
            gameManager.SetCurrentScore(score.getScore());
            gameManager.SetPreviousSceneIndex(SceneManager.GetActiveScene().buildIndex);
            DataPersistenceManager.instance.SaveGame();
            
            // Load next scene
            FindObjectOfType<SceneLoader>().LoadGameOverScene();
        }
    }

    /// <summary>
    /// Updates the players score/points
    /// </summary>
    /// <param name="other"></param>
    /// <param name="pointsAmount"></param>
    private void UpdatePoints(Collider2D other, int pointsAmount)
    {
        Snake snake = FindObjectOfType<Snake>();
        snake.SetAte();
        int foodValue = pointsAmount;
        score.UpdateScore(foodValue);
        Destroy(other.gameObject);
        snake.IncreaseSnakeSpeed();
    }
}
