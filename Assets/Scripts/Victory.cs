using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour {


    [SerializeField]
    BossAIController bossAIController;

    [SerializeField]
    GameObject victoryMenu;

    PlayerController playerController;

    MusicManager musicManager;

    private bool isVictory = false;
    private float victoryDelaySeconds = 1f;

	void Start ()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        musicManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MusicManager>();
	}
	
    void WinGame()
    {
        Time.timeScale = 0;
        playerController.isGamePaused = true;
        victoryMenu.SetActive(true);
        musicManager.SetDefaultMusic();
    }


    IEnumerator VictoryDelay()
    {
        yield return new WaitForSeconds(victoryDelaySeconds);
        WinGame();
    }

    // Update is called once per frame
    void Update ()
    {
		if (!isVictory && bossAIController.isDead)
        {
            isVictory = true;
            StartCoroutine(VictoryDelay());
        }
	}
}
