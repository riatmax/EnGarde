using UnityEngine;

public class OpponentAttackState : IOpponentState
{
    private OpponentMovement opponent;
    private float attackTime = 0;
    private AnimationClip animation;


    public OpponentAttackState(OpponentMovement opponent, AnimationClip ani)
    {
        this.opponent = opponent;
        this.animation = ani;
    }

    public void Enter()
    {
        opponent.attacking = true;
        attackTime = 0;

        opponent.StopMovement();

        opponent.anim.SetTrigger("Attack");

        opponent.ResetAttackCooldown();
    }

    public void FixedUpdate()
    {
        attackTime += Time.fixedDeltaTime;

        if (attackTime >= animation.length)
        {
            opponent.StateMachine.ChangeState(opponent.SpacingState);
        }
    }

    public void Exit()
    {
        opponent.attacking = false;
        opponent.DeactivateHitbox();
    }
}