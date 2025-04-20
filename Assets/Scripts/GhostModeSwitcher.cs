using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostModeSwitcher : MonoBehaviour
{
    public Color ghostColor;
    public Color normalColor;
    public float outlineStrength, transparencyStrength;
    public List<Material> materials = new List<Material>();
    private Coroutine transitionRoutine;

    void Start()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in renderers)
        {
            Material mat = rend.material; // Instance of the material
            if (mat.HasProperty("_Ghost_Color") &&
                mat.HasProperty("_Transparency") &&
                mat.HasProperty("_Outline"))
            {
                materials.Add(mat);
            }
        }
        EnableGhost();
    }

    void Update()
    {
        // we do this with animations
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    EnableGhost();
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    DisableGhost();
        //}
    }

    public void EnableGhost()
    {
        if (transitionRoutine != null) StopCoroutine(transitionRoutine);
        transitionRoutine = StartCoroutine(TransitionAllMaterials(ghostColor, transparencyStrength, outlineStrength));
    }

    public void DisableGhost()
    {
        if (transitionRoutine != null) StopCoroutine(transitionRoutine);
        transitionRoutine = StartCoroutine(TransitionAllMaterials(normalColor, 1, 0));
    }

    private IEnumerator TransitionAllMaterials(Color targetColor, float targetTransparency, float targetOutline)
    {
        float duration = 1.5f;
        float time = 0f;

        List<Color> startColors = new List<Color>();
        List<float> startTransparencies = new List<float>();
        List<float> startOutlines = new List<float>();

        foreach (var mat in materials)
        {
            startColors.Add(mat.GetColor("_Ghost_Color"));
            startTransparencies.Add(mat.GetFloat("_Transparency"));
            startOutlines.Add(mat.GetFloat("_Outline"));
        }

        while (time < duration)
        {
            float t = time / duration;
            for (int i = 0; i < materials.Count; i++)
            {
                materials[i].SetColor("_Ghost_Color", Color.Lerp(startColors[i], targetColor, t));
                materials[i].SetFloat("_Transparency", Mathf.Lerp(startTransparencies[i], targetTransparency, t));
                materials[i].SetFloat("_Outline", Mathf.Lerp(startOutlines[i], targetOutline, t));
            }

            time += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].SetColor("_Ghost_Color", targetColor);
            materials[i].SetFloat("_Transparency", targetTransparency);
            materials[i].SetFloat("_Outline", targetOutline);
        }
    }
}
