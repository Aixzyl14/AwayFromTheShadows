using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 5f)]
    public float Volume;
    [Range(.1f, 3f)]
    public float Pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
