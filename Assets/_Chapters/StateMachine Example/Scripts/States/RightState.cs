using UnityEngine;
public class RightState : BaseState
{
    public override void Enter()
    {
        Debug.Log("I'm RIGHT now!");
    }

    public override void Update()
    {
        // state update logic
    }

    public override void Exit()
    {
        // Exit state logic
    }
}