using UnityEngine;

public abstract class BaseState
{
    public virtual void Enter()
    {
        Debug.Log("THIS IS BASE BEHAVIOUR");
        // Enter idle state logic
    }

    public virtual void Update()
    {
        // Idle state update logic
    }

    public virtual void Exit()
    {
        // Exit idle state logic
    }
}
