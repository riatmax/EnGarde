using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldOppAvatar : MonoBehaviour
{
    public Animator ani;
    public GameObject player;
    float distChars;
    public bool closing = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distChars = transform.position.x - player.transform.position.x;
        if (transform.position.x >= 5)
        {
            closing = false;
        }
    }

    void Advance()
    {
        ani.Play("OppAdvance");
        transform.position -= new Vector3(.5f, 0, 0);
    }
    void Lunge()
    {
        ani.Play("OppLunge");
        transform.position -= new Vector3(1, 0, 0);
    }
    void Retreat()
    {
        ani.Play("OppRetreat");
        transform.position += new Vector3(.5f, 0, 0);
    }
    void Parry()
    {
        ani.Play("OppParry");
    }
    public void CloseDist()
    {
        if (closing)
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
        }
       
    }
}
