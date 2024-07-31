using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSaysManager : MonoBehaviour
{
    [SerializeField] private int maxRounds = 6;
    [SerializeField] private float highlightDelay = 0.5f;
    [SerializeField] private float roundDelay = 2f;
    private SimonSaysTarget[] targets;
    private readonly List<int> sequence = new();
    private int _currentStep = 0;
    private bool _playerTurn = false;

    void Start()
    {
        targets = FindObjectsOfType<SimonSaysTarget>();
        StartCoroutine(GameRoutine());
    }

    IEnumerator GameRoutine()
    {
        while (sequence.Count <= maxRounds)
        {
            yield return new WaitForSeconds(roundDelay);
            _currentStep = 0;
            sequence.Add(targets.NextIndex());
            yield return StartCoroutine(ShowSequence());
            _playerTurn = true;
            yield return StartCoroutine(PlayerInput());
            _playerTurn = false;
        }
        HandleWin();
    }

    private void HandleWin() {
        if (MessageInScreen.Instance.isActive) return;
        MessageInScreen.Instance.StartDialog(AfterChapterThreeDialogs.Start, (_) => 
        {
        GameManager.Instance.BackToGame(true);
        });
    }

    IEnumerator ShowSequence()
    {
        for (int i = 0; i < sequence.Count; i++)
        {
            var target = targets[sequence[i]];
            yield return new WaitForSeconds(target.Highlight() + highlightDelay);
        }
    }

    IEnumerator PlayerInput()
    {
        while (_playerTurn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    var clickedTarget = hit.collider.gameObject.GetComponent<SimonSaysTarget>();
                    if(clickedTarget == null) {
                        yield return null;
                        continue;
                    };

                    clickedTarget.Highlight();
                    if (clickedTarget.id == targets[sequence[_currentStep]].id)
                    {
                        _currentStep++;
                        if (_currentStep >= sequence.Count)
                        {
                            _playerTurn = false;
                        }
                    }
                    else
                    {
                        if(Camera.main.TryGetComponent<CameraShaker>(out var camManager)) camManager.Shake();
                        sequence.Clear();
                        break;
                    }
                }
            }
            yield return null;
        }
    }
}
