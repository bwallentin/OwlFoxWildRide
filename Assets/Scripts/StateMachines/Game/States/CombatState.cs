using UnityEngine;
using System.Collections.Generic;

public class CombatState : IState
{
    // The state machine
    private BattleStateMachine bsm = new BattleStateMachine();
    
    // List of actors engaged in combat
    private List<GameObject> actors = new List<GameObject>();
    
    // Our singleton game controller
    private GameController gameController;

    public CombatState()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        bsm.Add(BattleState.Turn, new TurnState(bsm, actors));
        bsm.Add(BattleState.Execute, new ExecuteState(bsm));
        bsm.Add(BattleState.Loot, new LootState(bsm));
    }

    private void SetCombatReadiness(bool state)
    {
        actors.ForEach(delegate (GameObject entity)
        {
            // Set the health panel to active for all entities in the battle
            Actor actor = entity.GetComponent<Actor>();

            if (actor != null)
            {
                actor.SetHealthPanelActive(state);
            }

            // Disable the click to move input for our entities who are movable
            IMovable movement = entity.GetComponent<IMovable>();

            if (movement != null)
            {
                movement.IsMovable(!state);
            }
        });
    }

    public void Update()
    {
        bsm.Update();
    }

    public void OnEnter()
    {
        Debug.Log("Enter combat");
        // We'll take the combat groups the game controller have collected,
        // adding them to our own list of combatants
        foreach (KeyValuePair<CombatGroup, GameObject> combatant in gameController.combatGroups)
        {
            actors.Add(combatant.Value);
        }

        // Initation of combat
        SetCombatReadiness(true);

        // TODO: Sort actors by speed, so the fastest one will get the first turn

        // First turn
        bsm.Change(BattleState.Turn);

    }

    public void OnExit()
    {
        Debug.Log("Exit combat");

        SetCombatReadiness(false);

        // reset combatants
        actors = null;
    }
}
