using UnityEngine;

public class OpponentAttackState : IOpponentState
{
    private OpponentMovement opponent;
    private float attackTimer;

    private float attackDuration = 0.5f;

    public OpponentAttackState(OpponentMovement opponent)
    {
        this.opponent = opponent;
    }

    public void Enter()
    {
        attackTimer = attackDuration;

        opponent.StopMovement();

        // Trigger animation
        opponent.anim.SetTrigger("Attack");
    }

    public void FixedUpdate()
    {
        attackTimer -= Time.fixedDeltaTime;

        if (attackTimer <= 0f)
        {
            opponent.StateMachine.ChangeState(opponent.SpacingState);
        }
    }

    public void Exit()
    {
    }
}