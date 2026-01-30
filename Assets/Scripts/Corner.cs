using UnityEngine;

public class Corner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        if (collided.GetComponent<OpponentMovement>() != null)
        {
            collided.GetComponent<OpponentMovement>().isInCorner = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        if (collided.GetComponent<OpponentMovement>() != null)
        {
            collided.GetComponent<OpponentMovement>().isInCorner = false;
        }
    }
}
