using UnityEngine;

public class hitBox : MonoBehaviour
{
    [Header("Status")]
    public bool collidedWithPlayer = false; // For visual effects
    public bool canCollide = false;         // Controlled by animation/state

    private bool hasScoredThisSwing = false;
    private GameManager gm;
    public OpponentMovement opponent; // Reference to the main script

    private void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
        if (opponent == null) opponent = GetComponentInParent<OpponentMovement>();
    }

    private void Update()
    {
        // If the state machine or animation turns canCollide off,
        // we MUST immediately clear the hit status.
        if (!canCollide)
        {
            collidedWithPlayer = false;
            hasScoredThisSwing = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // 1. MUST be the player
        // 2. MUST be during an active attack frame (canCollide)
        // 3. MUST be while the opponent is actually in the Attack State (Safety check)
        if (collision.CompareTag("PlayerAvatar") && canCollide && opponent.attacking)
        {
            collidedWithPlayer = true;

            // ONLY score once per swing
            if (!hasScoredThisSwing)
            {
                hasScoredThisSwing = true; // Lock the gate
                ScoreCounter.Instance.oppScore++;
                gm.resetRound();

                Debug.Log("Hit! Score: " + ScoreCounter.Instance.oppScore);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAvatar"))
        {
            collidedWithPlayer = false;
        }
    }
}