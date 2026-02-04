using UnityEngine;

public interface IOpponentState
{
    void Enter();
    void FixedUpdate();
    void Exit();
}
public class OpponentStateMachine : MonoBehaviour
{
    public IOpponentState CurrentState { get; private set; }

    public void ChangeState(IOpponentState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void FixedUpdate()
    {
        CurrentState?.FixedUpdate();
    }
}

