using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Abstract class that has common behaviour for game menu behaviour.
/// </summary> 
public abstract class AbstractGameMenu : MonoBehaviour {

    //public function that loads scene by index
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
