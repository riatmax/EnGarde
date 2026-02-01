using UnityEngine;

public class OpponentMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerAvatar player;
    public bool isInCorner;

    [Header("Movement Settings")]
    [SerializeField] protected float maxSpeed = 10f;
    [SerializeField] protected float stoppingDistance = 0.05f; // "Close enough"
    [SerializeField] protected float decelerationArea = 0.5f; // Start slowing down here
    [SerializeField] protected float distFromPlayer = 2.5f;

    [Header("Boundaries")]
    [SerializeField] protected Collider2D rightCollider;
    [SerializeField] protected Collider2D leftCollider;
    protected float rightBound;
    protected float leftBound;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<PlayerAvatar>();
        rightBound = rightCollider.bounds.min.x;
        leftBound = leftCollider.bounds.max.x;
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        float myX = rb.position.x;
        float playerX = player.transform.position.x;
        float directionToPlayer = Mathf.Sign(playerX - myX);
        float currentDist = Mathf.Abs(playerX - myX);

        // 1. Calculate the ideal target
        float targetX = playerX - (directionToPlayer * distFromPlayer);

        // 2. The Decision Tree
        if (isInCorner)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, player.transform.position.y);
            // If we're in the corner, we ONLY move if the player is getting TOO FAR away.
            // If the player is crowding us (currentDist < distFromPlayer), we stay put.
            if (currentDist > distFromPlayer)
            {
                MoveToX(targetX);
            }
            else
            {
                // Stop the "motor" so we don't push the player back
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            }
        }
        else
        {
            // Not in corner? Standard spacing logic.
            // Clamp the target to our bounds just in case
            targetX = Mathf.Clamp(targetX, leftBound, rightBound);
            MoveToX(targetX);
        }
    }

    private void MoveToX(float targetX)
    {
        float distanceToTarget = targetX - rb.position.x;
        float absoluteDistance = Mathf.Abs(distanceToTarget);

        // 2. Stop if we are within the tiny dead-zone
        if (absoluteDistance <= stoppingDistance)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        // 3. Calculate Speed
        // If we're inside the decelerationArea, scale the speed down
        float speedScaling = Mathf.Clamp01(absoluteDistance / decelerationArea);
        float desiredVelocityX = Mathf.Sign(distanceToTarget) * maxSpeed * speedScaling;

        // 4. Apply Velocity
        rb.linearVelocity = new Vector2(desiredVelocityX, rb.linearVelocity.y);
    }
}