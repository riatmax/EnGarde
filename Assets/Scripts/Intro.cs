using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public Animator ani;

    public GameObject player;
    public GameObject opp;
    public GameObject oppCursor;
    public GameObject playerCursor;
    // Start is called before the first frame update
    void Start()
    {
        ani.Play("Intro");
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Deactivate()
    {
        player.SetActive(true);
        opp.SetActive(true);
        oppCursor.SetActive(true);
        playerCursor.SetActive(true);
        gameObject.SetActive(false);
    }
}
