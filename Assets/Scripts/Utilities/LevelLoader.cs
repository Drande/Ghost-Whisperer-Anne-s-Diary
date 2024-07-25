using System;
using System.Collections;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance { get; private set; }
    [SerializeField] private Animator animator;
    public event Action onTransitionCompleted;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        StartCoroutine(StartTransition());
    }

    private IEnumerator StartTransition() {
        AnimatorStateInfo stateInfo;
        do
        {
            yield return new WaitForSeconds(0.1f);
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        } while(stateInfo.normalizedTime < 1.0f);
        onTransitionCompleted?.Invoke();
        onTransitionCompleted = null;
    }

    public void Animate(Action onComplete) {
        StartCoroutine(Transition(onComplete));
    }

    private IEnumerator Transition(Action onComplete) {
        animator.SetTrigger("Start");
        AnimatorStateInfo stateInfo;
        do
        {
            yield return new WaitForSeconds(0.1f);
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        } while(stateInfo.normalizedTime < 1.0f);
        onComplete?.Invoke();
    }

    private void OnDestroy() {
        onTransitionCompleted = null;
    }
}