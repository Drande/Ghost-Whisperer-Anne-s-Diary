using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

[RequireComponent(typeof(PlayableDirector))]
public class SmartCamera : MonoBehaviour
{
    private PlayableDirector director;
    [SerializeField] private UnityEvent onStopped;
    [SerializeField] private UnityEvent onStarted;

    // Start is called before the first frame update
    void Start()
    {
        onStarted?.Invoke();
        director = GetComponent<PlayableDirector>();
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector dir)
    {
        onStopped?.Invoke();
    }

    void OnDestroy()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }
}
