using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    PlayerData playerController;

    private bool isMoving = true;
    private float attackResetTime;
    private bool isAttackCooldown = false;

    #region Enemy properties
    [SerializeField]
    private int speed = 2;

    [SerializeField]
    private int damage = 15;

    [SerializeField]
    private float attackCooldown = 1f;
    #endregion

    void Start ()
    {
        playerController = player.GetComponent<PlayerData>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        playerController.Wound(damage);
        isMoving = false;
        isAttackCooldown = true;
        attackResetTime = Time.time + attackCooldown;
    }

    void Update()
    {
        if (isMoving)
        {
            Vector3 localPosition = player.transform.position - transform.position;
            localPosition = localPosition.normalized; // The normalized direction in LOCAL space
            transform.Translate(localPosition.x * Time.deltaTime * speed, localPosition.y * Time.deltaTime * speed, localPosition.z * Time.deltaTime * speed);
        }

        if (isAttackCooldown)
        {
            if (attackResetTime <= Time.time)
            {
                isAttackCooldown = false;
                isMoving = true;
            }
        }
    }
}
