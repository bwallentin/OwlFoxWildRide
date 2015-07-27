using System.Collections.Generic;

public enum GameState
{
    Roaming = 0,
    Combat = 1
}

public class GameStateMachine {

    private Dictionary<GameState, IState> states = new Dictionary<GameState, IState>();
    private IState state;

    public void Add(GameState gameState, IState state)
    {
        if (!states.ContainsKey(gameState))
        {
            states[gameState] = state;
        }
    }

    public void Update()
    {
        state.Update();
    }

    public void Change(GameState next)
    {
        // exit current state
        if(state != null)
        {
            state.OnExit();
        }

        // set new state
        state = states[next];
        state.OnEnter();
    }
}
