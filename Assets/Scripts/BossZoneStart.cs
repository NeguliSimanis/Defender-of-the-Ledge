using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZoneStart : MonoBehaviour
{
    [SerializeField]
    GameObject audioCamera;

    [SerializeField]
    GameObject bossHealthBar;

    [SerializeField]
    GameObject boss;

    [SerializeField]
    AudioClip bossMusic;

    MusicManager musicManager;

    private bool isBossActive = false;

    void Start()
    {
        musicManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MusicManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isBossActive && other.gameObject.tag == "Player")
        {
            musicManager.SetBossMusic();

            boss.SetActive(true);
            bossHealthBar.SetActive(true);

            isBossActive = true;
        }
    }
}

