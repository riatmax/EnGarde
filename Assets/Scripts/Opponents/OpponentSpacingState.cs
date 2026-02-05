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
        // Optional: Reset any specific animation triggers if needed
    }

    public void FixedUpdate()
    {
        opponent.FacePlayerY(); // Keep Y aligned (from your original helper)

        // 1. Determine which side of the player we are currently on
        // returns 1 if opponent is on right, -1 if on left
        float dirToMe = Mathf.Sign(opponent.transform.position.x - opponent.Player.transform.position.x);

        // 2. Calculate the "Sweet Spot" (Target Position)
        // This keeps us 'distFromPlayer' away, but on the correct side
        float targetX = opponent.Player.transform.position.x + (dirToMe * opponent.distFromPlayer);

        // 3. Move towards that target
        MoveTowards(targetX);

        // 4. Update Animator based on our new velocity
        opponent.UpdateAnimation();

        // 5. Check if we should attack
        if (opponent.CanAttack())
        {
            opponent.StateMachine.ChangeState(opponent.QuickLunge);
        }
    }

    private void MoveTowards(float targetX)
    {
        // Calculate distance to the target point
        float distToTarget = Mathf.Abs(targetX - opponent.transform.position.x);

        // Determine move direction (-1 for left, 1 for right)
        float moveDir = Mathf.Sign(targetX - opponent.transform.position.x);

        float newSpeed = 0;

        if (distToTarget > opponent.stoppingDistance)
        {
            // If we are far away, move at MaxSpeed
            // If we are close (inside decelerationArea), slow down smoothly
            if (distToTarget < opponent.decelerationArea)
            {
                float t = distToTarget / opponent.decelerationArea;
                newSpeed = Mathf.Lerp(opponent.maxSpeed * 0.1f, opponent.maxSpeed, t);
            }
            else
            {
                newSpeed = opponent.maxSpeed;
            }
        }
        else
        {
            // We are at the sweet spot, stop.
            newSpeed = 0;
        }

        // Apply Velocity
        opponent.RB.linearVelocity = new Vector2(moveDir * newSpeed, opponent.RB.linearVelocity.y);
    }

    public void Exit()
    {
        // Clean up if needed
    }
}