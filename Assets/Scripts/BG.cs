using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    public OpponentCursor OppCursor;

    public void ResetStage()
    {
        OppCursor.ResetStage();
    }
}
