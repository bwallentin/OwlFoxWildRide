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

        // Default action to Attack on the first target avaiable
        Action = new Attack(actor, targets[0]);

        IsReady = true;
    }
}
