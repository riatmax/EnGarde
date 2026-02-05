using UnityEngine;

public class OpponentTiredState : IOpponentState
{
    private OpponentMovement opponent;

    public OpponentTiredState(OpponentMovement opponent)
    {
        this.opponent = opponent;
    }
    public void Exit()
    {
        
    }

    public void FixedUpdate()
    {
        
    }

    void IOpponentState.Enter()
    {
        opponent.StopMovement();
        opponent.anim.SetBool("Tired", true);
        opponent.isTired = true;
    }
}
