using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour {
    
    //reference to UI objects
    [SerializeField]
    GameObject gameUI;

    [SerializeField]
    GameObject mainMenu;

    //reference to spawn player
    [SerializeField]
    GameObject playerPrefab;

    [SerializeField]
    GameObject playerStartPosition;

    void Start()
    {
        DelayMainMenuDisplay();
    }

    void OnEnable()
    {
        //add delegate to subscribe
        EventManager.onStartGame += ShowGameUI;
        EventManager.onPlayerDeath += ShowMainMenu;
    }

    void OnDisable()
    {
        //remove delegate to un0subscribe
        EventManager.onStartGame -= ShowGameUI;
        EventManager.onPlayerDeath -= ShowMainMenu;
    }

    //show main menu UI
    void ShowMainMenu()
    {
        //add delay to show particle effects
        Invoke("DelayMainMenuDisplay", Asteroid.destructionDelay * 2.5f);            
    }

    void DelayMainMenuDisplay()
    {
        mainMenu.SetActive(true);
        gameUI.SetActive(false);
    }

    //show game UI
    void ShowGameUI()
    {
        mainMenu.SetActive(false);
        gameUI.SetActive(true);

        //instantiate player
        Instantiate(playerPrefab, playerStartPosition.transform.position, playerStartPosition.transform.rotation);
    }
}
