using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public enum Language
{
    Br,
    En
}
public class LanguageManager : MonoBehaviour
{
    [HideInInspector]
    public static LanguageManager instance;
    public Language currentLanguage;
    private int idxLanguage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public event Action OnLanguageChange;

    private void Start()
    {
        idxLanguage = PlayerPrefs.GetInt("Language");
        currentLanguage = (Language)idxLanguage;
        OnLanguageChange?.Invoke();
    }

    public void ChangeLanguage(Language language)
    {
        idxLanguage = (int)language;
        PlayerPrefs.SetInt("Language", idxLanguage);
        currentLanguage = language;
        OnLanguageChange?.Invoke();
    }
}
