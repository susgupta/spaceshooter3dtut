using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour {

    //track internally whether UI elements are hidden
    bool isDisplayed = true;

    //reference to play button
    [SerializeField]
    GameObject playButton;

    void OnEnable()
    {
        //add delegate to subscribe
        EventManager.onStartGame += HidePanel;
    }

    void OnDisable()
    {
        //remove delegate to un0subscribe
        EventManager.onStartGame -= HidePanel;
    }

    //hide panel UI
    void HidePanel()
    {
        //simple toggle
        isDisplayed = !isDisplayed;
        playButton.SetActive(isDisplayed);        
    }

	//to begin game
    public void PlayGame()
    {
        //use event manager to start game
        EventManager.StartGame();
    }
}
