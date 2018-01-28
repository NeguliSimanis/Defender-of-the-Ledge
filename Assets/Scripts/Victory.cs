using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour {


    [SerializeField]
    BossAIController bossAIController;

    [SerializeField]
    GameObject victoryMenu;

    PlayerController playerController;

    private bool isVictory = false;

	void Start ()
    {
        playerController = gameObject.GetComponent<PlayerController>();
	}
	
    void WinGame()
    {
        Time.timeScale = 0;
        playerController.isGamePaused = true;
        victoryMenu.SetActive(true);
    }

	// Update is called once per frame
	void Update ()
    {
		if (!isVictory && bossAIController.isDead)
        {
            isVictory = true;
            WinGame();
        }
	}
}
