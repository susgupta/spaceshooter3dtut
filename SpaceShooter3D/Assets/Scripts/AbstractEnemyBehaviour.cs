using UnityEngine;
using System.Collections;

public abstract class AbstractEnemyBehaviour : MonoBehaviour {

    //reference to target
    protected Transform target;

    protected bool FindTarget(string targetTag)
    {
        if (target == null)
        {
            GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
            //now check if player is not destroyed
            if (targetObject != null)
            {
                target = targetObject.transform;
            }
        }

        if (target == null)
        {
            return false;
        }

        return true;
    }

    //method will return tag string based attached target state
    //default will be player
    protected string GetTargetTagFromTargetState()
    {
        string targetTag = "Player";

        if (GetTargetState().Equals(EnemyTargetState.AttackBase))
        {
            targetTag = "Base";
        }

        Debug.Log("Targetting set to: " + targetTag);

        return targetTag;
    }

    //method will retrieve attached component enemytarget state
    //if attached is null, this will target player
    protected EnemyTargetState GetTargetState()
    {
        EnemyTargetBehaviour enemyTargetBehaviour = GetComponent<EnemyTargetBehaviour>();

        //default target state
        EnemyTargetState targetState = EnemyTargetState.AttackPlayer;

        //get from attached component
        if (enemyTargetBehaviour != null)
        {
            targetState = enemyTargetBehaviour.GetAssignedEnemyTarget();
        }

        return targetState;
    }
}
