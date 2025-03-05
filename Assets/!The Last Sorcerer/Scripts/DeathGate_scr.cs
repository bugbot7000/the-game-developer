using UnityEngine;

public class DeathGate_scr : MonoBehaviour
{
    public GameObject sacrifice;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sacrifice == null) { Destroy(gameObject); }
    }
}
