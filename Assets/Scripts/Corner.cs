using UnityEngine;

public class Corner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        if (collided.GetComponent<OpponentAvatar>() != null)
        {
            collided.GetComponent<OpponentAvatar>().isInCorner = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        if (collided.GetComponent<OpponentAvatar>() != null)
        {
            collided.GetComponent<OpponentAvatar>().isInCorner = false;
        }
    }
}
