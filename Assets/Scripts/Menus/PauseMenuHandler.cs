using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject inventoryPanel;
    public bool usingPausePanel;
    private bool isPaused;


    public void ChangePauseState()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            usingPausePanel = true;
        }
        else
        {
            inventoryPanel.SetActive(false);
            pausePanel.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }


    public void SwitchPanels()
    {
        usingPausePanel = !usingPausePanel;

        if (usingPausePanel)
        {
            pausePanel.SetActive(true);
            inventoryPanel.SetActive(false);
        }
        else
        {
            pausePanel.SetActive(false);
            inventoryPanel.SetActive(true);
        }
    }


    public void QuitToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }


    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            ChangePauseState();
        }
    }


    private void Start()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);
        usingPausePanel = false;
    }
}
