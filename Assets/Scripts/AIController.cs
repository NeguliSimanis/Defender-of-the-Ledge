using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    private GameObject player;
    PlayerData playerData;

    private bool isMoving = true;
    private float attackResetTime;
    private bool isAttackCooldown = false;
    

    public bool isTargetted = false;

    #region Enemy properties
    [SerializeField]
    private bool isDead = false;

    [SerializeField]
    private int speed = 2;

    [SerializeField]
    private int damage = 15;

    [SerializeField]
    private float attackCooldown = 1f;

    [SerializeField]
    private int hp = 25;

    [SerializeField]
    private int expGiven = 25;

    [SerializeField]
    private Sprite deadSprite;
    #endregion

    void Start ()
    {
        player = GameObject.FindWithTag("Player");
        playerData = player.GetComponent<PlayerData>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("COLLISION");
        if (other.gameObject.tag == "Player" && !isDead)
        {
            playerData.Wound(damage);
            isMoving = false;
            isAttackCooldown = true;
            attackResetTime = Time.time + attackCooldown;
        }   
    }

    void OnMouseDown()
    {
        if (!isDead)
            isTargetted = true;
        //Debug.Log("enemy targetted!");
    }

    public void Wound(int playerDamage)
    {
        //Debug.Log("enemy is being attacked!");
        hp = hp - playerDamage;
        if (hp <= 0)
        {
            isDead = true;
            StayDead();
        }
    }

    public void StayDead()
    {
        playerData.AddExp(expGiven);

        gameObject.GetComponent<Animator>().SetTrigger("die");
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //Debug.Log("IM DEAD LOL");
        
    }

    void Update()
    {
        if (!isDead)
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

            if (hp <= 0)
            {
                isDead = true;
                StayDead();
            }
        }
        
    }
}
