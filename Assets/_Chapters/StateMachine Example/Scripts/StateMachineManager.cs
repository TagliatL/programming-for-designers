using UnityEngine;

public class StateMachineManager : MonoBehaviour
{
    private BaseState currentState;

    private void Start()
    {
        // Initialize the state machine with the idle state (when we are not pressing anything)
        currentState = new IdleState();
        currentState.Enter();
    }

    private void Update()
    {
        // Update the current state
        currentState.Update();
    }

    public void ChangeState(BaseState newState)
    {
        // Exit the current state
        currentState.Exit();

        // Assign the new state and enter it
        currentState = newState;
        currentState.Enter();
    }
}