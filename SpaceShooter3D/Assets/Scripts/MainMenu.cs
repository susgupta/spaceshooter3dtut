using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class MainMenu : AbstractGameMenu {
            
    //public function for quit
    public void Quit()
    {
#if UNITY_EDITOR
        //if in unity, stop playing in editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //actual build mode
        Application.Quit();
#endif
    }        
}
