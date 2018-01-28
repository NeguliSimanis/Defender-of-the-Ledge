using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGameFromVictory : MonoBehaviour {

    PlayerController playerController;

	// Use this for initialization
	void Start ()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}
	
	public void ResumeGame()
    {
        playerController.isGamePaused = false;
        Time.timeScale = 1f;
    }
}
