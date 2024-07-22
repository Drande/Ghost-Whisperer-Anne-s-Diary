using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private Sound[] musicSounds, sfxSounds;
    [SerializeField] private AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void HandlePauseChange(bool isPaused)
    {
        if (isPaused)
        {
            musicSource.Pause();
        }
        else
        {
            musicSource.UnPause();
        }
    }

    private void Start()
    {
        HandleSceneChange(SceneManager.GetActiveScene());
        SceneManager.activeSceneChanged += (previous, current) =>
        {
            if (previous.name != current.name)
            {
                HandleSceneChange(current);
            }
        };
    }

    private void HandleSceneChange(Scene scene)
    {
        switch (scene.name)
        {
            default:
                // TODO: Add scene music changes.
                break;
        }
    }

    public void PlayMusic(string name)
    {
        var sound = Array.Find(musicSounds, s => s.name == name);
        if (sound != null)
        {
            musicSource.Stop();
            musicSource.loop = true;
            musicSource.clip = sound.audioClip;
            musicSource.Play();
        }
        else
        {
            Debug.Log($"Couldn't find music sound by name: '{name}'");
        }
    }

    public void PlaySFX(string name)
    {
        var sound = Array.Find(sfxSounds, s => s.name == name);
        if (sound != null)
        {
            sfxSource.clip = sound.audioClip;
            sfxSource.Play();
        }
        else
        {
            Debug.Log($"Couldn't find sound effect by name: {name}");
        }
    }
}