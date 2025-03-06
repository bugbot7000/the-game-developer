using UnityEngine;

public class threshold_scr : MonoBehaviour
{
    public GameObject sprite;
    //public GameObject[] guardians;
    public GameObject guardianHolder;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == sprite)
        {
            //for (int i = 0; i < guardians.Length; i++)
            //{
            //    guardians[i].SetActive(true);
            //}
            guardianHolder.SetActive(true);
        }
    }
}
