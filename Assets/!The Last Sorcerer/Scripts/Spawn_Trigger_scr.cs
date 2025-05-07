using System.Collections;
using UnityEngine;

public class Spawn_Trigger_scr : MonoBehaviour
{
    public GameObject enemySpawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "player")
        {
            StartCoroutine(TurnEnemiesOn());
        }
    }

    IEnumerator TurnEnemiesOn()
    {
        yield return new WaitForSeconds(5);
        enemySpawner.SetActive(true) ;
    }
}
