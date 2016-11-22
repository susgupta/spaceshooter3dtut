using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

    //reference to max health or max shields value
    [SerializeField]
    int maxHealth = 10;

    //reference to current health or current shields value
    [SerializeField]
    int currentHealth;

    //reference for regeneration rate
    [SerializeField]
    float regenerationRate = 2f;

    //reference for regeneration amount
    [SerializeField]
    int regenerationAmount = 1;

    void Start()
    {
        currentHealth = maxHealth;
        //begin process to set regeneration, using the rate
        InvokeRepeating("Regenerate", regenerationRate, regenerationRate);
    }

    //increase health/shields
    void Regenerate()
    {
        //begin restoration
        if (currentHealth < maxHealth)
        {
            currentHealth += regenerationAmount;
        }
        
        //ensure current health never goes above max health
        if (currentHealth > maxHealth){
            currentHealth = maxHealth;            
        }

        //use events to notify
        EventManager.HandleDamage(currentHealth / (float)maxHealth);
    }

    //method to take damage on shields
    public void TakeDamage(int damageAmount = 1)
    {
        currentHealth -= damageAmount;

        //check to ensure have current health at least zero
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        //use events to notify
        EventManager.HandleDamage(currentHealth / (float)maxHealth);

        //if health is 0 or lower, invoke blow up on explosion
        if (currentHealth < 1)
        {
            Explosion explosion = GetComponent<Explosion>();
            explosion.BlowUp();
            //TODO - remove life from lives
        }
    }
}
