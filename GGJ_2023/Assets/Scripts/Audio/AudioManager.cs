using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    void Awake()
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

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audio;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        if (PlayerPrefs.GetInt("FirstGame") == 0)
        {
            PlayerPrefs.SetFloat("Master", 50);
            PlayerPrefs.SetFloat("Music", 50);
            PlayerPrefs.SetFloat("SFX", 50);
            PlayerPrefs.SetInt("FirstGame", 1);
        }
        UpdateSoundVolumes();
    }

    public void Play(string nome)
    {
        Sound s = Array.Find(sounds, sound => sound.nome == nome);
        if (s == null)
            return;
        s.source.Play();
    }

    public void Stop(string nome)
    {
        Sound s = Array.Find(sounds, sound => sound.nome == nome);
        if (s == null)
            return;
        s.source.Stop();
    }

    public void UpdateSoundVolumes()
    {
        foreach (Sound s in sounds)
        {
            if (s.type == SoundType.music)
                s.source.volume = PlayerPrefs.GetFloat("Music") * (1.0f / 100) * PlayerPrefs.GetFloat("Master") * (1.0f / 100);
            else
                s.source.volume = PlayerPrefs.GetFloat("SFX") * (1.0f / 100) * PlayerPrefs.GetFloat("Master") * (1.0f / 100);
        }
    }

}
