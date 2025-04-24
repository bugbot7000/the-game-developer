using UnityEngine;

public class FixScaleAfterAnimation : MonoBehaviour
{
    void Update()
    {
        if (transform.localScale == Vector3.one)
        {
            transform.localScale = Vector3.one * 2;
        }
    }
}