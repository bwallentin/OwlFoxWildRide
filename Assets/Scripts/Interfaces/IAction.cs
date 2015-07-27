using UnityEngine;

public interface IAction
{
    GameObject Actor { get; }
    GameObject Target { get; }
    Ability Ability { get; }
    float ExecutionTime { get; }
}
