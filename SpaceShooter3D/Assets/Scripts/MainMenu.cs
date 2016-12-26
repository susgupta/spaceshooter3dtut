using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class MainMenu : AbstractGameMenu {

    //reference to unity eventsystem
    [SerializeField]
    EventSystem uiEventSystem;

    //reference to menu selected object
    [SerializeField]
    GameObject selectedMenuObject;

    private bool buttonSelected;
    
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
}
