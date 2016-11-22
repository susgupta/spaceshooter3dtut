using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {
    
    //delegate definition
	public delegate void StartGameDelegate();
    public delegate void HandleDamageDelegate(float amount);
    public delegate void PlayerDeathDelegate();

    //accessors
    public static StartGameDelegate onStartGame;
    public static HandleDamageDelegate onHandleDamage;
    public static PlayerDeathDelegate onPlayerDeath;

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
}
