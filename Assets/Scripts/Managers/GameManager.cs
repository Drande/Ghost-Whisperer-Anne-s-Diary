using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject[] chapterPrefabs;
    private const string CurrentChapterKey = "CurrentChapter";
    public int currentChapter { get; private set; }
    public event Action OnChapterLoaded;
    public delegate void PauseStateChanged(bool isPaused);
    public event PauseStateChanged OnPauseStateChanged;
    public bool isPaused => Time.timeScale == 0;

    private void SaveChapter(int value)
    {
        PlayerPrefs.SetInt(CurrentChapterKey, value);
        PlayerPrefs.Save();
    }

    private void LoadIntegerForCurrentScene()
    {
        currentChapter = PlayerPrefs.GetInt(CurrentChapterKey, 0);
        OnChapterLoaded?.Invoke();
    }

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        HandleSceneChange(SceneManager.GetActiveScene());
        SceneManager.activeSceneChanged += (_, current) => HandleSceneChange(current);
    }

    public void TogglePause() {
        if(Time.timeScale == 0) {
            Time.timeScale = 1;
        } else {
            Time.timeScale = 0;
        }
        Instance.OnPauseStateChanged?.Invoke(isPaused);
    }

    private void HandleSceneChange(Scene scene)
    {
        switch (scene.name)
        {
            case GameScenes.Game:
                LoadIntegerForCurrentScene();
                HandleCurrentChapter();
            break;
            case GameScenes.PuzzleAssembly:
            break;
            case GameScenes.MemoryMatch:
            break;
            case GameScenes.SimonSays:
            break;
            default:
            break;
        }
    }
    
    private void HandleCurrentChapter() {
        if(currentChapter < chapterPrefabs.Length) {
            Instantiate(chapterPrefabs[currentChapter]);
        }
    }

    public void StartGame() {
        SaveChapter(0);
        LoadScene(GameScenes.Game);
    }

    public void ContinueGame() {
        LoadScene(GameScenes.Game);
    }

    public void BackToGame(bool completed = false) {
        if(completed) { SaveChapter(currentChapter + 1); }
        LoadScene(GameScenes.Game);
    }

    public void BackToTitle() {
        MessageInScreen.Instance.Stop();
        LoadScene(GameScenes.MainMenu);
    }

    public void StartMainMenu() {
        LoadScene(GameScenes.MainMenu);
    }

    public void LoadPuzzle() 
    {
        if(MessageInScreen.Instance.isActive) return;
        MessageInScreen.Instance.StartDialog(ChapterOneDialogs.Start, () => {
            LoadMinigame(GameScenes.PuzzleAssembly);
        });
    }
  
    public void LoadMemory() {
        if (MessageInScreen.Instance.isActive) return;
        MessageInScreen.Instance.StartDialog(ChapterTwoDialogs.Start, () => {
            LoadMinigame(GameScenes.MemoryMatch);
        });
    }

    public void LoadSimon() {
        MessageInScreen.Instance.StartDialog(ChapterThreeDialogs.Start, () => {
            LoadMinigame(GameScenes.SimonSays);
        });
    }

    public void LoadMinigame(string sceneName) {
        try {
            LoadScene(sceneName);
        } catch(Exception ex) {
            Debug.LogError("The scene " + sceneName + " could not be loaded: " + ex.Message);
        }
    }

    public void RestartScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        LoadScene(currentSceneName);
    }

    private void LoadScene(string sceneName) {
        if(LevelLoader.Instance != null) {
            LevelLoader.Instance.Animate(() => SceneManager.LoadScene(sceneName));
        } else {
            SceneManager.LoadScene(sceneName);
        }
    }

}
