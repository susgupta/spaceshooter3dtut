using UnityEngine;
using System.Collections;

public class DeployMenu : AbstractGameMenu {

    //simple event attach to button to start game
    public void PlayGame()
    {
        EventManager.StartGame();
    }
}
