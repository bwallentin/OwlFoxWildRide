using UnityEngine;

public class LootState : IBattleState
{
    // The game controller
    private GameController gameController;

    // The state machine holding this state
    private BattleStateMachine bsm;

    // Keeps track of if we're done looting or not
    private bool doneLooting = false;

    public LootState(BattleStateMachine battleStateMachine)
    {
        bsm = battleStateMachine;

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    public void Update()
    {
        //TODO: Implement looting
        Debug.Log("Enter Loot State");
        doneLooting = true;

        if (doneLooting)
        {
            Debug.Log("Exit Loot State");
            gameController.ExitCombat();
        }
    }

    public void OnEnter(IAction executable)
    {
    }

    public void OnExit()
    {
        doneLooting = false;
    }
}
