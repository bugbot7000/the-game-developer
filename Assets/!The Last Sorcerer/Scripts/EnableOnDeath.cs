using UnityEngine;

public class EnableOnDeath : MonoBehaviour
{
    [SerializeField] scr_health target;
    [SerializeField] MonoBehaviour toEnable;

    void Update()
    {
        if (target.health <= 0)
        {
            toEnable.enabled = true;
        }
    }
}