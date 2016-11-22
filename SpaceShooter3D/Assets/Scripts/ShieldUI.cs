using UnityEngine;
using System.Collections;

public class ShieldUI : MonoBehaviour {

    //reference to main health bar
    [SerializeField]
    RectTransform barRectTransform;

    //hold max width
    float maxWidth;

    void OnEnable()
    {
        //add delegate to subscribe
        EventManager.onHandleDamage += UpdateShieldDisplay;
    }

    void OnDisable()
    {
        //remove delegate to un-subscribe
        EventManager.onHandleDamage -= UpdateShieldDisplay;
    }

    void Awake()
    {
        maxWidth = barRectTransform.rect.width;
    }

    void UpdateShieldDisplay(float percentage)
    {
        barRectTransform.sizeDelta = new Vector2(maxWidth * percentage, 10.0f);
    }
}
