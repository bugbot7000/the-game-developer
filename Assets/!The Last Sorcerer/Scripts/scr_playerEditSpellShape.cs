using UnityEngine;

public class scr_playerEditSpellShape : MonoBehaviour
{
    scr_playerController playerController;

    void Start()
    {
        playerController = GetComponent<scr_playerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerController.SwitchSpell1ToSwipe();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerController.SwitchSpell1ToBeam();
        }              
    }
}