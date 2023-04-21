using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateText();
    }

    public int getScore()
    {
        return score;
    }

    private void UpdateText()
    {
        // Display the current score of the user
        scoreText.text = score.ToString();
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        UpdateText();
    }
}
