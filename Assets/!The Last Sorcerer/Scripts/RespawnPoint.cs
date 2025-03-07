using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.transform.position = transform.position;
        }
    }
}