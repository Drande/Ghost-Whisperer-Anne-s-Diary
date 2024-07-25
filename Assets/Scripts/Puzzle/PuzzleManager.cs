using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    private int _placedPieces;
    [SerializeField] private GameObject puzzleParent;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject[] piecePrefabs;
    [SerializeField] private float slotSize = 1.0f;
    [SerializeField] private Bounds bounds;

    private void Start() {
        BuildPuzzle();
    }

    void BuildPuzzle()
    {
        int totalPieces = piecePrefabs.Length;
        int columns = Mathf.CeilToInt(Mathf.Sqrt(totalPieces)); // Calculate columns for grid
        int rows = Mathf.CeilToInt((float)totalPieces / columns); // Calculate rows for grid
        float xOffset = (columns - 1) * slotSize / 2;
        float yOffset = (rows - 1) * slotSize / 2;
        for (int i = 0; i < totalPieces; i++)
        {
            int column = i % columns;
            int row = i / columns;

            // Calculate slot position in the grid
            Vector3 slotPosition = new Vector3(column * slotSize - xOffset, yOffset - row * slotSize, 0);

            // Instantiate slot at calculated position
            var slot = Instantiate(slotPrefab, slotPosition + puzzleParent.transform.position, Quaternion.identity, puzzleParent.transform).GetComponent<PuzzleSlot>();
            slot.OnPlaced += () => {
                _placedPieces++;
                if(_placedPieces == piecePrefabs.Length) {
                    OnPuzzleCompleted();
                }
            };

            // Instantiate piece at a random position within bounds
            Vector3 randomPosition = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
            var piece = Instantiate(piecePrefabs[i], randomPosition, Quaternion.identity);
            piece.GetComponent<PuzzlePiece>().Init(slot);
        }
    }

    private void OnPuzzleCompleted()
    {
        if (MessageInScreen.Instance.isActive) return;
        MessageInScreen.Instance.StartDialog(AfterChapterOneDialogs.Start, () => 
        {
        GameManager.Instance.BackToGame(true);
        });
    }
}
