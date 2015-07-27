public interface IState {

    // Runs every frame we're in this state
    void Update();

    // Initiator for when we enter a state
    void OnEnter();

    // Leaving the state, let's clean up
    void OnExit();
}
