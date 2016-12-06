using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

    //reference for GUI Text element for timer
    [SerializeField]
    Text timerText;

    //keep track of timePassed
    [SerializeField]
    float timePassed;

    //track when on or to keep time
    bool keepTime = false;

    //register events 
    void OnEnable()
    {
        EventManager.onStartGame += StartTimer;
        EventManager.onPlayerDeath += StopTimer; 
    }

    //un-register events
    void OnDisable()
    {
        EventManager.onStartGame -= StartTimer;
        EventManager.onPlayerDeath -= StopTimer;
    }

    void Update()
    {
        if (keepTime)
        {
            //simply update time passed from update of last frame and current one
            timePassed += Time.deltaTime;
            UpdateTimerDisplay();
        }        
    }

    //method to start timer
    void StartTimer()
    {
        keepTime = true;
        //reset time passed
        timePassed = 0;        
    }

    //method to stop timer
    void StopTimer()
    {
        keepTime = false;
    }

    //method to update timer display
    void UpdateTimerDisplay()
    {
        int minutes;
        float seconds;

        minutes = Mathf.FloorToInt(timePassed / 60);
        seconds = timePassed % 60;

        //update text component
        timerText.text = string.Format("{0}:{1:00.00}", minutes, seconds);
    }
}
