using UnityEngine;

public class scr_playerEditSpellShapeAndEffect : MonoBehaviour
{
    scr_playerController playerController;

    void Start()
    {
        playerController = GetComponent<scr_playerController>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                playerController.ChangeFirstSpellEffect(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                playerController.ChangeFirstSpellEffect(1);
            } 
        }
        else
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
}