using UnityEngine;
using System.Collections.Generic;

public class altar_scr : MonoBehaviour
{
    public GameObject boss;
    public GameObject altar;
    List<GameObject> sprites = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sprite")) 
        {
            other.gameObject.GetComponent<enemyAI_Script>().SpriteOrbitStart(altar);
            other.gameObject.GetComponent<enemyAI_Script>().enabled = false;
            sprites.Add(other.gameObject);
            if(sprites.Count == 3) { boss.SetActive(true); }
        }
    }
}
