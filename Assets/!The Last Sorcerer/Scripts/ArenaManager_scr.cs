using UnityEngine;

public class ArenaManager_scr : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject boss;
    public bool bossSpawned;
    public int deadEnemies;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (deadEnemies >= 2 && !bossSpawned) 
        {
            spawners[1].SetActive(true);
        }
        if (deadEnemies >= 4 && !bossSpawned)
        {
            spawners[2].SetActive(true);
        }
        if (deadEnemies >= 6 && !bossSpawned)
        {
            spawners[3].SetActive(true);
        }
        if (deadEnemies >= 10 && !bossSpawned)
        {
            spawners[4].SetActive(true);
        }
        if(deadEnemies >= 12 && !bossSpawned)
        {
            //GameObject enemy = Instantiate(boss, spawners[4].transform.position, spawners[4].transform.rotation);
            boss.SetActive(true);
            bossSpawned = true;
            spawners[0].SetActive(false);
            spawners[1].SetActive(false);
            spawners[2].SetActive(false);
            spawners[3].SetActive(false);
            spawners[4].SetActive(false);
        }
        CheckVictory();
    }

    public void CheckVictory()
    {
        if (bossSpawned && boss.GetComponent<scr_health>().health <= 0) { Debug.Log("This is where you might put victory text"); }
    }
}
