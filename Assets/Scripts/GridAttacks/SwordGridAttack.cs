using System.Collections;
using UnityEngine;

public class SwordGridAttack : MonoBehaviour
{
    public GameObject attackPoint;
    public GameObject playerCursor;
    public OpponentMovement opp;
    public float stamDam = 10;


    private bool isCursorInside = false;

    private void Start()
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
        Debug.Log("<color=green>PARRY SUCCESS!</color>");

        // 1. Damage stamina
        opp.currStam -= stamDam;

        // 2. IMPORTANT: Force the opponent out of the Attack State immediately
        // This stops the AttackState logic from running its next Update
        opp.StateMachine.ChangeState(opp.SpacingState);

        // 3. IMPORTANT: Tell the hitbox to stop everything RIGHT NOW
        opp.DeactivateHitbox();

        // 4. Force the animator to stop the attack animation so no more events fire
        opp.anim.Play("Idle", 0, 0f);

        Destroy(gameObject);
    }

    public void moveToPoint()
    {
        transform.position = new Vector2(attackPoint.transform.position.x, transform.position.y);
        StartCoroutine(destroy());
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

    private IEnumerator destroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}