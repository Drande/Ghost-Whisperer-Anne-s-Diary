using UnityEngine;

public delegate void OnPlaced();
public class PuzzleSlot : MonoBehaviour
{
    [SerializeField] private string completeSfx;
    [SerializeField] private PuzzlePiece puzzlePiecePrefab;
    [SerializeField] private Transform pieceSpawnPoint;
    public event OnPlaced OnPlaced;

    private void Awake() {
        if(puzzlePiecePrefab) {
            var piece = Instantiate(puzzlePiecePrefab, pieceSpawnPoint.position, Quaternion.identity);
            piece.Init(this);
        }
    }

    public void Placed() {
        AudioManager.Instance.PlaySFX(completeSfx);
        OnPlaced.Invoke();
        OnPlaced = null;
    }
}
