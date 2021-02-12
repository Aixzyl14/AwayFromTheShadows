
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public bool Playing = false;
    public Sound[] sounds;
    public static AudioManager instance;

    private void Awake()// before start
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip; // to affect the unity hud
            s.source.volume = s.Volume;
            s.source.pitch = s.Pitch;
            s.source.loop = s.loop;

        }
    }

    public void Play (string name)
    {
        Playing = true;
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        if (s == null)
            Debug.LogWarning("Sound:" + name + " not found!");
            return; 
    }
}
