using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour {

    Renderer renderer;

    [SerializeField]
    private float spawnCooldown = 3f;

    [SerializeField]
    private GameObject enemyPrefab;
    private bool isActive = false;
    private bool isInvisible = true;

    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        if (!renderer.isVisible)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (!renderer.isVisible)
        {
            //GameObject enemyExile = Instantiate(enemyPrefab) as GameObject;
            //enemyExile.transform.parent = gameObject.transform;
            Debug.Log("is invisible");
            Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);

        }
        StartCoroutine(SpawnCooldown());
    }


    IEnumerator SpawnCooldown()
    {
        print(Time.time);
        yield return new WaitForSeconds(spawnCooldown);
        SpawnEnemy();
    }

}
