using UnityEngine.UI;
using UnityEngine;

public class MuteToggler : MonoBehaviour
{
    [SerializeField] private Sprite enabledSprite;
    [SerializeField] private Sprite disabledSprite;
    private Image imageComponent;

    // Start is called before the first frame update
    void Start()
    {
        imageComponent = GetComponent<Image>();
        AudioManager.Instance.SubscribeToMuteStateChanged(HandleMuteStateChanged);
    }

    private void HandleMuteStateChanged(bool isMuted)
    {
        imageComponent.sprite = isMuted ? disabledSprite : enabledSprite;
    }

    private void OnDestroy() {
        AudioManager.Instance.OnMuteStateChanged -= HandleMuteStateChanged;
    }
}
