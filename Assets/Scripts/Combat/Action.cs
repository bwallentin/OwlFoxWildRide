using UnityEngine;

public class Action : IAction
{
    // The Actor attacking
    public GameObject Actor { get; private set; }

    // The Target being attacked
    public GameObject Target { get; private set; }

    // The Ability we're performing
    public Ability Ability { get; private set; }

    // The time it takes to execute this action
    public float ExecutionTime { get; private set; }
    private float executionTime = 1f;

    public Action(GameObject actor, GameObject target, Ability ability)
    {
        Actor = actor;
        Target = target;
        Ability = ability;
        ExecutionTime = executionTime;
    }
}
