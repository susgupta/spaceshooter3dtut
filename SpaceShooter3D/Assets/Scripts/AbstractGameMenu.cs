using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Abstract class that has common behaviour for game menu behaviour.
/// </summary> 
public abstract class AbstractGameMenu : MonoBehaviour {

    //reference to unity eventsystem
    [SerializeField]
    protected EventSystem uiEventSystem;

    //reference to menu selected object
    [SerializeField]
    protected GameObject selectedMenuObject;

    protected bool buttonSelected;

    void Update()
    {
        //an actual vertical key was pressed
        if (Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false)
        {
            if (uiEventSystem != null)
            {
                uiEventSystem.SetSelectedGameObject(selectedMenuObject);
                buttonSelected = true;
            }
        }
    }

    void OnDisable()
    {
        buttonSelected = false;
    }

    //public function that loads scene by index
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
