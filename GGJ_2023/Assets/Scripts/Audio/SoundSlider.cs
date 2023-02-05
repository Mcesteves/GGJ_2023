using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    private static string masterSoundPrefs = "Master";
    private static string soundEffectsPrefs = "SFX";
    private static string musicPrefs = "Music";
    private float soundValue;

    void Start()
    {
        SetSoundPrefs();
    }

    public void UpdateVolume()
    {
        soundValue = GetComponent<Slider>().value;

        if (gameObject.tag == "Master")
            PlayerPrefs.SetFloat(masterSoundPrefs, soundValue);
        else if (gameObject.tag == "Music")
            PlayerPrefs.SetFloat(musicPrefs, soundValue);
        else
            PlayerPrefs.SetFloat(soundEffectsPrefs, soundValue);

        AudioManager.instance.UpdateSoundVolumes();
    }

    private void SetSoundPrefs()
    {
        if (gameObject.tag == "Master")
            soundValue = PlayerPrefs.GetFloat(masterSoundPrefs);
        else if (gameObject.tag == "Music")
            soundValue = PlayerPrefs.GetFloat(musicPrefs);
        else
            soundValue = PlayerPrefs.GetFloat(soundEffectsPrefs);

        GetComponent<Slider>().value = soundValue;
    }
}
