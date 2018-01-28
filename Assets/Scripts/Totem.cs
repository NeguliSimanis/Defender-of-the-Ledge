using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour {

    PlayerData playerData;
    Animator _animator;

    public bool isPlayerNear = false;
    private bool isHealAvailable = true;

    [SerializeField]
    private int healAmount = 30;
    [SerializeField]
    private float healCooldown = 5f;
    private float nextHealTime;

    void Start ()
    {
        _animator = gameObject.GetComponent<Animator>();
        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerNear = true;
            _animator.SetBool("signal", true);
        }
        //Debug.Log("player near");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerNear = false;
            _animator.SetBool("signal", false);
        }
           // Debug.Log("player far");
    }
    public void HealPlayer()
    {
        if (isHealAvailable)
        {
            if (playerData.hp < playerData.maxHp)
            {
                playerData.hp = playerData.hp + healAmount;
            }

            if (playerData.mana < playerData.maxMana)
            {
                playerData.mana = playerData.mana + healAmount;
            }
            
            nextHealTime = Time.time + healCooldown;
            _animator.SetBool("inactive", true);
            isHealAvailable = false;
        }
       
    }

    void OnMouseDown()
    {
        HealPlayer();
       /*Debug.Log("heal called");
        if (totem.isPlayerNear)
        {
            totem.HealPlayer();

            Debug.Log("heal called");
        }*/
    }

    void Update()
    {
        if (!isHealAvailable && nextHealTime <= Time.time)
        {
            isHealAvailable = true;
            _animator.SetBool("inactive", false);
        }
    }
}
