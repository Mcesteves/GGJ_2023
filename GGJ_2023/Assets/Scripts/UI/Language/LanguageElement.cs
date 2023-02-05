using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageElement : MonoBehaviour
{
    public List<string> texts;
    private TextMeshProUGUI text;
    void Start()
    {
        LanguageManager.instance.OnLanguageChange += UpdateLanguage;
        text = GetComponent<TextMeshProUGUI>();
        UpdateLanguage();
    }
    private void UpdateLanguage()
    {
        var currentLanguage = (int)LanguageManager.instance.currentLanguage;
        text.text = texts[currentLanguage];
    }
}
