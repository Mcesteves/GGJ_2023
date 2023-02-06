using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    private GameObject fade;
    public void GotoMainMenu()
    {
        NextScene("MainMenu");
    }
    public void GotoLevelSelection()
    {
        NextScene("LevelSelection");
    }

    private IEnumerator ChangeScene(string scene)
    {
        fade.GetComponent<Animator>().Play("FadeOut");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(scene);
    }
    public void NextScene(string scene)
    {
        StartCoroutine(ChangeScene(scene));
    }

}
