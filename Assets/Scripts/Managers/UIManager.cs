using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private MenuScreen[] menuScreens;
    [SerializeField] private Button continueButton;

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
                if(!menuScreen.screen.activeInHierarchy)
                    StartCoroutine(Coroutines.FadeIn(menuScreen.screen));
            }
            else {
                if(menuScreen.screen.activeInHierarchy)
                    StartCoroutine(Coroutines.FadeOut(menuScreen.screen));
            }
        }
    }

    private void OnDestroy()
    {
        // Remove listeners when the object is destroyed to avoid memory leaks
        musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);
        sfxVolumeSlider.onValueChanged.RemoveListener(OnSfxVolumeChanged);
        GameManager.Instance.OnChapterLoaded -= HandleChapterLoad;
    }
}
