using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

    //reference for highscore text
    [SerializeField]
    Text highScoreText;

    //reference for score text
    [SerializeField]
    Text scoreText;

    //track in inspector
    [SerializeField]
    int score;
    [SerializeField]
    int highScore;

    void Start()
    {
        LoadHighScore();
    }

    //subscribe to events
    void OnEnable()
    {
        EventManager.onStartGame += ResetScore;
        EventManager.onPlayerDeath += CheckNewHighScore;
        EventManager.onScorePoints += AddScore;
    }

    //un-subscribe to events
    void OnDisable()
    {
        EventManager.onStartGame -= ResetScore;
        EventManager.onPlayerDeath -= CheckNewHighScore;
        EventManager.onScorePoints -= AddScore;
    }

    //reset the score
    void ResetScore()
    {
        score = 0;
        DisplayScore();
    }

    //add to score
    void AddScore(int amount)
    {
        score += amount;
    }

    //update the score display
    void DisplayScore()
    {
        scoreText.text = score.ToString();
    }

    //load high score
    void LoadHighScore()
    {
        //get from player prefs
        highScore = PlayerPrefs.GetInt("highScore", 0);
        DisplayHighScore();
    }

    //check for new value for high score and replace
    void CheckNewHighScore()
    {
        if (score > highScore)
        {
            //set in player prefs
            PlayerPrefs.SetInt("highScore", score);
            DisplayHighScore();
        }
    }

    //update display for high score
    void DisplayHighScore()
    {
        highScoreText.text = highScore.ToString();
    }
}
