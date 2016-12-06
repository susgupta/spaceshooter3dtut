using UnityEngine;
using System.Collections;

public class PlayGameButton : MonoBehaviour {

    //simple event attach to button to start game
	public void PlayGame()
    {
        EventManager.StartGame();
    }
}
