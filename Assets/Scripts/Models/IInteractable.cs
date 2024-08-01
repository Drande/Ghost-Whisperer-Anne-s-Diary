using System;
using UnityEngine;

public interface IInteractable {
    void Interact(Action action);
    Transform GetHotspot();
}