using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CursorMover : MonoBehaviour
{
    public CinemachineCamera mainCam, optionsCam, creditsCam, loadingCam;
    public float mouseH, mouseV, xClamp,yClamp;
    public GameObject Lamp,Options,LoadingCanvas,DesktopCanvas,DeleteGameButton;
    public CanvasGroup fadeCanvas;
    public GameObject loadingSpinner;
    public TextMeshProUGUI startGameButton;

    Collider option;
    bool continueGame;
    
    void Start()
    {
        Cursor.visible = false;
        mainCam.Priority = 1;
        Cursor.lockState = CursorLockMode.Locked;
        fadeCanvas.DOFade(0.0f, 1.0f).SetEase(Ease.OutCubic);

        if (PlayerPrefs.HasKey("CompletedIntro") && PlayerPrefs.GetInt("CompletedIntro") == 1)
        {
            startGameButton.SetText("_continue");
            continueGame = true;
            DeleteGameButton.SetActive(true);
        }
    }

    public void StartGame()
    {
        StartCoroutine (_StartGame());

        IEnumerator _StartGame()
        {
            GetComponentInParent<Canvas>().enabled = false;
            
            if (continueGame)
            {
                DesktopCanvas.SetActive(true);
            }
            else
            {
                LoadingCanvas.SetActive(true);
            }

            loadingCam.Priority = 2;

            yield return new WaitForSeconds(3.0f);

            yield return fadeCanvas.DOFade(1.0f, 1.0f).SetEase(Ease.OutCubic).WaitForCompletion();

            loadingSpinner.SetActive(true);

            // We let the game feel like it's actually loading for a sec before the real load stalls the animation.
            yield return new WaitForSeconds(2.0f);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }            
        }
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

        if (Input.GetMouseButtonUp(0))
        {
            // If in credits, clicking also returns to the menu.
            if (Lamp.activeInHierarchy)
            {
                BackToMainMenu();
            }
            else if (option != null)
            {
                if (option.name == "Start")
                {
                    StartGame();
                }
                else if (option.name == "Credits")
                {
                    Lamp.SetActive(true);
                    SwitchToCredits();
                }
                else if (option.name == "Options")
                {
                    creditsCam.Priority = 0;
                    mainCam.Priority = 0;
                    optionsCam.Priority = 1;
                    Options.SetActive(true);
                }
                else if (option.name == "Exit")
                {
                    #if UNITY_EDITOR
                        EditorApplication.isPlaying = false;         
                    #else
                        Application.Quit();   
                    #endif
                }
                else if (option.name == "Delete")
                {
                    PlayerPrefs.DeleteAll();
                    startGameButton.SetText("_start");
                    continueGame = false;
                    DeleteGameButton.SetActive(false);
                    option = null;                    
                }            
            }   
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
        option = other;
        
        if (other.name == "Start" || other.name == "Credits" || other.name == "Options")
        {
            other.GetComponent<TextMeshProUGUI>().color = Color.white;
        }

        if (other.name == "Exit" || other.name == "Delete")
        {
            other.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<TextMeshProUGUI>().color = new Color(0.4f,0.4f,0.4f);

        option = null;
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
