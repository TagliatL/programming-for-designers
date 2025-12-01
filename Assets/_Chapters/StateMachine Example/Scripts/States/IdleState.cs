using UnityEngine;
public class IdleState : BaseState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        Debug.Log("I'm IDLING now!");
        // state update logic
    }

    public override void Exit()
    {
        // Exit state logic
    }
}