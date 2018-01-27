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

    private bool isBossActive = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isBossActive && other.gameObject.tag == "Player")
        {
            AudioSource audio = audioCamera.GetComponent<AudioSource>();
            audio.clip = bossMusic;
            audio.Play();

            boss.SetActive(true);
            bossHealthBar.SetActive(true);

            isBossActive = true;
        }
    }
}

