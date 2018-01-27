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
        _transform = GetComponent<Transform>();
        playerData = GameObject.FindWithTag("Player").GetComponent<PlayerData>();
        StartCoroutine(SelfDestroy());
    }

    public void Fly()
    {
        target = Camera.main.ScreenToWorldPoint(target);
        if (target.x >= gameObject.transform.position.x)
        {
           // Vector3 localScale = _transform.localScale;
            //localScale.x *= -1;

            _transform.localScale *= -1;
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
            AIController enemyController = other.gameObject.GetComponent<AIController>();
            enemyController.Wound(playerData.spellDamage);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, target, fireballSpeed);
    }
}
