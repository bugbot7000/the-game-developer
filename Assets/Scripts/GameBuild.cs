using UnityEngine;

public class GameBuild : MonoBehaviour
{
    [SerializeField] Camera gameCamera;
    [SerializeField] RenderTexture renderTexture;

    void Start()
    {
        gameCamera.targetTexture = renderTexture;
        Destroy(gameCamera.gameObject.GetComponent<AudioListener>());
    }
}