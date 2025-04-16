using UnityEngine;

public class Spawn_Trigger_scr : MonoBehaviour
{
    public GameObject enemySpawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "player")
        {
            enemySpawner.SetActive(true);
        }
    }
}
