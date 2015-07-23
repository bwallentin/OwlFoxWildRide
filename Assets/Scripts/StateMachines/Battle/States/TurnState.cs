using UnityEngine;
using System.Collections.Generic;

public class TurnState : IBattleState {

    // The state machine holding this state
    private BattleStateMachine bsm;

    // The combatants engaged in combat
    private List<GameObject> combatants;

    // The current decision made by an actor
    private IDecision decision;

    // The current actor holding the turn
    private GameObject actor;

    // The current targets
    private List<GameObject> targets = new List<GameObject>();

    // Determines if we're waiting for an action to be decided
    private bool waitingForAction = false;

    public TurnState(BattleStateMachine battleStateMachine, List<GameObject> actors)
    {
        bsm = battleStateMachine;
        combatants = actors;
    }

    private T NextOf<T>(IList<T> list, T item)
    {
        return list[(list.IndexOf(item) + 1) == list.Count ? 0 : (list.IndexOf(item) + 1)];
    }

    private void FindViableTargets(string group)
    {
        targets.Clear();

        combatants.ForEach(entity => {

            // If the entity isn't in our group, add them to targets
            if (entity.tag != group)
            {
                IHealth<int> entityHealth = entity.GetComponent<IHealth<int>>();

                if(!entityHealth.IsDead)
                {
                    targets.Add(entity);
                }
            }
        });
        

        if(targets.Count == 0)
        {
            bsm.Change(BattleState.Loot);
        }
    }

    public void Update()
    {
        if (!waitingForAction) 
        {
            // Determine who's turn it is
            actor = NextOf(combatants, actor);
            IHealth<int> actorHealth = actor.GetComponent<IHealth<int>>();

            FindViableTargets(actor.tag);

            // A dead actor can't make moves, so we'll simply return
            if (actorHealth.IsDead || targets.Count == 0)
            {
                return;
            }
     
            if (actor.tag == "Player")
            {
                decision = new PlayerDecision(actor, targets);
            }
            else
            {
                decision = new AIDecision(actor, targets);
            }

            waitingForAction = true;
        }

        if (decision.IsReady)
        {
            bsm.Change(BattleState.Execute, decision.Action);
        }
    }

    public void OnEnter(IAction action = null)
    {
    }

    public void OnExit()
    {
        waitingForAction = false;
    }
}
