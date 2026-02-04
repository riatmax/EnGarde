using UnityEngine;

public class OpponentSpacingState : IOpponentState
{
    private OpponentMovement opponent;

    public OpponentSpacingState(OpponentMovement opponent)
    {
        this.opponent = opponent;
    }

    public void Enter()
    {
        // Optional: play walk animation
    }

    public void FixedUpdate()
    {
        Rigidbody2D rb = opponent.RB;
        PlayerAvatar player = opponent.Player;

        float myX = rb.position.x;
        float playerX = player.transform.position.x;

        float directionToPlayer = Mathf.Sign(playerX - myX);
        float currentDist = Mathf.Abs(playerX - myX);

        float targetX = playerX - (directionToPlayer * opponent.distFromPlayer);

        // CORNER LOGIC
        if (opponent.isInCorner)
        {
            if (currentDist > opponent.distFromPlayer)
            {
                MoveToX(targetX);
            }
            else
            {
                opponent.StopMovement();
            }
        }
        else
        {
            targetX = Mathf.Clamp(targetX, opponent.leftBound, opponent.rightBound);
            MoveToX(targetX);
        }

        opponent.FacePlayerY();
        opponent.UpdateAnimation();

        // ==== ATTACK TRANSITION ====
        if (opponent.CanAttack())
        {
            opponent.StateMachine.ChangeState(opponent.AttackState);
        }

    }

    public void Exit()
    {
        opponent.StopMovement();
    }

    private void MoveToX(float targetX)
    {
        Rigidbody2D rb = opponent.RB;

        float distance = targetX - rb.position.x;
        float absDist = Mathf.Abs(distance);

        if (absDist <= opponent.stoppingDistance)
        {
            opponent.StopMovement();
            return;
        }

        float speedScale = Mathf.Clamp01(absDist / opponent.decelerationArea);
        float velocityX = Mathf.Sign(distance) * opponent.maxSpeed * speedScale;

        rb.linearVelocity = new Vector2(velocityX, rb.linearVelocity.y);
    }
}
