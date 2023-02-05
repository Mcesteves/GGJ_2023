using UnityEngine.Audio;
using UnityEngine;

public enum SoundType
{
    sfx,
    music
}

[System.Serializable]
public class Sound
{
    public string nome;
    public AudioClip audio;
    public SoundType type;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

}
