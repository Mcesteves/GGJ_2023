using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject SettingsUI;
    [SerializeField]
    private GameObject CreditsUI;
    [SerializeField]
    private GameObject fade;
    private string scene;

    private void Start()
    {
        AudioManager.instance.Stop("Battle");
        AudioManager.instance.Play("Tema");
        fade.GetComponent<Animator>().Play("FadeIn");
    }
    public void PlayGame()
    {
        NextScene("LevelSelection");
    }
    public void OpenSettings()
    {
        SettingsUI.SetActive(true);
        CreditsUI.SetActive(false);
    }
    public void CloseSettings()
    {
        SettingsUI.SetActive(false);
    }
    public void OpenCredits()
    {
        CreditsUI.SetActive(true);
        SettingsUI.SetActive(false);
    }
    public void CloseCredits()
    {
        CreditsUI.SetActive(false);
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
