using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject[] chapterPrefabs;
    private const string CurrentChapterKey = "CurrentChapter";
    private int currentChapter = 0;

    public void SaveChapter(int value)
    {
        PlayerPrefs.SetInt(CurrentChapterKey, value);
        PlayerPrefs.Save();
    }

    public void LoadIntegerForCurrentScene()
    {
        currentChapter = PlayerPrefs.GetInt(CurrentChapterKey, 0);
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
        SceneManager.activeSceneChanged += (previous, current) =>
        {
            HandleSceneChange(current);
        };
    }

    private void HandleSceneChange(Scene scene)
    {
        switch (scene.name)
        {
            case GameScenes.Game:
                LoadIntegerForCurrentScene();
                HandleCurrentChapter();
                // MessageInScreen.Instance.StartDialog(ChapterOneDialogs.Start, () => {
                //     // TODO: Configurar acciones despues de que termina el dialogo.
                //     Debug.Log("Termino el dialogo");
                // });
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
        SceneManager.LoadScene(GameScenes.Game);
    }

    public void ContinueGame() {
        SceneManager.LoadScene(GameScenes.Game);
    }

    public void BackToGame(bool completed = false) {
        if(completed) { SaveChapter(currentChapter + 1); }
        SceneManager.LoadScene(GameScenes.Game);
    }

    public void BackToTitle() {
        MessageInScreen.Instance.Stop();
        SceneManager.LoadScene(GameScenes.MainMenu);
    }

    public void LoadPuzzle() 
    {
        if(MessageInScreen.Instance.isActive) return;
        MessageInScreen.Instance.StartDialog(ChapterOneDialogs.Start, () => {
            // TODO: Configurar acciones despues de que termina el dialogo.
            LoadMinigame(GameScenes.PuzzleAssembly);
        });
    }
  
    public void LoadMemory() {
        if (MessageInScreen.Instance.isActive) return;
        MessageInScreen.Instance.StartDialog(ChapterTwoDialogs.Start, () => {
            // TODO: Configurar acciones despues de que termina el dialogo.
            LoadMinigame(GameScenes.MemoryMatch);
        });
    }

    public void LoadSimon() {
        MessageInScreen.Instance.StartDialog(ChapterThreeDialogs.Start, () => {
            // TODO: Configurar acciones despues de que termina el dialogo.
            LoadMinigame(GameScenes.SimonSays);
        });
    }

    public void LoadMinigame(string sceneName) {
        try {
            SceneManager.LoadScene(sceneName);
        } catch(Exception ex) {
            Debug.LogError("The scene " + sceneName + " could not be loaded: " + ex.Message);
        }
    }

    public void RestartScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(currentSceneName);
    }

}
