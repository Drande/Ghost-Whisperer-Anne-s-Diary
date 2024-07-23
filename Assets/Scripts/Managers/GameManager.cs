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
    public void LoadSimon() => LoadMinigame(GameScenes.Simon);

    public void LoadMinigame(string sceneName) {
        try {
            SceneManager.LoadScene(sceneName);
        } catch(Exception ex) {
            Debug.LogError("The scene " + sceneName + " could not be loaded: " + ex.Message);
        }
    }
}
