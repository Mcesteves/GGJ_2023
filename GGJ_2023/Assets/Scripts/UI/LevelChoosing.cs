using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChoosing : MonoBehaviour
{
    [SerializeField]
    private GameObject SettingsUI;
    [SerializeField]
    private GameObject fade;
    private string scene;

    private void Awake()
    {
        AudioManager.instance.Stop("Tema");
        AudioManager.instance.Stop("Battle");
    }
    private void Start()
    {
        fade.GetComponent<Animator>().Play("FadeIn");
    }
    public void OpenSettings()
    {
        SettingsUI.SetActive(true);
    }
    public void CloseSettings()
    {
        SettingsUI.SetActive(false);
    }
    public void GotoMainMenu()
    {
        NextScene("MainMenu");
    }
    private IEnumerator ChangeScene()
    {
        fade.GetComponent<Animator>().Play("FadeOut");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(scene);
    }
    public void NextScene(string scene)
    {
        this.scene = scene;
        StartCoroutine(ChangeScene());
    }
    public void PlayClickSoundIn()
    {
        AudioManager.instance.Play("click_01");
    }

    public void PlayClickSoundOut()
    {
        AudioManager.instance.Play("click_02");
    }
}
