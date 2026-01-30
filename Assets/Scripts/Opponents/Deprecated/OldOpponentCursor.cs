using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class OldOpponentCursor : MonoBehaviour
{
    float x;
    float y;
    float randPace;

    Vector3 newCoords;

    bool parryable = false;
    bool tired = false;

    public int oppScore = 0;
    public int playScore = 0;
    int parries = 0;
    int quad;

    public Animator ani;
    public Animator oppAni;
    public Animator playAni;
    public Animator BGAni;

    public GameObject playerAv;
    public GameObject oppAv;
    public GameObject intro;
    public GameObject playerCursor;
    public GameObject pROW;
    public GameObject oROW;

    public TMP_Text score;


    private void Start()
    {

    }

    private void Update()
    {
        Vector3 m = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m.z = 0;
        transform.position = Vector3.Lerp(transform.position, newCoords, Time.deltaTime * randPace);
        if (parryable && Input.GetMouseButtonDown(0) && !tired)
        {
            if (m.x >= (transform.position.x - .5)
                 && !(m.x > transform.position.x + .5)
                 && (m.y >= transform.position.y - .5)
                 && !(m.y > transform.position.y + .5))
            {
                oppAv.transform.position = new Vector3(playerAv.transform.position.x - .5f, oppAv.transform.position.y, oppAv.transform.position.z);
                oppAni.Play("OppLunge");
                playAni.Play("PlayerParry");
                ani.Play("OppCursorIdle");
                parries++;

                Tired();

            }
        }
        if (!parryable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!tired)
                {
                    playerAv.transform.position = new Vector3(oppAv.transform.position.x + .5f, playerAv.transform.position.y, playerAv.transform.position.z);
                    playAni.Play("PlayerLunge");
                    oppAni.Play("OppParry");
                }
                else
                {
                    playerAv.transform.position = new Vector3(oppAv.transform.position.x + .4f, playerAv.transform.position.y, playerAv.transform.position.z);
                    playAni.Play("PlayerLunge");
                    PlayScore();
                }
            }
        }
    }
    private void ROWQuadMover()
    {
        quad = Random.Range(1, 5);
        if (quad == 1)
        {
            x = Random.Range(.7f, 5.25f);
            y = Random.Range(.35f, 2f);
            randPace = Random.Range(2f, 3f);

            newCoords = new Vector3(x, y, 0);

        }
        else if (quad == 2)
        {
            x = Random.Range(-4.93f, -.67f);
            y = Random.Range(.35f, 2f);
            randPace = Random.Range(2f, 3f);

            newCoords = new Vector3(x, y, 0);
        }
        else if (quad == 3)
        {
            x = Random.Range(-4.93f, -.67f);
            y = Random.Range(-1.52f, -2.8f);
            randPace = Random.Range(2f, 3f);

            newCoords = new Vector3(x, y, 0);
        }
        else if (quad == 4)
        {
            x = Random.Range(.7f, 5f);
            y = Random.Range(-1.52f, -2.8f);
            randPace = Random.Range(2f, 3f);

            newCoords = new Vector3(x, y, 0);
        }
        Debug.Log("Quadrant: " + quad + " x: " + x + " y: " + y);
    }

    private void Attacks()
    {
        int rand = Random.Range(0, 3);

        if (rand == 2)
        {
            SLunge();
        }
        else
        {
            ROWQuadMover();
        }
        Debug.Log(rand);
    }
    private void SLunge()
    {
        //ani.Play("OppCursorAttack");
        ROWQuadMover();
    }
    private void DLunge()
    {

    }
    private void DDLunge()
    {

    }
    private void Feint()
    {

    }
    private void Attacking()
    {
        ani.Play("OppCursorFlash");
        parryable = true;
    }
    public void PlayScore()
    {
        parryable = false;
        tired = false;
        parries = 0;
        playScore++;
        score.text = $"{playScore} - {oppScore}";
        playerAv.SetActive(false);
        oppAv.SetActive(false);
        gameObject.SetActive(false);
        playerCursor.SetActive(false);
        BGAni.Play("BGPlay");
    }
    public void OppScore()
    {
        parryable = false;
        tired = false;
        parries = 0;
        oppScore++;
        score.text = $"{playScore} - {oppScore}";
        playerAv.SetActive(false);
        oppAv.SetActive(false);
        gameObject.SetActive(false);
        playerCursor.SetActive(false);
        BGAni.Play("BGOpp");
    }
    public void ResetStage()
    {
        Debug.Log("check");
        playerAv.transform.position = new Vector3(-1.15f, -3.7f, playerAv.transform.position.z);
        oppAv.transform.position = new Vector3(1.15f, -3.7f, oppAv.transform.position.z);
        transform.position = new Vector3(.91f, .25f, transform.position.z);
        pROW.SetActive(false);
        oROW.SetActive(true);
        intro.SetActive(true);
    }
    private void Tired()
    {
        if (parries == 3)
        {
            pROW.SetActive(true);
            oROW.SetActive(false);
            oppAni.Play("OppTired");
            tired = true;
        }
    }
    private void ParryCheck()
    {
        parryable = false;
    }
}
