using UnityEngine;
using System.Collections.Generic;


public enum CombatGroup
{
    PartyMember = 0,
    Enemy = 1
}

public class GameController : MonoBehaviour {

    // Sets the game state to in combat
    public bool inCombat = false;

    // We'll use combat groups to decide if an actor has joined the party members or the enemy
    public Dictionary<CombatGroup, GameObject> combatGroups = new Dictionary<CombatGroup, GameObject>();

    private GameStateMachine gsm;

    void Awake()
    {
        gsm = new GameStateMachine();

        gsm.Add(GameState.Roaming, new RoamingState());
        gsm.Add(GameState.Combat, new CombatState());

        gsm.Change(GameState.Roaming);
    }

    void Update()
    {
        // Of both parties have been added for combat, we're good to go
        if (combatGroups.ContainsKey(CombatGroup.PartyMember) && 
            combatGroups.ContainsKey(CombatGroup.Enemy) &&
            !inCombat)
        {
            InitiateCombat();
        }

        gsm.Update();
    }

    private void InitiateCombat()
    {
        inCombat = true;
        // Change State machine to combat
        gsm.Change(GameState.Combat);
    }

    public void ExitCombat()
    {
        inCombat = false;

        // Clear the combat groups
        combatGroups.Clear();

        // Change back to roaming state
        gsm.Change(GameState.Roaming);
    }

    public void AddCombatGroup(GameObject entity, CombatGroup combatGroup)
    {
        if (!combatGroups.ContainsKey(combatGroup))
        {
            combatGroups[combatGroup] = entity;
        }
    }
}
