using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    PlayerData playerData;

    [SerializeField]
    private float fireballDuration = 3f;
    [SerializeField]
    private float fireballSpeed = 0.1f;
    private bool isFlying = false;
    public Vector2 target;

  
    Transform _transform;

    void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
        playerData = GameObject.FindWithTag("Player").GetComponent<PlayerData>();
        fireballSpeed = playerData.spellProjSpeed;
        Debug.Log("spell proj speed" + fireballSpeed);
        StartCoroutine(SelfDestroy());
    }

    public void Fly()
    {
        target = Camera.main.ScreenToWorldPoint(target);
        if (target.x >= gameObject.transform.position.x)
        {
            gameObject.transform.localScale = new Vector3(-0.6F, 0.6F, 1F);

        }   
        isFlying = true;
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(fireballDuration);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.name != "Boss")
            {
                AIController enemyController = other.gameObject.GetComponent<AIController>();
                enemyController.Wound(playerData.spellDamage);
            }
            else
            {
                BossAIController enemyController = other.gameObject.GetComponent<BossAIController>();
                enemyController.Wound(playerData.spellDamage);
            }
            
            Destroy(gameObject);
        }

        else if (other.gameObject.tag == "EnemyProjectile")
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, target, fireballSpeed);
    }
}
