using UnityEngine;
using System.Collections.Generic;

public class PlayerDecision : IDecision {

    // Determines if the decision has been made yet
    public bool IsReady { get; private set; }

    // The actual that will be performed
    public IAction Action { get; private set; }

    public PlayerDecision(GameObject actor, List<GameObject> targets)
    {
        // Default action to Attack
        Action = new Attack(actor, targets[0]);
        
        // TODO: Show GUI for picking action, set isReady when it's done
        IsReady = true;
    }
}
