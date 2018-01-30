using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {


    private bool isMusicMuted = false;

    [SerializeField]
    private AudioClip defaultMusic;

    [SerializeField]
    private AudioClip bossMusic;

    [SerializeField]
    private AudioClip creditsMusic;

    AudioSource audioSource;
    
    
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void SetDefaultMusic ()
    {
        audioSource.clip = defaultMusic;
        audioSource.Play();
    }
	
    public void SetBossMusic()
    {
        audioSource.clip = bossMusic;
        audioSource.Play();
    }

    public void SetCreditsMusic()
    {
       // audioSource.clip = creditsMusic;
        //audioSource.Play();
    }


}
