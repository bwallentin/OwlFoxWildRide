public interface IDecision
{
    bool IsReady { get; }
    IAction Action { get; }

    void Update();
}
