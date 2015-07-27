using UnityEngine;
using System.Collections.Generic;

public class AIDecision : IDecision
{
    // Determines if the decision has been made yet
    public bool IsReady { get; private set; }

    // The actual that will be performed
    public IAction Action { get; private set; }

    public AIDecision(GameObject actor, List<GameObject> targets)
    {
        // TODO: Add target picking for AI

        // TODO: Implement real abilities instead of using the first one
        AbilityList abilityList = actor.GetComponent<AbilityList>();
        Action = new Action(actor, targets[0], abilityList.abilities[0]);

        IsReady = true;
    }

    public void Update()
    {
    }
}
