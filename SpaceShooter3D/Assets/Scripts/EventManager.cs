using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {
    
    //delegate definition
	public delegate void StartGameDelegate();
    public delegate void HandleDamageDelegate(float amount);
    public delegate void PlayerDeathDelegate();
    public delegate void ScorePointsDelegate(int amount);

    //accessors
    public static StartGameDelegate onStartGame;
    public static HandleDamageDelegate onHandleDamage;
    public static PlayerDeathDelegate onPlayerDeath;
    public static ScorePointsDelegate onScorePoints;

    //methods
    public static void StartGame()
    {
        Debug.Log("Start the game");
        if (onStartGame != null)
        {
            onStartGame();
        }
    }

    public static void HandleDamage(float percentAmount)
    {
        Debug.Log("Handling Damage : " + percentAmount);
        if (onHandleDamage != null)
        {
            onHandleDamage(percentAmount);
        }
    }

    public static void PlayerDeath()
    {
        Debug.Log("Player has died! Oh Noes!");
        if (onPlayerDeath != null)
        {
            onPlayerDeath();
        }
    }

    public static void ScorePoints(int score)
    {
        Debug.Log("Score Points value : " + score);
        if (onScorePoints != null)
        {
            onScorePoints(score);
        }
    }
}
