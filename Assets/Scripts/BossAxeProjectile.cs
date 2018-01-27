using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAxeProjectile : MonoBehaviour {

    PlayerData playerData;
    GameObject player;
    

    [SerializeField]
    private float axeDuration = 2f;
    [SerializeField]
    private float axeSpeed = 0.01f;
    [SerializeField]
    private float axeRotation= 10f;

    [SerializeField]
    private int axeDamage = 55;

    private bool isFlying = false;
    public Vector2 target;


    Transform _transform;

    void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
        player = GameObject.FindWithTag("Player");
        playerData = player.GetComponent<PlayerData>();

        target = new Vector2(player.GetComponent<Transform>().position.x, player.GetComponent<Transform>().position.y);
        //axeSpeed = playerData.spellProjSpeed;
        Debug.Log("axe proj speed" + axeSpeed);
        StartCoroutine(SelfDestroy());
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(axeDuration);
       // Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerData.Wound(axeDamage);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, target, axeSpeed);

       
            transform.Rotate(Vector3.forward * axeRotation);
        
    }
}
