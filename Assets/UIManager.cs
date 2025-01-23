using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject SelectionPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SelectionPanel.SetActive(!SelectionPanel.activeSelf);
        }
    }
}
