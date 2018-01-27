using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAIController : MonoBehaviour {

    private GameObject player;
    PlayerData playerData;

    private bool isMoving = true;
    private float attackResetTime;
    private bool isAttackCooldown = false;


    public bool isTargetted = false;

    [SerializeField]
    GameObject axe;

    [SerializeField]
    private Image healthBar;

    #region Enemy properties
    [SerializeField]
    private bool isDead = false;

    [SerializeField]
    private int speed = 2;

    [SerializeField]
    private int damage = 15;

    [SerializeField]
    private float attackCooldown = 2f;

    [SerializeField]
    private float prepareAttackCooldown = 1f;

    [SerializeField]
    private int hp = 110;
    [SerializeField]
    private int maxHp = 110;

    [SerializeField]
    private int expGiven = 25;

    [SerializeField]
    private Sprite deadSprite;
    #endregion

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerData = player.GetComponent<PlayerData>();
        ThrowAxe();
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

        gameObject.GetComponent<Animator>().SetTrigger("Die");
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //Debug.Log("IM DEAD LOL");

    }

    private void ThrowAxe()
    {
        if (!isDead)
        {
            StartCoroutine(ThrowAxePrepareCooldown());
        }
    }

    private IEnumerator ThrowAxePrepareCooldown()
    {
        isMoving = false;
        gameObject.GetComponent<Animator>().SetTrigger("Attack");
        yield return new WaitForSeconds(prepareAttackCooldown);
        StartCoroutine(ThrowAxeCooldown());
    }

    private IEnumerator ThrowAxeCooldown()
    {
        //Debug.Log("Throwing axe 1");
        GameObject projectile = Instantiate(axe,
          gameObject.transform.position, Quaternion.identity, gameObject.transform);
        isMoving = true;
        yield return new WaitForSeconds(attackCooldown);
        Debug.Log("Throwing axe");
        ThrowAxe();
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

        healthBar.fillAmount = (hp * 1f) / maxHp;

    }
}
