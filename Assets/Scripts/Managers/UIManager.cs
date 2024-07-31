using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private MenuScreen[] menuScreens;
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject textGameTitle;
    [SerializeField] private GameObject btnStart;
    [SerializeField] private GameObject btnContinue;
    [SerializeField] private GameObject btnSettings;
    [SerializeField] private GameObject btnAbout;
    [SerializeField] private bool MovingToAnotherScreen = false;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameManager.Instance.OnChapterLoaded += HandleChapterLoad;
        HandleChapterLoad();

        // Initialize slider values with the current settings
        musicVolumeSlider.value = AudioManager.Instance.MusicVolume;
        sfxVolumeSlider.value = AudioManager.Instance.SfxVolume;

        // Add listeners to handle value changes
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSfxVolumeChanged);
    }

    private void HandleChapterLoad() {
        continueButton.interactable = GameManager.Instance.currentChapter > 0;
    }

    private void OnMusicVolumeChanged(float value)
    {
        AudioManager.Instance.MusicVolume = value;
    }

    private void OnSfxVolumeChanged(float value)
    {
        AudioManager.Instance.SfxVolume = value;
    }

    public void SetScreen(string name)
    {
        foreach (var menuScreen in menuScreens)
        {
            if(menuScreen.name == name) {
                if (!menuScreen.screen.activeInHierarchy) 
                {
                    if (MovingToAnotherScreen) 
                    {
                        Invoke("ShowButtons", 1f);
                        MovingToAnotherScreen = false;
                    }
                    StartCoroutine(Coroutines.FadeIn(menuScreen.screen));
                }
            }
            else {
                if (menuScreen.screen.activeInHierarchy) 
                {
                    Invoke("HideButtons", 0f);
                    StartCoroutine(Coroutines.FadeOut(menuScreen.screen));

                    MovingToAnotherScreen = true;
                } 
            }
        }
    }

    private void ShowButtons()
    {
        textGameTitle.SetActive(true);
        btnStart.SetActive(true);
        btnContinue.SetActive(true);
        btnSettings.SetActive(true);
        btnAbout.SetActive(true);
    }

    private void HideButtons()
    {
        textGameTitle.SetActive(false);
        btnStart.SetActive(false);
        btnContinue.SetActive(false);
        btnSettings.SetActive(false);
        btnAbout.SetActive(false);
    }

    private void OnDestroy()
    {
        // Remove listeners when the object is destroyed to avoid memory leaks
        musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);
        sfxVolumeSlider.onValueChanged.RemoveListener(OnSfxVolumeChanged);
        GameManager.Instance.OnChapterLoaded -= HandleChapterLoad;
    }
}
