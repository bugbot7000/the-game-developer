using UnityEngine;
using UnityEngine.SceneManagement;

public class CoconutScr : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S) && Input.GetKeyUp(KeyCode.Space))
            {
                Debug.Log("Loading Ritual...");
                //SceneManager.LoadScene("Ritual"); // The ritual scene needs to be in the build settings
            }
        }
    }
}
