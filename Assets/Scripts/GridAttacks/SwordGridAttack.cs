using UnityEngine;

public class SwordGridAttack : MonoBehaviour
{
    public GameObject attackPoint;
    public GameObject playerCursor;
    public OpponentMovement opp;
    public float stamDam;


    private bool isCursorInside = false;

    private void Awake()
    {
        attackPoint = GameObject.FindWithTag("AttackPoint");
        playerCursor = GameObject.FindWithTag("PlayerCursor");
        opp = FindAnyObjectByType<OpponentMovement>();
    }

    private void Update()
    {
        // Check for the click in Update (most responsive)
        if (isCursorInside && Input.GetMouseButtonDown(0))
        {
            PerformParry();
        }
    }

    private void PerformParry()
    {
        opp.stam -= stamDam;
        Destroy(gameObject);
    }

    public void moveToPoint()
    {
        transform.position = new Vector2(attackPoint.transform.position.x, transform.position.y);
    }

    // Use these to just track the cursor's presence
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == playerCursor)
        {
            isCursorInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == playerCursor)
        {
            isCursorInside = false;
        }
    }
}