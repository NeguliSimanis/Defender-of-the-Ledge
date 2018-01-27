using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    // MOVEMENT
    private Vector3 mousePosition;
    [SerializeField]
    private float moveSpeed = 0.1f;
    Vector2 targetPosition;
    private bool isMoving = false;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            targetPosition = Input.mousePosition;
            targetPosition = Camera.main.ScreenToWorldPoint(targetPosition);
            if (transform.position.x != targetPosition.x && transform.position.y != targetPosition.y)
            {
                isMoving = true;
            }
        }

        if (isMoving)
        {
            transform.position = Vector2.Lerp(transform.position, targetPosition, moveSpeed);
        }

        if (transform.position.x == targetPosition.x && transform.position.y == targetPosition.y)
        {
            isMoving = false;
        }
    }
}
