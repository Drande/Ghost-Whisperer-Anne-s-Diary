using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PauseToggler : MonoBehaviour {
    [SerializeField] private Sprite enabledSprite;
    [SerializeField] private Sprite disabledSprite;
    private Image imageComponent;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        GameManager.Instance.OnPauseStateChanged += HandlePauseStateChanged;
    }

    private void HandlePauseStateChanged(bool isPaused)
    {
        imageComponent.sprite = isPaused ? disabledSprite : enabledSprite;
    }

    private void OnDestroy() {
        GameManager.Instance.OnPauseStateChanged -= HandlePauseStateChanged;
    }
}