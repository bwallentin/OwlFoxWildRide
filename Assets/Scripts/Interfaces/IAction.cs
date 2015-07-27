using UnityEngine;

public interface IAction
{
    GameObject Actor { get; }
    GameObject Target { get; }
    float ExecutionTime { get; }
}
