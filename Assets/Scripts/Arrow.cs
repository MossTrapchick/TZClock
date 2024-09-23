using UnityEngine;

public class Arrow : MonoBehaviour
{
    Vector2 zeroVector;
    Camera cam;
    Transform player;
    private void Start()
    {
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        zeroVector = new Vector2(player.position.x, player.position.y + 2) - (Vector2)player.position;
    }
    private void OnMouseDown()
    {
        Clock.IsMovingArrows = true;
    }
    private void OnMouseUp()
    {
        Clock.IsMovingArrows = false;
    }
    private void OnMouseDrag()
    {
        float angle = Vector2.Angle(zeroVector, (Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)player.position);
        bool isRight = cam.ScreenToWorldPoint(Input.mousePosition).x > player.position.x;
        transform.localRotation = Quaternion.Euler(0f, 0f, (isRight) ? -angle : angle);
    }
}
