using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class PauseMenuScript : MonoBehaviour
{
    public static PauseMenuScript instance;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject sessionInfoUI;
    [SerializeField] private bool isPaused;
    [SerializeField] private Text name;
    [SerializeField] private Text scoreUI;
    [SerializeField] private Text score;
    public bool isFinished;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (!isFinished && CrossPlatformInputManager.GetButtonDown("Cancel"))
        {
            isPaused = !isPaused;
        }

        if(isPaused && !isFinished)
        {
            ActivateMenu();
        }
        else if (isFinished)
        {
            // Do nothing
        }
        else
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        //TimerController.instance.EndTimer();
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DeactivateMenu()
    {
        if(sessionInfoUI.activeSelf)
        {
            sessionInfoUI.SetActive(!sessionInfoUI.activeSelf);
        }
        else {
            Time.timeScale = 1;
            AudioListener.pause = false;
            //TimerController.instance.BeginTimer();
            pauseMenuUI.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            isPaused = false;
        }
        
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void EndGame()
    {
        isFinished = true;
        Time.timeScale = 0;
        //finishMenuUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        score.text = scoreUI.text;
        TimerController.instance.EndTimer();
    }

    public void ToggleSessionInfo()
    {
        sessionInfoUI.SetActive(!sessionInfoUI.activeSelf);
        sessionInfoUI.GetComponent<SessionInfo>().UpdateSession();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Track1");
    }

    public void ReturnAfterFinish()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
