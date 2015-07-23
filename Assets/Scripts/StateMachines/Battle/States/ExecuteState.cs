using UnityEngine;

public class ExecuteState : IBattleState
{
    // The state machine holding this state
    private BattleStateMachine bsm;

    // The action to be executed
    private IAction action;

    // Keeps track of when we tried to start the execution
    private float executionStart;

    // Keeps track of if we've started the action or not
    private bool inProgress = false;

    public ExecuteState(BattleStateMachine battleStateMachine)
    {
        bsm = battleStateMachine;
    }

    public void Update()
    {
        // If the execution time of this action has passed,
        // e.g animations and everything is done, we'll execute the next turn
        if (Time.time >= (executionStart + action.ExecutionTime))
        {
            bsm.Change(BattleState.Turn);
            return;
        }

        if(!inProgress)
        {
            inProgress = true;

            // TODO: Start attack animation for actor
            //Animator actorAnimator = action.Actor.GetComponent<Animator>();
            
            // TODO: Fix so the target takes damage somwwhere in the animation
            IHealth<int> targetHealth = action.Target.GetComponent<IHealth<int>>();
            targetHealth.TakeDamage(20);
        }
    }

    public void OnEnter(IAction executable)
    {
        action = executable;
        
        // Time the Execution began
        executionStart = Time.time;  
    }

    public void OnExit()
    {
        inProgress = false;
    }
}
