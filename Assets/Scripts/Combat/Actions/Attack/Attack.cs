using UnityEngine;

public class Attack : IAction {

    // The Actor attacking
    public GameObject Actor { get; private set; }

    // The Target being attacked
    public GameObject Target { get; private set; }

    // The time it takes to execute this action
    public float ExecutionTime { get; private set; }

    private float executionTime = 1f;

    public Attack(GameObject actor, GameObject target)
    {
        Actor = actor;
        Target = target;
        ExecutionTime = executionTime;
    }
}
