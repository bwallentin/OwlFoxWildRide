public interface IBattleState
{
    // Runs every frame we're in this state
    void Update();

    // Initiator for when we enter a state
    void OnEnter(IAction action = null);

    // Leaving the state, let's clean up
    void OnExit();
}
