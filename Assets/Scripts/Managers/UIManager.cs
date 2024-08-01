using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private MenuScreen[] menuScreens;
    [SerializeField] private Button continueButton;
    [SerializeField] private TMP_Dropdown languageDropdown;
    private const string PlayerPrefKey = "SelectedLanguage";

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(InitalizeLanguageSelector());
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
                    StartCoroutine(Coroutines.FadeIn(menuScreen.screen));
                }
            }
            else {
                if (menuScreen.screen.activeInHierarchy) 
                {
                    StartCoroutine(Coroutines.FadeOut(menuScreen.screen));
                }
            }
        }
    }

    private void OnDestroy()
    {
        // Remove listeners when the object is destroyed to avoid memory leaks
        musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);
        sfxVolumeSlider.onValueChanged.RemoveListener(OnSfxVolumeChanged);
        GameManager.Instance.OnChapterLoaded -= HandleChapterLoad;
        languageDropdown.onValueChanged.RemoveAllListeners();
    }

    private IEnumerator InitalizeLanguageSelector()
    {
        // Populate the dropdown with the available locales
        yield return LocalizationSettings.InitializationOperation;
        var locales = LocalizationSettings.AvailableLocales.Locales;
        languageDropdown.options.Clear();
        foreach (var locale in locales)
        {
            languageDropdown.options.Add(new TMP_Dropdown.OptionData(locale.name));
        }

        // Check if a language has been selected previously
        if (PlayerPrefs.HasKey(PlayerPrefKey))
        {
            int savedLanguageIndex = PlayerPrefs.GetInt(PlayerPrefKey, 0); // Default to 0 if no value is found

            // Ensure the saved index is within bounds
            if (savedLanguageIndex >= 0 && savedLanguageIndex < locales.Count)
            {
                languageDropdown.value = savedLanguageIndex;
                LocalizationSettings.SelectedLocale = locales[savedLanguageIndex];
            }
        }
        else
        {
            // If no language has been selected previously, use the default locale set by Unity
            var selectedLocale = LocalizationSettings.SelectedLocale;
            for (int i = 0; i < locales.Count; i++)
            {
                if (locales[i] == selectedLocale)
                {
                    languageDropdown.value = i;
                    break;
                }
            }
        }

        // Add listener to handle changes
        languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
    }

    private void OnLanguageChanged(int index)
    {
        var selectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        LocalizationSettings.SelectedLocale = selectedLocale;

        // Save the selected language index to PlayerPrefs
        PlayerPrefs.SetInt(PlayerPrefKey, index);
        PlayerPrefs.Save();
    }
}
