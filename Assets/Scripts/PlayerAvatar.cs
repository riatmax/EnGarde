using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : MonoBehaviour
{
    public GameObject oppAv;
    public Animator ani;
    public float distChars;
    public bool closing = true;
    public Vector2 MoveRange;
    public float MoveTarget;
    void Start()
    {
        //Advance();
        SetTarget();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, new Vector3(MoveTarget, transform.position.y, 0)) < .01f)
        {
            SetTarget();
        }
        /*distChars = oppAv.transform.position.x - transform.position.x;
        if (transform.position.x <= -4.6)
        {
            closing = false;
        }
        if (transform.position.x >= 0)
        {
            closing = true;
        }*/
    }

    void Advance()
    {
        ani.Play("PlayerAdvance");
        transform.position += new Vector3(.5f, 0, 0);
    }
    void Lunge()
    {
        ani.Play("PlayerLunge");
        transform.position += new Vector3(1, 0, 0);
    }
    void Retreat()
    {
        ani.Play("PlayerRetreat");
        transform.position -= new Vector3(.5f, 0, 0);
    }
    void Parry()
    {
        ani.Play("PlayerParry");
    }
    public void CloseDist()
    {
        if (MoveTarget < transform.position.x)
        {
            Retreat();
        }
        else if (MoveTarget > transform.position.x)
        {
            Advance();
        }
        /*if (closing)
        {
            if (distChars > 1)
            {
                Advance();
            }
            else if (distChars < 1)
            {
                Retreat();
            }
        }
        else
        {
            Advance();
        }*/
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector3(MoveRange.x, transform.position.y, 0), new Vector3(MoveRange.y, transform.position.y, 0));
    }
    private void SetTarget()
    {
        MoveTarget = Random.Range(MoveRange.x, MoveRange.y);
    }
}
