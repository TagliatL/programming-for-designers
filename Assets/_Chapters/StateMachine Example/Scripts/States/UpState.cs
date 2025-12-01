using UnityEngine;

public class UpState : BaseState
{
    public override void Enter()
    {
        Debug.Log("I'm UPSTATE now!");
    }

    public override void Update()
    {
        //  state update logic
        Debug.Log("UPSTATE UPDATE!");
    }

    public override void Exit()
    {
        // Exit state logic
    }
}