using UnityEngine;

public class SaveAndLoadBroken : MonoBehaviour
{
    [SerializeField] GameObject errorScreen;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Period))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.Comma))
        {
            errorScreen.SetActive(true);
        }
    }
}