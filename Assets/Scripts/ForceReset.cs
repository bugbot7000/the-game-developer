using UnityEngine;
using UnityEngine.SceneManagement;

public class ForceReset : MonoBehaviour
{
    float timer;
    float holdTime = 3.0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            timer += Time.deltaTime;
            
            if (timer > holdTime)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                this.enabled = false;
            }
        }
        else
        {
            timer = 0;
        }
    }
}