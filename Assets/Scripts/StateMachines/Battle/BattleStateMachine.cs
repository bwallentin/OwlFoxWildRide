using System.Collections.Generic;

public enum BattleState
{
    Turn = 0,
    Execute = 1,
    Loot = 3,
    Defeat = 4
}

public class BattleStateMachine
{
    private Dictionary<BattleState, IBattleState> states = new Dictionary<BattleState, IBattleState>();
    private IBattleState state;

    public void Add(BattleState battleState, IBattleState state)
    {
        if (!states.ContainsKey(battleState))
        {
            states[battleState] = state;
        }
    }

    public void Update()
    {
        state.Update();
    }

    public void Change(BattleState next, IAction action = null)
    {
        // exit current state
        if (state != null)
        {
            state.OnExit();
        }

        // set new state
        state = states[next];

        state.OnEnter(action);

    }
}
