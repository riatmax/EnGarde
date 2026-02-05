using UnityEngine;

public class hitBox : MonoBehaviour
{
    public bool collidedWithPlayer = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerAvatar")
        {
            collidedWithPlayer = true;
            Debug.Log("Hit!");
        }
    }
}
