using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private string pickSfx, dropSfx;
    private Vector3 originalPosition;
    private float placeThreshold = 0.5f;
    private bool _isDragging;
    private bool _isPlaced;
    private Vector2 offset;
    private PuzzleSlot _slot;

    public void Init(PuzzleSlot slot) {
        _slot = slot;
    }

    private void Start() {
        originalPosition = transform.position;
    }

    void Update()
    {
        if(_isPlaced) return;
        if(!_isDragging) return;

        var mousePosition = (Vector2)Camera.main.MouseWorldPosition(transform.position.z - Camera.main.transform.position.z);
        var newPosition = mousePosition - offset;
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
    
    private void OnMouseDown() {
        _isDragging = true;
        AudioManager.Instance.PlaySFX(pickSfx);
        offset = Camera.main.MouseWorldPosition(transform.position.z - Camera.main.transform.position.z) - transform.position;
    }

    private void OnMouseUp() {
        if(((Vector2)(transform.position - _slot.transform.position)).magnitude <= placeThreshold) {
            transform.position = _slot.transform.position;
            _isPlaced = true;
            _slot.Placed();
        } else {
            _isDragging = false;
            transform.position = originalPosition;
            AudioManager.Instance.PlaySFX(dropSfx);
        }
    }
}
