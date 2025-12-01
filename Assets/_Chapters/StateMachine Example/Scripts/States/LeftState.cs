using UnityEngine;

public class LeftState : BaseState
{
    public override void Enter()
    {
        Debug.Log("I'm LEFT now!");
    }

    public override void Update()
    {
        //  state update logic
    }

    public override void Exit()
    {
        // Exit state logic
    }
}