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
    /*void Update ()
    {
        // isPointVisible = renderer.isVisible;
        if (isInvisible)
        {
            if (!renderer.isVisible)
            {
                Debug.Log("spawning");
                SpawnEnemy();
            }
            else if (renderer.isVisible)
            {
                isInvisible = false;
            }
        }

        if (!renderer.isVisible)
        {
            isInvisible = true;
        }



        /*if (renderer.isVisible)
        {
            isActive = false;
        }*/

        /* if (isActive)
         {
             Debug.Log("spawning");

         }*/

   // }
}
