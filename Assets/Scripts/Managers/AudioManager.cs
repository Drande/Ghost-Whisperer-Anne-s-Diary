using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] private Sound[] musicSounds, sfxSounds;
    [SerializeField] private AudioSource musicSource, sfxSource;
    [SerializeField] private AudioMixer masterMixer;
    private const string MusicVolumeKey = "MusicVolume";
    private const string SfxVolumeKey = "SfxVolume";
    private const string MasterVolumeKey = "MasterVolume";
    public bool isMuted => MasterVolume == 0f;
    public delegate void MuteStateChanged(bool isMuted);
    public event MuteStateChanged OnMuteStateChanged;

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

    private void Start()
    {
        ApplySettings();
        OnMuteStateChanged?.Invoke(isMuted);
        HandleSceneChange(SceneManager.GetActiveScene());
        SceneManager.activeSceneChanged += (previous, current) =>
        {
            if (previous.name != current.name) HandleSceneChange(current);
        };
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)) {
            ToggleMute();
        }
    }

    public float MusicVolume
    {
        get => PlayerPrefs.GetFloat(MusicVolumeKey, 1.0f);
        set
        {
            PlayerPrefs.SetFloat(MusicVolumeKey, value);
            PlayerPrefs.Save();
            SetMusicVolume(value);
        }
    }

    public float SfxVolume
    {
        get => PlayerPrefs.GetFloat(SfxVolumeKey, 1.0f);
        set
        {
            PlayerPrefs.SetFloat(SfxVolumeKey, value);
            PlayerPrefs.Save();
            SetSfxVolume(value);
        }
    }

    public float MasterVolume
    {
        get => PlayerPrefs.GetFloat(MasterVolumeKey, 1.0f);
        set
        {
            PlayerPrefs.SetFloat(MasterVolumeKey, value);
            PlayerPrefs.Save();
            SetMasterVolume(value);
            OnMuteStateChanged?.Invoke(value == 0f);
        }
    }

    public void SubscribeToMuteStateChanged(MuteStateChanged handler)
    {
        OnMuteStateChanged += handler;
        // Immediately invoke the handler with the current state
        handler?.Invoke(isMuted);
    }

    public void ToggleMute()
    {
        if(masterMixer.GetFloat(MasterVolumeKey, out var volume)) {
            Instance.MasterVolume = volume == 0f ? 0f : 1f;
        }
    }

    public void ApplySettings()
    {
        // Apply initial settings
        SetSfxVolume(SfxVolume);
        SetMusicVolume(MusicVolume);
        SetMasterVolume(MasterVolume);
    }

    public void SetMusicVolume(Slider slider) => MusicVolume = slider.value;
    public void SetSfxVolume(Slider slider) => SfxVolume = slider.value;
    public void SetMusicVolume(float volume) => SetVolume(MusicVolumeKey, volume);
    public void SetSfxVolume(float volume) => SetVolume(SfxVolumeKey, volume);
    public void SetMasterVolume(float volume) => SetVolume(MasterVolumeKey, volume);
    private void SetVolume(string key, float volume) => masterMixer.SetFloat(key, Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1.0f)) * 20);

    /// <summary>
    /// Pause background music, call on Time.TimeScale changes if music should stop and resume during this action.
    /// </summary>
    /// <param name="isPaused">Boolean that determines if we are entering pause state.</param>
    private void HandlePauseChange(bool isPaused)
    {
        if (isPaused)
            musicSource.Pause();
        else
            musicSource.UnPause();
    }

    private void HandleSceneChange(Scene scene)
    {
        switch (scene.name)
        {
            default:
                if(!musicSource.isPlaying) {
                    PlayMusic("SadPiano");
                }
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
        var sound = Array.Find(Instance.sfxSounds, s => s.name == name);
        if (sound != null)
        {
            Instance.sfxSource.clip = sound.audioClip;
            Instance.sfxSource.Play();
        }
        else
        {
            Debug.Log($"Couldn't find sound effect by name: {name}");
        }
    }
}