using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class OptionEvent {
    public string option;
    public UnityEvent actions;
}

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogData dialogData;
    [SerializeField] private Transform hotspot;
    public OptionEvent[] onConfirm;

    public Transform GetHotspot() => hotspot;

    public void Interact(Action action)
    {
        MessageInScreen.Instance.StartDialog(dialogData.messages, (result) => {
            var optionResult = onConfirm.FirstOrDefault(a => a.option == result);
            if (optionResult?.actions != default) {
                optionResult.actions.Invoke();
            }
            action?.Invoke();
        });
    }
}
