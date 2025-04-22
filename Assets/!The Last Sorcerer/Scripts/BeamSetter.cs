using UnityEngine;

[ExecuteInEditMode]
public class BeamSetter : MonoBehaviour
{
    [ContextMenu("Set Particle Scaling to Hierarchy and Scale")]
    void SetParticleScaling()
      {
          Transform[] children = GetComponentsInChildren<Transform>(true);
  
          foreach (Transform child in children)
          {
              ParticleSystem ps = child.GetComponent<ParticleSystem>();
              if (ps != null)
              {
                  var main = ps.main;
                  main.scalingMode = ParticleSystemScalingMode.Hierarchy;
                  child.localScale = new Vector3(0.5f, 0.5f, 0.5f);
              }
          }
  
          Debug.Log("Particle emitters updated.");
      }
}
