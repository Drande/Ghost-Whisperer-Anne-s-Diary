using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
                MessageInScreen.Instance.StartDialog(ChapterOneDialogs.Start, () => {
                    // TODO: Configurar acciones despues de que termina el dialogo.
                    Debug.Log("Termino el dialogo");
                });
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

    public void StartGame() {
        SceneManager.LoadScene(GameScenes.Game);
    }

    public void BackToGame() {
        SceneManager.LoadScene(GameScenes.Game);
    }

    public void BackToTitle() {
        SceneManager.LoadScene(GameScenes.MainMenu);
    }

    public void LoadPuzzle() => LoadMinigame(GameScenes.PuzzleAssembly);
    public void LoadMemory() => LoadMinigame(GameScenes.MemoryMatch);
    public void LoadSimon() => LoadMinigame(GameScenes.SimonSays);

    public void LoadMinigame(string sceneName) {
        try {
            SceneManager.LoadScene(sceneName);
        } catch(Exception ex) {
            Debug.LogError("The scene " + sceneName + " could not be loaded: " + ex.Message);
        }
    }
}
