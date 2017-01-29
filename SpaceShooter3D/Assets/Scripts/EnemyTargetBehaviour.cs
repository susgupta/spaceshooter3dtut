using UnityEngine;
using System.Collections;

public class EnemyTargetBehaviour : MonoBehaviour {

    //reference to enemyTargetState
    private EnemyTargetState enemyTargetState;

	// Use this for initialization
	void Awake() {

        //randomly generate behaviour for targeting, since it's only 2 choices for now
        enemyTargetState = Random.value < .5 ? EnemyTargetState.AttackPlayer : EnemyTargetState.AttackBase;
    }
	
    //expose method to allow other components to read the enemy target state.
	public EnemyTargetState GetAssignedEnemyTarget()
    {
        return enemyTargetState;
    }
}
