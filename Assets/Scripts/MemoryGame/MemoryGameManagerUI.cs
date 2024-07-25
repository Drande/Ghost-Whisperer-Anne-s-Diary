using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MemoryGameManagerUI : MonoBehaviour
{
    public static MemoryGameManagerUI Instance { get; private set; }

    [SerializeField] private CardGroup cardGroup;
    [SerializeField] private List<CardSingleUI> cardSingleUIList = new List<CardSingleUI>();
    [SerializeField] private GameObject gameArea;

    [SerializeField] private string completeGameSfx;
    private bool isOnComplete = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        cardGroup.OnCardMatch += CardGroup_OnCardMatch;
        StartGame();
    }

    private void StartGame()
    {
        ToggleGameArea(true);
    }

    public void Subscribe(CardSingleUI cardSingleUI)
    {
        if (cardSingleUIList == null)
        {
            cardSingleUIList = new List<CardSingleUI>();
        }

        if (!cardSingleUIList.Contains(cardSingleUI))
        {
            cardSingleUIList.Add(cardSingleUI);
        }
    }

    private void CardGroup_OnCardMatch(object sender, System.EventArgs e)
    {
        if (cardSingleUIList.All(x => x.GetObjectMatch()))
        {
            StartCoroutine(OnCompleteGame());
        }
    }

    private IEnumerator OnCompleteGame()
    {
        if (isOnComplete)
        {
            yield return null;
        }
        else
        {
            isOnComplete = true;
            StartAfterChapterTwoDialog();
            yield return new WaitForSeconds(0.75f);
            // AudioManager.Instance.PlaySFX(completeGameSfx);
            isOnComplete = false;
        }
    }

    public void Restart()
    {
        cardSingleUIList.Clear();
    }

    private void ToggleGameArea(bool toggle)
    {
        gameArea.SetActive(toggle);
    }

    private void StartAfterChapterTwoDialog()
    {
        if (MessageInScreen.Instance.isActive) return;
        MessageInScreen.Instance.StartDialog(AfterChapterTwoDialogs.Start, () => {
            GameManager.Instance.BackToGame(true);
        });
    }
}
