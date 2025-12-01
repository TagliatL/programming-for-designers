using UnityEngine;

public class SphereController : MonoBehaviour
{
    private StateMachineManager stateMachine;

    private void Start()
    {
        stateMachine = GetComponent<StateMachineManager>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            stateMachine.ChangeState(new UpState());
        }
        else
        {
            stateMachine.ChangeState(new IdleState());
        }
    }
}