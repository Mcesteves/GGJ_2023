using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageButton : MonoBehaviour
{
    public Language buttonLanguage;
    void Start()
    {
        LanguageManager.instance.OnLanguageChange += UpdateButton;
        UpdateButton();
    }
    public void UpdateLanguage()
    {
        LanguageManager.instance.ChangeLanguage(buttonLanguage);
    }
    private void UpdateButton()
    {
        var check = buttonLanguage == LanguageManager.instance.currentLanguage;
        GetComponent<Button>().interactable = !check;
    }
}
