using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public bool introDone = false;
    
    void Start()
    {
        anim.Play("Intro");
    }

    void Deactivate()
    {
        introDone = true;
    }
}
