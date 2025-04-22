using System;
using UnityEngine;

public class BlackTortoiseController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject TortoiseSpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.transform.position = TortoiseSpawnPoint.transform.position;
        }
    }
}
