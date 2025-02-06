using UnityEngine;

public class ResolutionTest : MonoBehaviour
{
    [SerializeField] RenderTexture renderTexture;

    public void Set270p()
    {
        setResolution(480, 270);
    }

    public void Set160p()
    {
        setResolution(284, 164);
    }

    public void Set1080p()
    {
        setResolution(1920, 1080);
    }

    void setResolution(int width, int height)
    {
        renderTexture.Release();
        renderTexture.width = width;
        renderTexture.height = height;
        renderTexture.Create();
    }
}