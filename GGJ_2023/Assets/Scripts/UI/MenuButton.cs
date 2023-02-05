using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void GotoMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
    public void GotoLevelSelection()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("LevelSelection");
    }

}
