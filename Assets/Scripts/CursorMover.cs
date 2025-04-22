using System;
using TMPro;
using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorMover : MonoBehaviour
{
    public CinemachineCamera mainCam, optionsCam, creditsCam;
    public float mouseH, mouseV, xClamp,yClamp;
    public GameObject Lamp,Options;
    void Start()
    {
        Cursor.visible = false;
        mainCam.Priority = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);

    }
    void Update()
    {
        if (mainCam.Priority > optionsCam.Priority && mainCam.Priority > creditsCam.Priority)
        {
            MoveMainCursor();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMainMenu();
        }

        if (Lamp.activeInHierarchy)
        {
            mouseH = -Input.GetAxis("Mouse X");
            float rotationX = Lamp.transform.localEulerAngles.x;
            if (rotationX > 180f) rotationX -= 360f;
            rotationX += mouseH;
            rotationX = Mathf.Clamp(rotationX, 45f, 165f);
            Lamp.transform.localRotation = Quaternion.Euler(rotationX, 175.631f, 220.4f);
        }
    }

    void MoveMainCursor()
    {
        mouseH = -Input.GetAxis("Mouse X");
        mouseV = Input.GetAxis("Mouse Y");
        transform.localPosition += new Vector3(mouseH, mouseV, 0) * Time.deltaTime;
        xClamp = Mathf.Clamp(transform.localPosition.x, -0.24f,0.24f);
        yClamp = Mathf.Clamp(transform.localPosition.y, -0.135f,0.14f);
        transform.localPosition = new Vector3(xClamp, yClamp, 0.0005f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Start" || other.name == "Credits" || other.name == "Options")
        {
            other.GetComponent<TextMeshProUGUI>().color = Color.white;
        }

        if (other.name == "Exit")
        {
            other.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<TextMeshProUGUI>().color = new Color(0.7f,0.7f,0.7f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (other.name == "Start")
            {
                StartGame();
            }
            else   if (other.name == "Credits")
            {
                Lamp.SetActive(true);
                Invoke("SwitchToCredits", 0.85f);
            }
            else   if (other.name == "Options")
            {
                
                creditsCam.Priority = 0;
                mainCam.Priority = 0;
                optionsCam.Priority = 1;
                Options.SetActive(true);
            }
        }
    }
    private void SwitchToCredits()
    {
        creditsCam.Priority = 1;
        mainCam.Priority = 0;
        optionsCam.Priority = 0;
    }

    public void BackToMainMenu()
    {
        Options.SetActive(false);
        Lamp.SetActive(false);
        optionsCam.Priority = 0;
        creditsCam.Priority = 0;
        mainCam.Priority = 1;
       
    }
}
