using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachineBehaviour
{
    private Enemy enemy;
    private EnemyHealth health;
    public Enemy GetEnemy(Animator animator)
    {
        enemy = animator.GetComponent<Enemy>();
        return enemy;
    }

    public EnemyHealth GetEnemyHealth(Animator animator)
    {
        health = animator.GetComponent<EnemyHealth>();
        return health;
    }
}
