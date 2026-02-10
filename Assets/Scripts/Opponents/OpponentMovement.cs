using UnityEngine;
using UnityEngine.UI;

public class OpponentMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerAvatar player;
    public bool isInCorner;

    public OpponentStateMachine StateMachine { get; private set; }

    [Header("Stamina Stat")]
    public float maxStam = 50f;
    public float currStam;
    public Image stamBar;

    [Header("States")]
    public OpponentSpacingState SpacingState;
    public OpponentAttackState QuickLunge;
    public OpponentTiredState TiredState;

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
    public bool attacking = false;
    public bool isTired = false;
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

        hitBox.canCollide = false;

        currStam = maxStam;

        SpacingState = new OpponentSpacingState(this);
        QuickLunge = new OpponentAttackState(this, animations[0]);
        TiredState = new OpponentTiredState(this);
    }

    private void Start()
    {
        StateMachine.ChangeState(SpacingState);
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;
        stamBar.fillAmount = currStam / maxStam;
        if (currStam <= 0)
        {
            StateMachine.ChangeState(TiredState);
        }

        if (StateMachine.CurrentState != QuickLunge && hitBox.canCollide)
        {
            DeactivateHitbox();
        }
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
        hitBox.canCollide = true;
    }
    public void DeactivateHitbox()
    {
        if (hitBox != null)
        {
            hitBox.canCollide = false;
            // Force the physics check to fail immediately
            hitBox.collidedWithPlayer = false;
        }
    }
}

   