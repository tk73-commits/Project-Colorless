using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    // access Enemy class and EnemyStateMachine class from within each state
    protected Enemy enemy;
    protected EnemyStateMachine enemyStateMachine;

    // pass the data in whenever instantiating this script
    public EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine)
    {
        this.enemy = enemy;
        this.enemyStateMachine = enemyStateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType) { }
}
