using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGroup : MonoBehaviour
{
    [SerializeField] private List<CardSingleUI> cardSingleUIList = new List<CardSingleUI>();
    [SerializeField] private List<CardSingleUI> selectedCardList = new List<CardSingleUI>();

    [SerializeField] private Sprite cardIdle;
    [SerializeField] private Sprite cardActive;

    [SerializeField] private int attemps;
    [SerializeField] private string matchSfx;
    [SerializeField] private string noMatchSfx;
    public MemoryGameManagerUI memoryGameManager;
    public event EventHandler OnCardMatch;

    public void Start()
    {
        attemps = 20;
    }

    public void Update()
    {
        GameOverForAttemps();
        Debug.Log("attemps: " + attemps);
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

    public void OnCardSelected(CardSingleUI cardSingleUI)
    {
        selectedCardList.Add(cardSingleUI);
        cardSingleUI.Select();
        cardSingleUI.GetCardFrontBackground().sprite = cardActive;

        if ( cardSingleUI.name == "Belt")
        {
            Debug.Log("uh... This card stirs up a painful memory for me."); //Logic to deploy message
        }

        if (selectedCardList.Count == 2)
        {
            CountingAttemps();

            if (CheckIfMatch())
            {
                foreach (CardSingleUI cardSingle in selectedCardList)
                {
                    cardSingle.DisableCardBackButton();
                    cardSingle.SetObjectMatch();
                }
                selectedCardList.Clear();
                OnCardMatch?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                StartCoroutine(DontMatch());
            }
        }

        ResetTabs();
    }

    public void ResetTabs()
    {
        foreach (CardSingleUI cardSingleUI in selectedCardList)
        {
            if (selectedCardList != null && selectedCardList.Count < 3) continue;

            cardSingleUI.GetCardBackBackground().sprite = cardIdle;
        }
    }

    private IEnumerator DontMatch()
    {
        yield return new WaitForSeconds(0.5f);

        foreach (CardSingleUI cardSingleUI in selectedCardList)
        {
            cardSingleUI.Deselect();
        }
        selectedCardList.Clear();
    }

    private bool CheckIfMatch()
    {
        if (selectedCardList.Count < 2)
        {
            return false;

        }

        CardSingleUI firstCard = selectedCardList[0];

        foreach (CardSingleUI cardSingleUI in selectedCardList)
        {
            if (cardSingleUI.name != firstCard.name)
            {
                if (Camera.main.TryGetComponent<CameraShaker>(out var camManager)) camManager.Shake(); //Convert canvas to worldspace for working
                //AudioManager.Instance.PlaySFX(noMatchSfx);
                return false;
            }
        }
        //AudioManager.Instance.PlaySFX(matchSfx);
        return true;

    }

    public void CountingAttemps()
    {
        if (selectedCardList.Count == 2)
        {
            attemps--;
            Debug.Log("attemps: " + attemps);
        }
    }

    public void GameOverForAttemps() 
    {
        if (attemps < 1) 
        {
            if (Camera.main.TryGetComponent<CameraShaker>(out var camManager)) camManager.Shake(); //Convert canvas to worldspace for working
            Debug.Log("Game Over"); //logic for game over
        }
    }
}
