using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using DG.Tweening;

public class RitualInput : MonoBehaviour
{
    [SerializeField] Transform respawn;
    [SerializeField] CanvasGroup blackFade;

    enum InputAction
    {
        Space, One, Two, Three, Shift
    }

    List<InputAction> secretSequence = new List<InputAction> {
        InputAction.Space, InputAction.One, InputAction.Two, InputAction.Three,
        InputAction.Space, InputAction.One, InputAction.Shift, InputAction.Shift
    };

    Queue<InputAction> inputBuffer = new Queue<InputAction>();
    int maxBufferSize;

    Coroutine ritualCoroutine;

    void Start()
    {
        maxBufferSize = secretSequence.Count;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) registerInput(InputAction.One);
        if (Input.GetKeyDown(KeyCode.Alpha2)) registerInput(InputAction.Two);
        if (Input.GetKeyDown(KeyCode.Alpha3)) registerInput(InputAction.Three);
        if (Input.GetKeyDown(KeyCode.Space)) registerInput(InputAction.Space);
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))  registerInput(InputAction.Shift);
    }

    void registerInput(InputAction action)
    {
        inputBuffer.Enqueue(action);

        if (inputBuffer.Count > maxBufferSize)
            inputBuffer.Dequeue();

        if (inputBuffer.SequenceEqual(secretSequence))
        {
            onSecretCodeEntered();
            inputBuffer.Clear();
        }
    }

    void onSecretCodeEntered()
    {
        if (ritualCoroutine == null)
        {
            ritualCoroutine = StartCoroutine(loadRitual());
        }
    }

    IEnumerator loadRitual()
    {
        yield return blackFade.DOFade(1, 1).WaitForCompletion();

        transform.position = respawn.transform.position;

        yield return blackFade.DOFade(0, 1).WaitForCompletion();

        ritualCoroutine = null;
    }
}
