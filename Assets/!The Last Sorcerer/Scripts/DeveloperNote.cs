using UnityEngine;

public class DeveloperNote : MonoBehaviour
{
    [SerializeField] GameObject canvas;

    void OnTriggerEnter()
    {
        canvas.SetActive(true);
    }
}