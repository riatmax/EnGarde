using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    [SerializeField] private BoxCollider2D gridCollider;
    private Bounds gridBounds;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        gridBounds = gridCollider.bounds;
    }

    void Update()
    {
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;

        float clampedX = Mathf.Clamp(mouseWorld.x, gridBounds.min.x, gridBounds.max.x);
        float clampedY = Mathf.Clamp(mouseWorld.y, gridBounds.min.y, gridBounds.max.y);

        transform.position = new Vector2(clampedX, clampedY);
    }
}
