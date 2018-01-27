using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private bool isAttaking = false;

    // ANIMATIONS
    Animator _animator;
    [SerializeField]
    Animation attackAnimation;

    // MOVEMENT
    private Vector3 mousePosition;
    [SerializeField]
    private float moveSpeed = 0.1f;
    Vector2 targetPosition;
    private bool isMoving = false;
    
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            //Debug.Log("Collision with enemey!");
            AIController collisionEnemy = coll.gameObject.GetComponent<AIController>();
            if (collisionEnemy.isTargetted)
            {
                animation["attackAnimation"].wrapMode = WrapMode.Once;
                animation.Play("attackAnimation");
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            targetPosition = Input.mousePosition;
            targetPosition = Camera.main.ScreenToWorldPoint(targetPosition);
            if (transform.position.x != targetPosition.x && transform.position.y != targetPosition.y)
            {
                isMoving = true;
                _animator.SetBool("isMoving", isMoving);
            }
        }

        if (isMoving)
        {
            transform.position = Vector2.Lerp(transform.position, targetPosition, moveSpeed);
        }

        if (transform.position.x == targetPosition.x && transform.position.y == targetPosition.y)
        {
            isMoving = false;
            _animator.SetBool("isMoving", isMoving);
        }
    }
}
