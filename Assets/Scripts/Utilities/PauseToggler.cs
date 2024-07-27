using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PauseToggler : MonoBehaviour {
    [SerializeField] private Sprite enabledSprite;
    [SerializeField] private Sprite disabledSprite;
    private Image imageComponent;
    private Button button;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(TogglePause);
    }

    void Destroy() {
        button.onClick.RemoveListener(TogglePause);
    }

    public void TogglePause() {
        if(Time.timeScale == 0) {
            Time.timeScale = 1;
            imageComponent.sprite = enabledSprite;
        } else {
            Time.timeScale = 0;
            imageComponent.sprite = disabledSprite;
        }
    }
}