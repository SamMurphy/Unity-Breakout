using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public int StartingScore = 0;
    public int StartingLives = 3;

    public static int score;        // The player's score.
    public static int lives;        // The player's lives.
    public Text text;               // Reference to the Text component.

    void Start()
    {
        // Reset the score.
        score = StartingScore;
        lives = StartingLives;
    }


    void Update()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
        text.text = "    Score - " + score;
    }

    public static void GameOver()
    {

    }
}
