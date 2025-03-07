using UnityEngine;
using UnityEngine.Events;

public class InvokeEventOnTriggerEnter : MonoBehaviour
{
    [SerializeField] bool oneTime;
    [SerializeField] UnityEvent unityEvent;

    bool triggered;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (oneTime)
            {
                if (!triggered)
                {
                    triggered = true;
                    unityEvent.Invoke();
                }
            }
            else
            {
                unityEvent.Invoke();
            }
        }
    }
}