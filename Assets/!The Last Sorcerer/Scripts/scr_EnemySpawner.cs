using System.Collections;
using UnityEngine;

public class scr_EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnInterval = 5f;
    public GameObject spawnedEnemy;
    public GameObject arenaManager;
    public ArenaManager_scr arenaMgrScr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemies());
        arenaMgrScr = arenaManager.GetComponent<ArenaManager_scr>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedEnemy != null)
        {
            if (spawnedEnemy.GetComponent<scr_health>().health <= 0f) 
            { 
                spawnedEnemy = null; 
                if (arenaManager != null)
                {
                    arenaMgrScr.deadEnemies += 1;
                }
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(spawnInterval);

        if (spawnedEnemy == null) 
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            GameObject enemy = Instantiate(enemyPrefabs[randomEnemy], transform.position, transform.rotation);
            spawnedEnemy = enemy;
        }

        StartCoroutine(SpawnEnemies());
    }
}
