using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseUI;
    [HideInInspector]
    public bool isPaused;
    [SerializeField]
    private GameObject fade;

    private void Awake()
    {
        AudioManager.instance.Stop("Tema");
        AudioManager.instance.Stop("Battle");
    }
    private void Start()
    {
        AudioManager.instance.Play("Battle");
        fade.GetComponent<Animator>().Play("FadeIn");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        PauseUI.SetActive(true);
        isPaused = true;

    }
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        PauseUI.SetActive(false);
        isPaused = false;
    }

    public void GotoMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
}
