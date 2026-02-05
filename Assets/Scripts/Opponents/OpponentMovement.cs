using UnityEngine;
public class OpponentMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerAvatar player;
    public bool isInCorner;

    public OpponentStateMachine StateMachine { get; private set; }

    [Header("Stamina Stat")]
    public float stam = 50f;

    [Header("States")]
    public OpponentSpacingState SpacingState;
    public OpponentAttackState QuickLunge;

    [Header("Movement Settings")]
    public float maxSpeed = 10f;
    public float stoppingDistance = 0.05f;
    public float decelerationArea = 0.5f;
    public float distFromPlayer = 2.5f;

    [Header("Boundaries")]
    public Collider2D rightCollider;
    public Collider2D leftCollider;
    public float rightBound;
    public float leftBound;

    [Header("Animation")]
    public Animator anim;
    public AnimationClip[] animations;

    [Header("Components")]
    public GameManager gm;

    [Header("Attack Settings")]
    public float attackCooldown = 3f;
    private float attackTimer;

    [Header("Grid Bounds")]
    public BoxCollider2D grid;
    private Bounds gridBounds;
    private GameObject attackStart;

    [Header("Attacks")]
    public GameObject attackPrefab;
    public GameObject hitBoxGO;
    public hitBox hitBox;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<PlayerAvatar>();
        gm = FindFirstObjectByType<GameManager>();

        rightBound = rightCollider.bounds.min.x;
        leftBound = leftCollider.bounds.max.x;

        StateMachine = new OpponentStateMachine();

        gridBounds = grid.bounds;
        attackStart = GameObject.FindWithTag("AttackStart");

        hitBox.enabled = false;

        SpacingState = new OpponentSpacingState(this);
        QuickLunge = new OpponentAttackState(this, animations[0]);
    }

    private void Start()
    {
        StateMachine.ChangeState(SpacingState);
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (!gm.introDone || player == null) return;

        StateMachine.FixedUpdate();
    }

    // ===== Helpers used by states =====

    public Rigidbody2D RB => rb;
    public PlayerAvatar Player => player;

    public void UpdateAnimation()
    {
        float deadZone = .001f;
        float vx = rb.linearVelocity.x;

        int animDir = Mathf.Abs(vx) < deadZone ? 0 : (int)Mathf.Sign(vx);
        anim.SetInteger("Velocity", animDir);
    }

    public void FacePlayerY()
    {
        transform.position = new Vector2(transform.position.x, player.transform.position.y);
    }

    public void StopMovement()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    public bool CanAttack()
    {
        if (attackTimer > 0) return false;

        float dist = Mathf.Abs(Player.transform.position.x - transform.position.x);

        if (dist > distFromPlayer + 0.3f) return false;

        return true;
    }

    public void ResetAttackCooldown()
    {
        attackTimer = attackCooldown;
    }

    public void SpawnAttack()
    {
        float randY;
        randY = Random.Range(gridBounds.min.y, gridBounds.max.y);
        Instantiate(attackPrefab, new Vector2(attackStart.transform.position.x, randY), Quaternion.identity);
    }
    public void ActivateHitbox ()
    {
        hitBox.enabled = true;
    }
    public void DeactivateHitbox()
    {
        hitBox.enabled = false;
    }
}

    /*private Rigidbody2D rb;
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

    [Header("Animation")]
    [SerializeField] protected Animator anim;

    [Header("Components")]
    [SerializeField] protected GameManager gm;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<PlayerAvatar>();
        gm = FindFirstObjectByType<GameManager>();
        rightBound = rightCollider.bounds.min.x;
        leftBound = leftCollider.bounds.max.x;
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        if (gm.introDone)
        {
            float myX = rb.position.x;
            float playerX = player.transform.position.x;
            float directionToPlayer = Mathf.Sign(playerX - myX);
            float currentDist = Mathf.Abs(playerX - myX);

            // 1. Calculate the ideal target
            float targetX = playerX - (directionToPlayer * distFromPlayer);

            // 2. The Decision Tree
            if (isInCorner)
            {
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

            gameObject.transform.position = new Vector2(gameObject.transform.position.x, player.transform.position.y);
            UpdateAnimation();
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

    private void UpdateAnimation()
    {
        float deadZone = .001f;

        float vx = rb.linearVelocity.x;

        int animDir = Mathf.Abs(vx) < deadZone ? 0 : (int)Mathf.Sign(vx);

        anim.SetInteger("Velocity", animDir);
    }*/
