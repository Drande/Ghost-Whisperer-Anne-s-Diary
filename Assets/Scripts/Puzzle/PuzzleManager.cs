using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    private int _slotCount;
    private int _placedPieces;

    private void Start() {
        var slots = GameObject.FindObjectsOfType<PuzzleSlot>();
        Debug.Log("Found " + slots.Length);
        _slotCount = slots.Length;
        foreach (var slot in slots)
        {
            slot.OnPlaced += () => {
                _placedPieces++;
                if(_placedPieces == _slotCount) {
                    OnPuzzleCompleted();
                }
            };
        }
    }

    private void OnPuzzleCompleted() {
        GameManager.Instance.BackToGame(true);
    }
}
