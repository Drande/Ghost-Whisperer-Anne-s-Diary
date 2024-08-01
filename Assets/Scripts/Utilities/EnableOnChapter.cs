using UnityEngine;

public class EnableOnChapter : MonoBehaviour {
    [SerializeField] private int chapter;

    private void Start()
    {
        gameObject.SetActive(GameManager.Instance.currentChapter == chapter);
    }
}